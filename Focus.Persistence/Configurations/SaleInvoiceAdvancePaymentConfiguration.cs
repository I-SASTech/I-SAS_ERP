using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleInvoiceAdvancePaymentConfiguration : IEntityTypeConfiguration<SaleInvoiceAdvancePayment>
    {
        public void Configure(EntityTypeBuilder<SaleInvoiceAdvancePayment> builder)
        {
            builder.HasOne(s => s.Sale)
                .WithMany(ad => ad.SaleInvoiceAdvancePayments)
                .HasForeignKey(ad => ad.SaleInvoiceId);

            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.SaleInvoiceAdvancePayments)
                .HasForeignKey(ad => ad.SaleOrderId);
        }
    }
}
