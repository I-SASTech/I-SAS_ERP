using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PaymentRefundConfiguration : IEntityTypeConfiguration<PaymentRefund>
    {
        public void Configure(EntityTypeBuilder<PaymentRefund> builder)
        {
            builder.HasOne(d => d.Account)
                .WithMany(d => d.PaymentRefundBankCash)
                .HasForeignKey(d => d.BankCashAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ContactAccount)
                .WithMany(d => d.PaymentRefundContact)
                .HasForeignKey(d => d.ContactAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.TaxRate)
                .WithMany(d => d.PaymentRefunds)
                .HasForeignKey(d => d.TaxRateId);
        }
    }
}
