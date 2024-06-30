using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            //builder.Property(x => x.BarCode).IsRequired();
            //builder.Property(x => x.ProductName).IsRequired().HasMaxLength(80);
            //builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(150);
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.PurchaseOrderId).IsRequired();
            builder.Property(x => x.TaxRateId).IsRequired();
            builder.Property(x => x.SerialNo).IsRequired(false);

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchaseOrderItems)
                .HasForeignKey(ad => ad.PurchaseOrderId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.PurchaseOrderItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.PurchaseOrderVersion)
                .WithMany(ad => ad.PurchaseOrderItems)
                .HasForeignKey(ad => ad.PurchaseOrderVersionId);
        }
    }
}
