using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchasePostItemConfiguration : IEntityTypeConfiguration<PurchasePostItem>
    {
        public void Configure(EntityTypeBuilder<PurchasePostItem> builder)
        {
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount);
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.PurchasePostId).IsRequired();
            builder.Property(x => x.TaxRateId).IsRequired();
            builder.Property(x => x.ExpiryDate);

            builder.HasOne(s => s.PurchasePost)
                .WithMany(ad => ad.PurchasePostItems)
                .HasForeignKey(ad => ad.PurchasePostId);

            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.PurchasePostItems)
                .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.PurchasePostItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.WareHouse)
               .WithMany(ad => ad.GetPurchasePostItems)
               .HasForeignKey(ad => ad.WareHouseId);

            builder.HasOne(s => s.WarrantyType)
               .WithMany(ad => ad.PurchasePostItems)
               .HasForeignKey(ad => ad.WarrantyTypeId);

            builder.HasOne(s => s.Color)
               .WithMany(ad => ad.PurchasePostItems)
               .HasForeignKey(ad => ad.ColorId);
        }
    }
}
