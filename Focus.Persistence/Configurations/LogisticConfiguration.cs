using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class LogisticConfiguration : IEntityTypeConfiguration<Logistic>
   {
       public void Configure(EntityTypeBuilder<Logistic> builder)
       {
           builder.Property(d => d.Address).HasMaxLength(500);

           builder.HasOne(ed => ed.Account)
               .WithMany(d => d.Logistics)
               .HasForeignKey(f => f.ClearanceAgent);

         
       }
   }
}
