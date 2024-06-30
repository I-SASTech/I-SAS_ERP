using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class GatePassItemConfiguration : IEntityTypeConfiguration<GatePassItem>
   {
       public void Configure(EntityTypeBuilder<GatePassItem> builder)
       {
           builder.HasOne(s => s.GatePass)
               .WithMany(ad => ad.GatePassItems)
               .HasForeignKey(ad => ad.GatePassId);
       }
   }
}
