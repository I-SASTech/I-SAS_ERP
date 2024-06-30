using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    public class ImportExportItemConfiguration : IEntityTypeConfiguration<ImportExportItem>
    {
        public void Configure(EntityTypeBuilder<ImportExportItem> builder)
        {


            builder.HasOne(ed => ed.Service)
                .WithMany(d => d.Services)
                .HasForeignKey(d => d.ServiceId);

            builder.HasOne(ed => ed.StuffingLocation)
              .WithMany(d => d.StuffingLocations)
              .HasForeignKey(d => d.StuffingLocationId);

            builder.HasOne(ed => ed.PortOfLoading)
             .WithMany(d => d.PortOfLoadings)
             .HasForeignKey(d => d.PortOfLoadingId);

            builder.HasOne(ed => ed.PortOfDestination)
            .WithMany(d => d.PortOfDestinations)
            .HasForeignKey(d => d.PortOfDestinationId);

            builder.HasOne(ed => ed.Carrier)
          .WithMany(d => d.Carriers)
          .HasForeignKey(d => d.CarrierId);
            builder.HasOne(ed => ed.SaleOrder)
       .WithMany(d => d.ImportExportItems)
       .HasForeignKey(d => d.SaleOrderId);


        }
    }
}
