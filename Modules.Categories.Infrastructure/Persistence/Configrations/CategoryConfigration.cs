using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Categories.Domain;

namespace Modules.Categories.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        // Seeded rows use explicit ids 1-8; start the identity sequence past them so the first
        // category created through the API does not collide with seed data.
        builder.Property(c => c.Id).HasIdentityOptions(startValue: 100);

        builder.HasQueryFilter(x => !x.IsDeleted);

        var seedDate = new DateTimeOffset(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

        // Ids are referenced by the Products module seed (ProductSeed.CategoryId).
        builder.HasData(
            new Category { Id = 1, Name = "Elektronika", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 2, Name = "Kompüter və Aksesuarlar", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 3, Name = "Geyim", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 4, Name = "Ayaqqabı", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 5, Name = "Ev və Mebel", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 6, Name = "Mətbəx", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 7, Name = "İdman və Əyləncə", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 8, Name = "Kitablar", CreatedAt = seedDate, IsDeleted = false }
        );
    }
}
