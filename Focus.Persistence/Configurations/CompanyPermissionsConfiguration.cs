using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CompanyPermissionsConfiguration : IEntityTypeConfiguration<CompanyPermission>
    {
        public void Configure(EntityTypeBuilder<CompanyPermission> builder)
        {
            builder.HasOne(a => a.NobleModules)
                .WithMany(j => j.CompanyPermissions)
                .HasForeignKey(a => a.NobleModuleId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.NobleGroup)
                .WithMany(j => j.CompanyPermissions)
                .HasForeignKey(a => a.NobleGroupId);
        }
    }
}
