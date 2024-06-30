using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SampleRequestConfiguration : IEntityTypeConfiguration<SampleRequest>
    {
        public void Configure(EntityTypeBuilder<SampleRequest> builder)
        {
           
            

            builder.HasOne(s => s.Customer)
                .WithMany(ad => ad.SampleRequests)
                .HasForeignKey(ad => ad.CustomerId);
            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.SampleRequests)
                .HasForeignKey(ad => ad.ProductId);
        }
    }
}
