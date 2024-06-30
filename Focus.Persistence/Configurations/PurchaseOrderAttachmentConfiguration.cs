using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PurchaseOrderAttachmentConfiguration : IEntityTypeConfiguration<PurchaseAttachment>
   {
       public void Configure(EntityTypeBuilder<PurchaseAttachment> builder)
       {
           builder.Property(a => a.Name);
           builder.Property(a => a.Description).HasMaxLength(150);
           builder.Property(a => a.Path);

           builder.HasOne(s => s.PurchaseOrder)
               .WithMany(ad => ad.PurchaseAttachments)
               .HasForeignKey(ad => ad.PurchaseOrderId);


        }
    }
}
