using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ProcessItemConfiguration : IEntityTypeConfiguration<ProcessItem>
   {
       public void Configure(EntityTypeBuilder<ProcessItem> builder)
       {



           builder.HasOne(s => s.Process)
               .WithMany(ad => ad.ProcessItems)
               .HasForeignKey(ad => ad.ProcessId);
           builder.HasOne(s => s.Product)
               .WithMany(ad => ad.ProcessItems)
               .HasForeignKey(ad => ad.ProductId);




        }
   }
}
