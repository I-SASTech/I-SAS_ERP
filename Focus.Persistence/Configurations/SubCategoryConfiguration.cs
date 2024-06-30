using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(150);
            builder.Property(d => d.Code).HasMaxLength(30);

            builder.HasOne(ed => ed.Category)
                .WithMany(d => d.SubCategories)
                .HasForeignKey(d=> d.CategoryId);
        }
    }
}
