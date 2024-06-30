using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ManualAttendanceConfiguration : IEntityTypeConfiguration<ManualAttendance>
    {
        public void Configure(EntityTypeBuilder<ManualAttendance> builder)
        {
            builder.HasOne(s => s.EmployeeRegistration)
                .WithMany(ad => ad.ManualAttendances)
                .HasForeignKey(ad => ad.EmployeeId);
        }
    }
}
