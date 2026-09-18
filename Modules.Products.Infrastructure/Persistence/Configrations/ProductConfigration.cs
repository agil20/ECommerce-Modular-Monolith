using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Products.Domain;
using Modules.Products.Infrastructure.Persistence.Seed;

namespace Modules.Products.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Price).IsRequired();

        // Seeded rows use explicit ids up to 804; start the identity sequence past them so the first
        // product created through the API does not collide with seed data.
        builder.Property(c => c.Id).HasIdentityOptions(startValue: 1000);

        // ProductDescription ilə One-to-One (Birə-Bir) əlaqəsi
        builder.HasOne(p => p.ProductDescription)
               .WithOne(pd => pd.Product)
               .HasForeignKey<ProductDescription>(pd => pd.Id);

        // Qlobal Query Filter (Silinmişləri gətirmə)
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasMany(p => p.ProductPriceHistories)
            .WithOne(ph => ph.Product)
            .HasForeignKey(ph => ph.ProductId);

        builder.HasData(ProductSeed.Items.Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CreatedAt = ProductSeed.SeedDate,
            IsDeleted = false
        }));
    }
}
