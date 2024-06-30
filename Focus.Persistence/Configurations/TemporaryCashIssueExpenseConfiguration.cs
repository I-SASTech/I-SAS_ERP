using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashIssueExpenseConfiguration : IEntityTypeConfiguration<TemporaryCashIssueExpense>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashIssueExpense> builder)
        {
            builder.HasOne(s => s.TemporaryCashIssue)
                .WithMany(ad => ad.TemporaryCashIssueExpenses)
                .HasForeignKey(ad => ad.TemporaryCashIssueId);
        }
    }
}
