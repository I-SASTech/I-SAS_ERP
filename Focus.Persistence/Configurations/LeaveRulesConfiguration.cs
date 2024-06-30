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
    public class LeaveRulesConfiguration : IEntityTypeConfiguration<LeaveRules>
    {
        public void Configure(EntityTypeBuilder<LeaveRules> builder)
        {
            builder.HasOne(x => x.LeaveTypes)
                .WithMany(x => x.LeaveRules)
                .HasForeignKey(x => x.LeaveTypeId);

            builder.HasOne(x => x.LeaveGroups)
                .WithMany(x=>x.LeaveRules)
                .HasForeignKey(x => x.LeaveGroupId);

            builder.HasOne(x => x.Designations)
               .WithMany(x => x.LeaveRules)
               .HasForeignKey(x => x.DesignationId);

            builder.HasOne(x => x.EmployeeRegistration)
              .WithMany(x => x.LeaveRules)
              .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.Departments)
             .WithMany(x => x.LeaveRules)
             .HasForeignKey(x => x.DepartmentId);

            builder.HasOne(x => x.LeavePeriods)
             .WithMany(x => x.LeaveRules)
             .HasForeignKey(x => x.LeavePeriodId);
        }
    }
}
