using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BatchProcessConfiguration : IEntityTypeConfiguration<BatchProcess>
    {
        public void Configure(EntityTypeBuilder<BatchProcess> builder)
        {

            builder.HasOne(s => s.Warehouse)
                .WithMany(ad => ad.BatchProcesses)
                .HasForeignKey(ad => ad.WarehouseId);
            builder.HasOne(s => s.ProductionBatch)
                .WithMany(ad => ad.BatchProcesses)
                .HasForeignKey(ad => ad.ProductionBatchId);

        }
    }
}
