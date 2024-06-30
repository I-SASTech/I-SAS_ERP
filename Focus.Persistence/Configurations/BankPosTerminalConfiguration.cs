using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class BankPosTerminalConfiguration : IEntityTypeConfiguration<BankPosTerminal>
   {
       public void Configure(EntityTypeBuilder<BankPosTerminal> builder)
       {
           builder.HasOne(ea => ea.Account)
               .WithMany(a => a.BankPosTerminals)
               .HasForeignKey(ea => ea.BankId);

       }
   }
}

