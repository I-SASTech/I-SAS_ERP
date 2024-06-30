using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class StockAdjustmentConfiguration : IEntityTypeConfiguration<StockAdjustment>
    {
        public void Configure(EntityTypeBuilder<StockAdjustment> builder)
        {
            builder.Property(j => j.Narration).HasMaxLength(200);
            builder.Property(j => j.Narration).IsRequired(false);
            builder.Property(j => j.Date).IsRequired();
            builder.Property(j => j.WarehouseId).IsRequired();

            builder.HasOne(a => a.Warehouse)
                .WithMany(j => j.StockAdjustments)
                .HasForeignKey(a => a.WarehouseId);
            
            builder.HasOne(a => a.TaxRate)
                .WithMany(j => j.StockAdjustments)
                .HasForeignKey(a => a.TaxRateId);
        }
    }
}
