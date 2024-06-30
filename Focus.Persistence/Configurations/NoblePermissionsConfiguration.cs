using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class NoblePermissionsConfiguration : IEntityTypeConfiguration<NoblePermission>
    {
        public void Configure(EntityTypeBuilder<NoblePermission> builder)
        {
            builder.HasOne(a => a.NobleModules)
                .WithMany(j => j.NoblePermissions)
                .HasForeignKey(a => a.NobleModuleId);
        }
    }
}
