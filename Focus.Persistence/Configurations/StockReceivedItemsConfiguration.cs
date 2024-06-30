using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class StockReceivedItemsConfiguration : IEntityTypeConfiguration<StockReceivedItems>
    {
        public void Configure(EntityTypeBuilder<StockReceivedItems> builder)
        {
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Amount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.TransferAmount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.RemainingQuantity).HasColumnType("decimal(18,4)");
            builder.Property(x => x.ReceivedQuantity).HasColumnType("decimal(18,4)");
            builder.Property(x => x.lineTotal).HasColumnType("decimal(18,4)");

            builder.HasOne<Product>(s => s.Product)
               .WithMany(ad => ad.StockReceivedItems)
               .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.StockReceived)
                .WithMany(ad => ad.StockReceivedItems)
                .HasForeignKey(ad => ad.StockReceivedId);


        }
    }
}