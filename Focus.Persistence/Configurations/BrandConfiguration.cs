using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            //builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
            //builder.Property(d => d.Description).HasMaxLength(150);
            //builder.Property(d => d.Code).HasMaxLength(30);

            //builder.HasMany(ed => ed.EmployeeDepartments)
            //    .WithOne(d => d.Department)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
