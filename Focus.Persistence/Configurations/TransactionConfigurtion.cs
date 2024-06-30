using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TransactionConfigurtion : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.DocumentId).IsRequired();
            builder.Property(x => x.DocumentNumber).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Debit).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Credit).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.Account)
                .WithMany(ad => ad.Transactions)
                .HasForeignKey(ad => ad.AccountId);
            builder.HasOne(c => c.Period)
                .WithMany(s => s.Transactions)
                .HasForeignKey(c => c.PeriodId);
        }
    }
}
