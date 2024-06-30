using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class ReparingItemConfiguration : IEntityTypeConfiguration<ReparingItem>
    {
        public void Configure(EntityTypeBuilder<ReparingItem> builder)
        {


            builder.HasOne(ed => ed.Product)
                .WithMany(d => d.ReparingItems)
                .HasForeignKey(d => d.ProductId);
            builder.HasOne(ed => ed.ReparingOrder)
               .WithMany(d => d.ReparingItems)
               .HasForeignKey(d => d.ReparingOrderId);








        }
    }
}
