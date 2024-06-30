using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InventoryBlindDetailConfiguration:IEntityTypeConfiguration<InventoryBlindDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryBlindDetail> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany(x => x.InventoryBlindDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.InventoryBlind)
                .WithMany(x => x.InventoryBlindDetails)
                .HasForeignKey(x => x.InventoryBlindId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
