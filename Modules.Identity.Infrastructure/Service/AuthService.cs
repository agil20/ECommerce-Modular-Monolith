using Common.Authorization;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Contracts.AuthDTOs;
using Modules.Identity.Contracts.Services;
using Modules.Identity.Domain;
using Modules.Identity.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Modules.Identity.Infrastructure.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Təhlükəsizlik: "email yoxdur" ilə "parol səhvdir" ayrı mesaj olmamalıdır
        if (user is null)
            throw new NotFoundException("Email və ya parol yanlışdır");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            throw new NotFoundException("Email və ya parol yanlışdır");

        return await GenerateTokenAsync(user);
    }

    private async Task<AuthResponse> GenerateTokenAsync(ApplicationUser user)
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

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires,
            UserId = user.Id,
            Email = user.Email!
        };
    }
}