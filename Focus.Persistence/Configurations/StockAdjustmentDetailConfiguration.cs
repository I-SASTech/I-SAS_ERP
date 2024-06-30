using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class StockAdjustmentDetailConfiguration : IEntityTypeConfiguration<StockAdjustmentDetail>
    {
        public void Configure(EntityTypeBuilder<StockAdjustmentDetail> builder)
        {
            builder.Property(j => j.Quantity).HasMaxLength(5);
            builder.Property(j => j.Price).IsRequired();
            builder.Property(j => j.ProductId).IsRequired();
            builder.Property(j => j.Quantity).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(j => j.Price).IsRequired().HasColumnType("decimal(18,4)");

            builder.HasOne(a => a.Warehouse)
                .WithMany(j => j.StockAdjustmentDetails)
                .HasForeignKey(a => a.WarehouseId);

            builder.HasOne(a => a.StockAdjustments)
                .WithMany(j => j.StockAdjustmentDetails)
                .HasForeignKey(a => a.StockAdjustmentId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.WarrantyType)
                .WithMany(j => j.StockAdjustmentDetails)
                .HasForeignKey(a => a.WarrantyTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
