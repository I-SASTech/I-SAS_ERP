using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Configurations
{
    public class LeaderAccountConfiguration : IEntityTypeConfiguration<LedgerAccount>
    {
        public void Configure(EntityTypeBuilder<LedgerAccount> builder)
        {

            builder.HasOne(j => j.Account)
                .WithMany(j => j.LeadgerAccounts)
                .HasForeignKey(j => j.AccountId);



        }
    }
}
