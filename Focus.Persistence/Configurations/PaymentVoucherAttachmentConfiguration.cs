using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public  class PaymentVoucherAttachmentConfiguration : IEntityTypeConfiguration<PaymentVoucherAttachment>
    {
        public void Configure(EntityTypeBuilder<PaymentVoucherAttachment> builder)
        {
            builder.Property(a => a.Name);
            builder.Property(a => a.Description).HasMaxLength(300);
            builder.Property(a => a.Path);

            builder.HasOne(s => s.PaymentVoucher)
                .WithMany(ad => ad.PaymentVoucherAttachments)
                .HasForeignKey(ad => ad.PaymentVoucherId);

            builder.HasOne(s => s.PurchaseOrderExpense)
                .WithMany(ad => ad.PaymentVoucherAttachments)
                .HasForeignKey(ad => ad.PurchaseOrderExpenseId);


        }
    }
}
