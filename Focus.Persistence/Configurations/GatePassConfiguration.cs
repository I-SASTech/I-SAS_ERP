using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class GatePassConfiguration : IEntityTypeConfiguration<GatePass>
  {
      public void Configure(EntityTypeBuilder<GatePass> builder)
      {

          builder.HasOne(s => s.EmployeeRegistration)
              .WithMany(ad => ad.GatePasses)
              .HasForeignKey(ad => ad.EmployeeId);

          builder.HasOne(s => s.ProductionBatch)
              .WithMany(ad => ad.GatePasses)
              .HasForeignKey(ad => ad.ProductionBatchId);
      }
  }
}
