using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class NobleRolesConfiguration : IEntityTypeConfiguration<NobleRoles>
    {
        public void Configure(EntityTypeBuilder<NobleRoles> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
            builder.Property(d => d.NormalizedName).IsRequired().HasMaxLength(50);
        }
    }
}
