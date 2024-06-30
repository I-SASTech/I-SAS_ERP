using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashReturnConfiguration : IEntityTypeConfiguration<TemporaryCashReturn>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashReturn> builder)
        {
            builder.HasOne(s => s.TemporaryCashIssue)
                .WithMany(ad => ad.TemporaryCashReturns)
                .HasForeignKey(ad => ad.TemporaryCashIssueId);
        }
    }
}
