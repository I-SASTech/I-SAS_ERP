using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class FinancialYearClosingBalanceConfiguration : IEntityTypeConfiguration<FinancialYearClosingBalance>
    {
        public void Configure(EntityTypeBuilder<FinancialYearClosingBalance> builder)
        {
            builder.HasOne(ea => ea.Account)
                .WithMany(a => a.FinancialYearClosingBalances)
                .HasForeignKey(ea => ea.AccountId);

            builder.HasOne(ea => ea.FinancialYearClosing)
                .WithMany(a => a.FinancialYearClosingBalances)
                .HasForeignKey(ea => ea.FinancialYearClosingId);
        }
    }
}
