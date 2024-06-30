using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashAllocationConfiguration : IEntityTypeConfiguration<TemporaryCashAllocation>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashAllocation> builder)
        {
            builder.HasOne(a => a.Account)
                .WithMany(x => x.TemporaryCashAllocations)
                .HasForeignKey(x => x.BankCashAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
