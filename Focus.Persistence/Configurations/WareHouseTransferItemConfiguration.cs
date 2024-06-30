using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class WareHouseTransferItemConfiguration : IEntityTypeConfiguration<WareHouseTransferItem>
   {
       public void Configure(EntityTypeBuilder<WareHouseTransferItem> builder)
       {
           builder.Property(x => x.Quantity).HasColumnType("decimal(18,4)");

            builder.HasOne<Product>(s => s.Product)
               .WithMany(ad => ad.WareHouseTransferItems)
               .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.WareHouseTransfer)
                .WithMany(ad => ad.WareHouseTransferItems)
                .HasForeignKey(ad => ad.WareHouseTransferId);


        }
   }
}
