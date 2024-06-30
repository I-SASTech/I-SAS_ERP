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
    public class LeaveGroupEmployeeConfiguration : IEntityTypeConfiguration<LeaveGroupEmployee>
    {
        public void Configure(EntityTypeBuilder<LeaveGroupEmployee> builder)
        {
            builder.HasOne(x => x.EmployeeRegistration)
                .WithMany(x => x.LeaveGroupEmployees)
                .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.LeaveGroups)
                .WithMany(x => x.LeaveGroupEmployees)
                .HasForeignKey(x => x.LeaveGroupId);
        }
    }
}
