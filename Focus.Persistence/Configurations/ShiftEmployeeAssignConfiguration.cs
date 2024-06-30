using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ShiftEmployeeAssignConfiguration : IEntityTypeConfiguration<ShiftEmployeeAssign>
    {
        public void Configure(EntityTypeBuilder<ShiftEmployeeAssign> builder)
        {
            builder.HasOne(s => s.Employee)
                .WithMany(ad => ad.ShiftEmployeeAssigns)
                .HasForeignKey(ad => ad.EmployeeId);

            builder.HasOne(s => s.ShiftAssign)
                .WithMany(ad => ad.ShiftEmployeeAssigns)
                .HasForeignKey(ad => ad.ShiftAssignId);
        }
    }
}
