using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class BatchProcessContractorConfiguration : IEntityTypeConfiguration<BatchProcessContractor>
   {
       public void Configure(EntityTypeBuilder<BatchProcessContractor> builder)
       {

           
           builder.HasOne(s => s.BatchProcess)
               .WithMany(ad => ad.BatchProcessContractors)
               .HasForeignKey(ad => ad.BatchProcessId);
           builder.HasOne(s => s.Contractor)
               .WithMany(ad => ad.BatchProcessContractors)
               .HasForeignKey(ad => ad.ContractorId);

        }
   }
}
