using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PaymentVoucherDetailConfiguration : IEntityTypeConfiguration<PaymentVoucherDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentVoucherDetail> builder)
        {
            builder.Property(j => j.Description).HasMaxLength(200);
            builder.Property(j => j.Description);
            builder.Property(j => j.Date).IsRequired();
            builder.Property(j => j.PaymentVoucherId).IsRequired();
            builder.Property(j => j.ContactAccountId).IsRequired();
            builder.Property(j => j.Amount).IsRequired().HasColumnType("decimal(18,4)");

           

        
        }
    }
}
