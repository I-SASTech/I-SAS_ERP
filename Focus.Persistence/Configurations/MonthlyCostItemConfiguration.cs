using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
  public  class MonthlyCostItemConfiguration : IEntityTypeConfiguration<MonthlyCostItem>
    {
        public void Configure(EntityTypeBuilder<MonthlyCostItem> builder)
        {
            builder.HasOne(s => s.MonthlyCost)
                .WithMany(ad => ad.MonthlyCostItems)
                .HasForeignKey(ad => ad.MonthlyCostId);
            builder.Property(x => x.MonthlyCosts).HasColumnType("decimal(18,4)");
            builder.Property(x => x.YearlyCost).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Daily).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Total).HasColumnType("decimal(18,4)");
        }
    }
}
