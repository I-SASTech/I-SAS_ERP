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
    internal class LoginPermissionConfiguration : IEntityTypeConfiguration<LoginPermissions>
    {
        public void Configure(EntityTypeBuilder<LoginPermissions> builder)
        {
            builder.Property(x=>x.IsOverAllAccess).HasDefaultValue(true);
            builder.Property(x=>x.AllowAll).HasDefaultValue(true);

        }
    }
}
