using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class RunPayrollDetailConfiguration : IEntityTypeConfiguration<RunPayrollDetail>
    {
        public void Configure(EntityTypeBuilder<RunPayrollDetail> builder)
        {
            builder.HasOne(s => s.RunPayroll)
                .WithMany(ad => ad.RunPayrollDetails)
                .HasForeignKey(ad => ad.RunPayrollId);
        }
    }
}
