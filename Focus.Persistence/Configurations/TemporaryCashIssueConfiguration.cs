using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashIssueConfiguration : IEntityTypeConfiguration<TemporaryCashIssue>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashIssue> builder)
        {
            builder.HasOne(ed => ed.TemporaryCashRequest)
                .WithMany(d => d.TemporaryCashIssues)
                .HasForeignKey(d => d.TemporaryCashRequestId);
        }
    }
}
