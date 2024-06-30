using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InventoryBlindConfiguration:IEntityTypeConfiguration<InventoryBlind>
    {
        public void Configure(EntityTypeBuilder<InventoryBlind> builder)
        {
            builder.Property(x => x.DocumentId).IsRequired().HasMaxLength(30);
            builder.HasOne(x => x.Warehouse)
                .WithMany(x => x.InventoryBlinds)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
