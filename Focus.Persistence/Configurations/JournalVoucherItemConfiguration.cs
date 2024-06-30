using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class JournalVoucherItemConfiguration : IEntityTypeConfiguration<JournalVoucherItem>
    {
        public void Configure(EntityTypeBuilder<JournalVoucherItem> builder)
        {
            builder.Property(j => j.Description).HasMaxLength(200);
            builder.Property(j => j.Description).IsRequired(false);
            
            builder.Property(j => j.Debit);
            builder.Property(j => j.Credit);

            builder.HasOne(a => a.Account)
                .WithMany(j => j.JournalVoucherItems)
                .HasForeignKey(a => a.AccountId);

            builder.HasOne(j => j.JournalVoucher)
                .WithMany(j => j.JournalVoucherItems)
                .HasForeignKey(j => j.JournalVoucherId);

            builder.HasOne(j => j.Contact)
                .WithMany(j => j.JournalVoucherItems)
                .HasForeignKey(j => j.ContactId);

           
        }
    }
}
