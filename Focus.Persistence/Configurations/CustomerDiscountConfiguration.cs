using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CustomerDiscountConfiguration:IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.Property(e => e.DiscountName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Discount);
            builder.Property(e => e.ExtraDiscount);
            builder.Property(e => e.FreightDiscount);
            builder.Property(e => e.OtherDiscount);
            builder.Property(e => e.OpenDiscount);
        }
    }
}
