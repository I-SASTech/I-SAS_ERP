using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class WareHouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.Property(d => d.LicenseExpiry).IsRequired(false);
            builder.Property(d => d.CivilDefenceLicenseExpiry).IsRequired(false);
            builder.Property(d => d.CCTVLicenseExpiry).IsRequired(false);
          

           
        }
    }
}
