using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PurchaseBillItemConfiguration : IEntityTypeConfiguration<PurchaseBillItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseBillItem> builder)
        {
            

            builder.HasOne(s => s.Account)
                .WithMany(ad => ad.PurchaseBillItems)
                .HasForeignKey(ad => ad.AccountId);
            
            builder.HasOne(s => s.PurchaseBill)
                .WithMany(ad => ad.PurchaseBillItems)
                .HasForeignKey(ad => ad.PurchaseBillId);
        }
    }
}
