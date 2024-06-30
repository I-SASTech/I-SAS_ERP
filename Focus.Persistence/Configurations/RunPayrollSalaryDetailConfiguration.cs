using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class RunPayrollSalaryDetailConfiguration : IEntityTypeConfiguration<RunPayrollSalaryDetail>
    {
        public void Configure(EntityTypeBuilder<RunPayrollSalaryDetail> builder)
        {
            builder.HasOne(s => s.RunPayrollDetail)
                .WithMany(ad => ad.RunPayrollSalaryDetails)
                .HasForeignKey(ad => ad.RunPayrollDetailId);
        }
    }
}
