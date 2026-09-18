using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Products.Domain;
using Modules.Products.Infrastructure.Persistence.Seed;

namespace Modules.Products.Infrastructure.Persistence.Configurations;

public class ProductDescriptionConfiguration : IEntityTypeConfiguration<ProductDescription>
{
    public void Configure(EntityTypeBuilder<ProductDescription> builder)
    {
        // Shares its primary key with Product (one-to-one), so Id is the product id.
        builder.HasData(ProductSeed.Items.Select(p => new ProductDescription
        {
            Id = p.Id,
            Description = p.Description,
            CreatedAt = ProductSeed.SeedDate,
            IsDeleted = false
        }));
    }
}
