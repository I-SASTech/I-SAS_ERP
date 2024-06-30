using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseOrderActionConfiguration : IEntityTypeConfiguration<PurchaseOrderAction>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderAction> builder)
        {
            builder.Property(x => x.Description).IsRequired(false);

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchaseOrderActions)
                .HasForeignKey(ad => ad.PurchaseOrderId);

            builder.HasOne(s => s.CompanyProcess)
                .WithMany(ad => ad.PurchaseOrderActions)
                .HasForeignKey(ad => ad.ProcessId);
        }
    }
}
