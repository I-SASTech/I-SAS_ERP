using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    internal class BomRawProductsConfiguration : IEntityTypeConfiguration<BomRawProducts>
    {
        public void Configure(EntityTypeBuilder<BomRawProducts> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,4)");


            builder.HasOne(s => s.Products)
              .WithMany(ad => ad.BomRawProducts)
              .HasForeignKey(ad => ad.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.BomSaleOrderItems)
              .WithMany(ad => ad.BomRawProducts)
              .HasForeignKey(ad => ad.BomSaleOrderItemId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
