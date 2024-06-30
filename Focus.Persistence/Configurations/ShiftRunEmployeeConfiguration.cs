using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ShiftRunEmployeeConfiguration : IEntityTypeConfiguration<ShiftRunEmployee>
    {
        public void Configure(EntityTypeBuilder<ShiftRunEmployee> builder)
        {
            builder.HasOne(s => s.Employee)
                .WithMany(ad => ad.ShiftRunEmployees)
                .HasForeignKey(ad => ad.EmployeeId);

            builder.HasOne(s => s.ShiftRun)
                .WithMany(ad => ad.ShiftRunEmployees)
                .HasForeignKey(ad => ad.ShiftRunId);
        }
    }
}
