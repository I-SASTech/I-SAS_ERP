using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PriceRecordConfiguration : IEntityTypeConfiguration<PriceRecord>
   {
       public void Configure(EntityTypeBuilder<PriceRecord> builder)
       {

            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,4)");
            builder.Property(x => x.CostPrice).HasColumnType("decimal(18,4)");
            builder.Property(x => x.SalePrice).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Price).HasColumnType("decimal(18,4)");
            builder.Property(x => x.PurchasePrice).HasColumnType("decimal(18,4)");

            builder.HasOne(ed => ed.PriceLabeling)
               .WithMany(d => d.PriceRecords)
               .HasForeignKey(f => f.PriceLabelingId);
           builder.HasOne(ed => ed.Product)
               .WithMany(d => d.PriceRecords)
               .HasForeignKey(f => f.ProductId);

         

          
       }
   }
}
