using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class RunPayrollConfiguration : IEntityTypeConfiguration<RunPayroll>
    {
        public void Configure(EntityTypeBuilder<RunPayroll> builder)
        {
            builder.HasOne(s => s.PaySchedule)
                .WithMany(ad => ad.RunPayrolls)
                .HasForeignKey(ad => ad.PayrollScheduleId);
        }
    }
}
