using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleInvoiceDiscountConfiguration : IEntityTypeConfiguration<SaleInvoiceDiscount>
    {
        public void Configure(EntityTypeBuilder<SaleInvoiceDiscount> builder)
        {
            builder.HasOne(s => s.Sale)
                .WithMany(ad => ad.SaleInvoiceDiscounts)
                .HasForeignKey(ad => ad.SaleId);

            builder.HasOne(s => s.Discount)
                .WithMany(ad => ad.SaleInvoiceDiscounts)
                .HasForeignKey(ad => ad.DiscountId);
        }
    }
}
