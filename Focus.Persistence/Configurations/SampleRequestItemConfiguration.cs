using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class SampleRequestItemConfiguration : IEntityTypeConfiguration<SampleRequestItem>
   {
       public void Configure(EntityTypeBuilder<SampleRequestItem> builder)
       {
           builder.Property(x => x.SampleRequestId).IsRequired();
           builder.HasOne(s => s.Product)
               .WithMany(ad => ad.SampleRequestItems)
               .HasForeignKey(ad => ad.ProductId);

           builder.HasOne(s => s.SampleRequests)
               .WithMany(ad => ad.SampleRequestItems)
               .HasForeignKey(ad => ad.SampleRequestId).OnDelete(DeleteBehavior.Restrict);
       }
   }
}
