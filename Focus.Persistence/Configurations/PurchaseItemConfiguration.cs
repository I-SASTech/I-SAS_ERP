using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseItem> builder)
        {
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount);
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.PurchaseId).IsRequired();
            builder.Property(x => x.TaxRateId).IsRequired();
            builder.Property(x => x.SerialNo).IsRequired(false);
            builder.Property(x => x.ExpiryDate);

            builder.HasOne(s => s.Purchase)
                .WithMany(ad => ad.PurchaseItems)
                .HasForeignKey(ad => ad.PurchaseId);

            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.PurchaseItems)
                .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.PurchaseItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.WareHouse)
               .WithMany(ad => ad.PurchaseItems)
               .HasForeignKey(ad => ad.WareHouseId);
        }
    }
}
