using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ShiftRunConfiguration : IEntityTypeConfiguration<ShiftRun>
    {
        public void Configure(EntityTypeBuilder<ShiftRun> builder)
        {
            builder.HasOne(s => s.ShiftAssign)
                .WithMany(ad => ad.ShiftRuns)
                .HasForeignKey(ad => ad.ShiftAssignId);
        }
    }
}
