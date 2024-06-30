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
    public class PaymentVoucherItemConfiguration : IEntityTypeConfiguration<PaymentVoucherItem>
    {
        public void Configure(EntityTypeBuilder<PaymentVoucherItem> builder)
        {
            builder.Property(j => j.Amount).HasColumnType("decimal(18,4)");
            builder.Property(j => j.DueAmount).HasColumnType("decimal(18,4)");
            builder.Property(j => j.PayAmount).HasColumnType("decimal(18,4)");
            builder.Property(j => j.Total).HasColumnType("decimal(18,4)");
            builder.Property(j => j.ExtraAmount).HasColumnType("decimal(18,4)");
            builder.HasOne(a => a.PaymentVouchers)
                .WithMany(x => x.PaymentVoucherItems)
                .HasForeignKey(x => x.PaymentVoucherId);




        }
    }
}