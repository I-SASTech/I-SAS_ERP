using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseOrderPaymentConfiguration : IEntityTypeConfiguration<PurchaseOrderPayment>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderPayment> builder)
        {

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchaseOrderPayments)
                .HasForeignKey(ad => ad.PurchaseOrderId);

            builder.HasOne(s => s.PaymentVoucher)
                .WithMany(ad => ad.PurchaseOrderPayments)
                .HasForeignKey(ad => ad.PaymentVoucherId);
        }
    }
}
