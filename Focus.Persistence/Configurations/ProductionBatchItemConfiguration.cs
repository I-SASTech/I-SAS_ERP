using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ProductionBatchItemConfiguration : IEntityTypeConfiguration<ProductionBatchItem>
    {
        public void Configure(EntityTypeBuilder<ProductionBatchItem> builder)
        {

            builder.Property(x => x.Quantity);
            builder.Property(x => x.HighQuantity);
            builder.Property(x => x.Waste).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ProductionBatchId).IsRequired();

            builder.HasOne(s => s.ProductionBatch)
                .WithMany(ad => ad.ProductionBatchItems)
                .HasForeignKey(ad => ad.ProductionBatchId);

            builder.HasOne(s => s.Warehouse)
             .WithMany(ad => ad.ProductionBatchItems)
             .HasForeignKey(ad => ad.WarehouseId);


        }
    }
}
