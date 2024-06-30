using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasOne(s => s.Products)
                .WithMany(ad => ad.ProductSizes)
                .HasForeignKey(ad => ad.ProductId);
            
            builder.HasOne(s => s.Size)
                .WithMany(ad => ad.ProductSizes)
                .HasForeignKey(ad => ad.SizeId);

        }
    }
}
