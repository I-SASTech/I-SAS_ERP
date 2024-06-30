using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class EmployeeSalaryConfiguration : IEntityTypeConfiguration<EmployeeSalary>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalary> builder)
        {
            builder.HasOne(j => j.Employee)
                .WithMany(j => j.EmployeeSalaries)
                .HasForeignKey(j => j.EmployeeId);
        }
    }
}
