using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.SaleId).IsRequired();
            builder.Property(x => x.TaxRateId).IsRequired();

            builder.HasOne(s => s.Sale)
                .WithMany(ad => ad.SaleItems)
                .HasForeignKey(ad => ad.SaleId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.SaleItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.Product)
               .WithMany(ad => ad.SaleItems)
               .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.WareHouse)
               .WithMany(ad => ad.SaleItems)
               .HasForeignKey(ad => ad.WareHouseId);

            builder.HasOne(s => s.Color)
               .WithMany(ad => ad.SaleItems)
               .HasForeignKey(ad => ad.ColorId);

            builder.HasOne(s => s.PromotionOfferItem)
               .WithMany(ad => ad.SaleItems)
               .HasForeignKey(ad => ad.PromotionItemId);
        }
    }
}
