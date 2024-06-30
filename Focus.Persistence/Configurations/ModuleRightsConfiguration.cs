using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class ModuleRightsConfiguration : IEntityTypeConfiguration<ModulesRights>
    {
        public void Configure(EntityTypeBuilder<ModulesRights> builder)
        {
            builder.Property(d => d.Description).IsRequired();

            builder.HasOne(a => a.ModulesNames)
               .WithMany(j => j.ModulesRights)
               .HasForeignKey(a => a.ModuleId);
        }
    }
}
