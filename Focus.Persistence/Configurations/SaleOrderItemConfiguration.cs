using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class SaleOrderItemConfiguration : IEntityTypeConfiguration<SaleOrderItem>
    {
        public void Configure(EntityTypeBuilder<SaleOrderItem> builder)
        {
        
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.SaleOrderId).IsRequired();
            builder.Property(x => x.TaxRateId).IsRequired();

            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.SaleOrderItems)
                .HasForeignKey(ad => ad.SaleOrderId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.SaleOrderItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.WareHouse)
                .WithMany(ad => ad.SaleOrderItems)
                .HasForeignKey(ad => ad.WareHouseId);

            builder.HasOne(s => s.SaleOrderVersion)
                .WithMany(ad => ad.SaleOrderItems)
                .HasForeignKey(ad => ad.SaleOrderVersionId);
        }
    }
}
