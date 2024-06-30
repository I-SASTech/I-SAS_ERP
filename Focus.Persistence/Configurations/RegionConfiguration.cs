using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    public class RegionConfigration : IEntityTypeConfiguration<Region>
    {

        public void Configure(EntityTypeBuilder<Region> builder)
        {


            builder.Property(d => d.Code).HasMaxLength(150);
            builder.Property(d => d.CountryId).HasMaxLength(150);
            builder.Property(d => d.CityId).HasMaxLength(150);
            builder.Property(d => d.StateId).HasMaxLength(150);
            builder.Property(d => d.Area).HasMaxLength(150);
          
            builder.HasOne(d => d.City).WithMany(d => d.Regions).HasForeignKey(d => d.CityId);
          
        }
    }

}

