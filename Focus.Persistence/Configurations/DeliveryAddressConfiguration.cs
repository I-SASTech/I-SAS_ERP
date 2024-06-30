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
    public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder.HasOne(ea => ea.Contact)
                .WithMany(a => a.DeliveryAddresses)
                .HasForeignKey(ea => ea.ContactId);
           

        }
    }
}
