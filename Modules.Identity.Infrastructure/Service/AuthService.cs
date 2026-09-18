using Common.Authorization;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Contracts.AuthDTOs;
using Modules.Identity.Contracts.Services;
using Modules.Identity.Domain;
using Modules.Identity.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modules.Identity.Infrastructure.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityModuleDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IdentityModuleDbContext dbContext,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userManager.FindByEmailAsync(request.Email);
        if (existing is not null)
            throw new DublicatedDataException("Bu email ilə istifadəçi artıq mövcuddur");

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
            throw new ConfilictException(errors);
        }

        await _userManager.AddToRoleAsync(user, IdentitySeeder.UserRole);

        return await GenerateTokensAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Same message for unknown email and wrong password, so emails cannot be enumerated.
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedException("Email və ya parol yanlışdır");

        return await GenerateTokensAsync(user);
    }

    public async Task<AuthResponse> RefreshAsync(RefreshRequest request)
    {
        var tokenHash = HashToken(request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(t => t.TokenHash == tokenHash)
            ?? throw new UnauthorizedException("Refresh token etibarsızdır");

        // A revoked token being presented again means it was stolen and replayed:
        // kill every active session of that user.
        if (storedToken.RevokedAt is not null)
        {
            await RevokeAllActiveTokensAsync(storedToken.UserId);
            throw new UnauthorizedException("Refresh token artıq istifadə olunub");
        }

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token-in vaxtı bitib");

        var user = await _userManager.FindByIdAsync(storedToken.UserId)
            ?? throw new UnauthorizedException("Refresh token etibarsızdır");

        storedToken.RevokedAt = DateTime.UtcNow;

        var response = await GenerateTokensAsync(user);

        storedToken.ReplacedByTokenHash = HashToken(response.RefreshToken);
        await _dbContext.SaveChangesAsync();

        return response;
    }

    public async Task LogoutAsync(RefreshRequest request)
    {
        var tokenHash = HashToken(request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(t => t.TokenHash == tokenHash);

        // Logout is idempotent: an unknown or already revoked token is not an error.
        if (storedToken is null || storedToken.RevokedAt is not null)
            return;

        storedToken.RevokedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    private async Task<AuthResponse> GenerateTokensAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow
            .AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"]!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        // Permissions are copied into the token at login, so role changes apply only after the next login.
        var permissions = new HashSet<string>();
        foreach (var roleName in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                continue;

            foreach (var claim in await _roleManager.GetClaimsAsync(role))
            {
                if (claim.Type == Permissions.ClaimType)
                    permissions.Add(claim.Value);
            }
        }

        claims.AddRange(permissions.Select(p => new Claim(Permissions.ClaimType, p)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var refreshTokenExpires = DateTime.UtcNow
            .AddDays(int.Parse(_configuration["Jwt:RefreshTokenDays"]!));

        _dbContext.RefreshTokens.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = HashToken(refreshToken),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = refreshTokenExpires
        });
        await _dbContext.SaveChangesAsync();

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpires,
            UserId = user.Id,
            Email = user.Email!
        };
    }

    private async Task RevokeAllActiveTokensAsync(string userId)
    {
        var now = DateTime.UtcNow;

        await _dbContext.RefreshTokens
            .Where(t => t.UserId == userId && t.RevokedAt == null)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.RevokedAt, now));
    }

    private static string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
