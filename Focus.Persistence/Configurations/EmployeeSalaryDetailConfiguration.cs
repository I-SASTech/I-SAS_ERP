using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class EmployeeSalaryDetailConfiguration : IEntityTypeConfiguration<EmployeeSalaryDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalaryDetail> builder)
        {
            builder.HasOne(ea => ea.EmployeeSalary)
                .WithMany(a => a.EmployeeSalaryDetails)
                .HasForeignKey(ea => ea.EmployeeSalaryId);
        }
    }
}
