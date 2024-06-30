using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class PaidTimeOffConfiguration : IEntityTypeConfiguration<PaidTimeOff>
    {
        public void Configure(EntityTypeBuilder<PaidTimeOff> builder)
        {
            builder.HasOne(x => x.EmployeeRegistration)
                .WithMany(x => x.PaidLeaveOffs)
                .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.LeavePeriods)
                .WithMany(x => x.PaidLeaveOffs)
                .HasForeignKey(x => x.LeavePeriodId);

            builder.HasOne(x => x.LeaveTypes)
                .WithMany(x => x.PaidLeaveOffs)
                .HasForeignKey(x => x.LeaveType);
        }
    }
}
