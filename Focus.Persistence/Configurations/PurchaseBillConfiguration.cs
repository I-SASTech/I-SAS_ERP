using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class PurchaseBillConfiguration : IEntityTypeConfiguration<PurchaseBill>
  {
      public void Configure(EntityTypeBuilder<PurchaseBill> builder)
      {
          builder.Property(x => x.Date).IsRequired();
          builder.Property(x => x.RegistrationNo).IsRequired();
          builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,4)");
          builder.Property(x => x.RemainingAmount).HasColumnType("decimal(18,4)");


            builder.HasOne(s => s.BillerAccount)
              .WithMany(ad => ad.PurchaseBills)
              .HasForeignKey(ad => ad.BillerId);
      }
  }
}
