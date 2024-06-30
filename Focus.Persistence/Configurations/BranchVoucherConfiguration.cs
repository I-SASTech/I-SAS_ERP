using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BranchVoucherConfiguration : IEntityTypeConfiguration<BranchVoucher>
    {
        public void Configure(EntityTypeBuilder<BranchVoucher> builder)
        {
            builder.HasOne(d => d.Account)
                .WithMany(d => d.BranchVoucherBankCash)
                .HasForeignKey(d => d.BankCashAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ContactAccount)
                .WithMany(d => d.BranchVoucherContact)
                .HasForeignKey(d => d.ContactAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.TaxRate)
                .WithMany(d => d.BranchVouchers)
                .HasForeignKey(d => d.TaxRateId);
        }
    }
}
