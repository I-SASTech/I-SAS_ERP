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
    public class DeliveryChallanReservedItemConfiguration : IEntityTypeConfiguration<DeliveryChallanReserverdItem>
    {
        public void Configure(EntityTypeBuilder<DeliveryChallanReserverdItem> builder)
        {
            builder.HasOne(s => s.DeliveryChallanReserved)
                 .WithMany(ad => ad.DeliveryChallanReserverdItems)
                 .HasForeignKey(ad => ad.DeliveryChallanReservedId);
            builder.HasOne(s => s.Product)
               .WithMany(ad => ad.DeliveryChallanReserverdItems)
               .HasForeignKey(ad => ad.ProductId);



        }
    }
}
