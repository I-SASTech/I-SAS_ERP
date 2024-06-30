using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ProcessContractorConfiguration : IEntityTypeConfiguration<ProcessContractor>
   {
       public void Configure(EntityTypeBuilder<ProcessContractor> builder)
       {



           builder.HasOne(s => s.Process)
               .WithMany(ad => ad.ProcessContractors)
               .HasForeignKey(ad => ad.ProcessId);
           builder.HasOne(s => s.Contractor)
               .WithMany(ad => ad.ProcessContractors)
               .HasForeignKey(ad => ad.ContractorId);





        }
   }
}
