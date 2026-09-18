using Common.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Domain;
using System.Security.Claims;

namespace Modules.Identity.Infrastructure.Persistence;

public static class IdentitySeeder
{
    public const string AdminRole = "Admin";
    public const string UserRole = "User";

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        foreach (var roleName in new[] { AdminRole, UserRole })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var adminRole = await roleManager.FindByNameAsync(AdminRole);
        var existingPermissions = (await roleManager.GetClaimsAsync(adminRole!))
            .Where(c => c.Type == Permissions.ClaimType)
            .Select(c => c.Value)
            .ToHashSet();

        foreach (var permission in Permissions.All.Where(p => !existingPermissions.Contains(p)))
            await roleManager.AddClaimAsync(adminRole!, new Claim(Permissions.ClaimType, permission));

        var adminEmail = configuration["AdminUser:Email"];
        var adminPassword = configuration["AdminUser:Password"];
        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            return;

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(
                    "Admin yaradılmadı: " + string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        if (!await userManager.IsInRoleAsync(admin, AdminRole))
            await userManager.AddToRoleAsync(admin, AdminRole);
    }
}
