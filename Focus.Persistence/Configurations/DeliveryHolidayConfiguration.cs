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
    public class DeliveryHolidayConfiguration : IEntityTypeConfiguration<DeliveryHoliday>
    {
        public void Configure(EntityTypeBuilder<DeliveryHoliday> builder)
        {
           
            builder.HasOne(ea => ea.DeliveryAddress)
              .WithMany(a => a.DeliveryHolidays)
              .HasForeignKey(ea => ea.DeliveryAddressId);
            builder.HasOne(ea => ea.WeekHoliday)
              .WithMany(a => a.DeliveryHolidays)
              .HasForeignKey(ea => ea.WeekHolidayId);

        }
    }
}
