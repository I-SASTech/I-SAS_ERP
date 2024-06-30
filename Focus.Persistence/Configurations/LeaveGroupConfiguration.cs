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
    public class LeaveGroupConfiguration : IEntityTypeConfiguration<LeaveGroup>
    {
        public void Configure(EntityTypeBuilder<LeaveGroup> builder)
        {

        }
    }
}
