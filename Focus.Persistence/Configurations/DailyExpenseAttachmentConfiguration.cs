using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class DailyExpenseAttachmentConfiguration : IEntityTypeConfiguration<DailyExpenseAttachment>
   {
       public void Configure(EntityTypeBuilder<DailyExpenseAttachment> builder)
       {
           builder.Property(a => a.Name);
           builder.Property(a => a.Description).HasMaxLength(150);
           builder.Property(a => a.Path);


            builder.HasOne(s => s.DailyExpense)
               .WithMany(ad => ad.DailyExpenseAttachments)
               .HasForeignKey(ad => ad.DailyExpenseId);


       }
   }
}
