using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class BomSaleOrderItemConfiguration : IEntityTypeConfiguration<BomSaleOrderItems>
    {
        public void Configure(EntityTypeBuilder<BomSaleOrderItems> builder)
        {

            builder.HasOne(s => s.Boms)
                .WithMany(ad => ad.BomSaleOrderItems)
                .HasForeignKey(ad => ad.BomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Products)
              .WithMany(ad => ad.BomSaleOrderItems)
              .HasForeignKey(ad => ad.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
