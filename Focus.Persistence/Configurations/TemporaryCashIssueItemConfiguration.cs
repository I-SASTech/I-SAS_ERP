using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashIssueItemConfiguration : IEntityTypeConfiguration<TemporaryCashIssueItem>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashIssueItem> builder)
        {
            builder.HasOne(ed => ed.TemporaryCashIssue)
                .WithMany(d => d.TemporaryCashIssueItems)
                .HasForeignKey(d => d.TemporaryCashIssueId);
        }
    }
}
