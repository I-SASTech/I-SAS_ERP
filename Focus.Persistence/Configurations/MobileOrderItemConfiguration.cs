using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class MobileOrderItemConfiguration : IEntityTypeConfiguration<MobileOrderItem>
    {
        public void Configure(EntityTypeBuilder<MobileOrderItem> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.MobileOrder)
                .WithMany(ad => ad.MobileOrderItems)
                .HasForeignKey(ad => ad.MobileOrderId);

            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.MobileOrderItems)
                .HasForeignKey(ad => ad.ProductId);
        }
    }
}
