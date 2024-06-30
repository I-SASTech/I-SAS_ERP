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
    public class DeliveryChallanConfiguration : IEntityTypeConfiguration<DeliveryChallan>
    {
        public void Configure(EntityTypeBuilder<DeliveryChallan> builder)
        {
            builder.HasOne(s => s.SaleOrder)
                 .WithMany(ad => ad.DeliveryChallansForSaleOrders)
                 .HasForeignKey(ad => ad.SaleOrderId);
            builder.HasOne(s => s.SaleInvoice)
               .WithMany(ad => ad.DeliveryChallansForSales)
               .HasForeignKey(ad => ad.SaleInvoiceId);
            builder.HasOne(s => s.Contact)
             .WithMany(ad => ad.DeliveryChallans)
             .HasForeignKey(ad => ad.CustomerId);



        }
    }
}
