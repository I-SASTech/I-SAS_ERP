using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class RolesPermissionsConfiguration : IEntityTypeConfiguration<RolesPermissions>
    {
        public void Configure(EntityTypeBuilder<RolesPermissions> builder)
        {
            builder.HasOne(a => a.Roles)
                .WithMany(j => j.UsersPermission)
                .HasForeignKey(a => a.RoleId);
        }
    }
}
