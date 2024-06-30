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
    public class DeliveryChallanItemConfiguration : IEntityTypeConfiguration<DeliveryChallanItem>
    {
        public void Configure(EntityTypeBuilder<DeliveryChallanItem> builder)
        {
            builder.HasOne(s => s.DeliveryChallan)
                 .WithMany(ad => ad.DeliveryChallanItems)
                 .HasForeignKey(ad => ad.DeliveryChallanId);
            builder.HasOne(s => s.Product)
               .WithMany(ad => ad.DeliveryChallanItems)
               .HasForeignKey(ad => ad.ProductId);



        }
    }
}
