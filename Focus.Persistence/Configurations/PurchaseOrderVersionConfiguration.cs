using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseOrderVersionConfiguration : IEntityTypeConfiguration<PurchaseOrderVersion>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderVersion> builder)
        {
            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchaseOrderVersions)
                .HasForeignKey(ad => ad.PurchaseOrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
