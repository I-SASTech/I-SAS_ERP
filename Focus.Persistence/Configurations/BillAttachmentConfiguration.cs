using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BillAttachmentConfiguration : IEntityTypeConfiguration<BillAttachment>
    {
        public void Configure(EntityTypeBuilder<BillAttachment> builder)
        {
           

            builder.HasOne(s => s.PurchaseBill)
                .WithMany(ad => ad.BillAttachments)
                .HasForeignKey(ad => ad.PurchaseBillId);
        }
    }
}
