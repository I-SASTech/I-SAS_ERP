using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleOrderPaymentConfiguration : IEntityTypeConfiguration<SaleOrderPayment>
    {
        public void Configure(EntityTypeBuilder<SaleOrderPayment> builder)
        {
            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.SaleOrderPayments)
                .HasForeignKey(ad => ad.SaleOrderId);

            builder.HasOne(s => s.PaymentVoucher)
                .WithMany(ad => ad.SaleOrderPayments)
                .HasForeignKey(ad => ad.PaymentVoucherId);
        }
    }
}
