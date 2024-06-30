using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BatchCostingItemConfiguration : IEntityTypeConfiguration<BatchCostingItem>
    {
        public void Configure(EntityTypeBuilder<BatchCostingItem> builder)
        {
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.BatchCosting)
                .WithMany(ad => ad.BatchCostingItems)
                .HasForeignKey(ad => ad.BatchCostingId);

        }
    }
}
