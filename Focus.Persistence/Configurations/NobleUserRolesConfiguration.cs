using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class NobleUserRolesConfiguration : IEntityTypeConfiguration<NobleUserRoles>
    {
        public void Configure(EntityTypeBuilder<NobleUserRoles> builder)
        {
            builder.Property(d => d.RoleId).IsRequired();

            builder.HasOne(a => a.Roles)
               .WithMany(j => j.NobleUserRoles)
               .HasForeignKey(a => a.RoleId);
        }
    }
}
