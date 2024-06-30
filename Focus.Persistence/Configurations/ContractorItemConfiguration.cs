using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ContractorItemConfiguration : IEntityTypeConfiguration<ContractorItem>
   {
       public void Configure(EntityTypeBuilder<ContractorItem> builder)
       {



           builder.HasOne(s => s.BatchProcessContractor)
               .WithMany(ad => ad.ContractorItems)
               .HasForeignKey(ad => ad.BatchProcessContractorId);
           builder.HasOne(s => s.Product)
               .WithMany(ad => ad.ContractorItems)
               .HasForeignKey(ad => ad.ProductId);




        }
   }
}
