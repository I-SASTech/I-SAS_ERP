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
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveTypes>
    {
        public void Configure(EntityTypeBuilder<LeaveTypes> builder)
        {
            builder.HasOne(x => x.LeaveGroups)
                .WithMany(x => x.LeaveTypes)
                .HasForeignKey(x => x.LeaveGroupId);
        }
    }
}
