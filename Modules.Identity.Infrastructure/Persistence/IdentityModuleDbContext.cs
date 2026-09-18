using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Identity.Domain;

namespace Modules.Identity.Infrastructure.Persistence;

public class IdentityModuleDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityModuleDbContext(DbContextOptions<IdentityModuleDbContext> options)
        : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");

        builder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.TokenHash).IsRequired().HasMaxLength(64);
            entity.Property(t => t.ReplacedByTokenHash).HasMaxLength(64);
            entity.HasIndex(t => t.TokenHash).IsUnique();

            // Same module and DbContext as the user, so a real foreign key is fine here.
            entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
