using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class ProductionStockTransferConfiguration : IEntityTypeConfiguration<ProductionStockTransfer>
    {
        public void Configure(EntityTypeBuilder<ProductionStockTransfer> builder)
        {
            builder.Property(x => x.DamageStock).HasColumnType("decimal(18,4)");
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,4)");
            builder.Property(x => x.RemainingStock).HasColumnType("decimal(18,4)");

        }
    }
}
