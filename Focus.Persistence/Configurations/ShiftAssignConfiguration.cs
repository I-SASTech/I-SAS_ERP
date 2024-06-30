using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ShiftAssignConfiguration : IEntityTypeConfiguration<ShiftAssign>
    {
        public void Configure(EntityTypeBuilder<ShiftAssign> builder)
        {
            builder.HasOne(s => s.Employee)
                .WithMany(ad => ad.ShiftAssigns)
                .HasForeignKey(ad => ad.EmployeeId);
        }
    }
}
