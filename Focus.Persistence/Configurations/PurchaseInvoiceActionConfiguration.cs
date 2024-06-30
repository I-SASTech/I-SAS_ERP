using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseInvoiceActionConfiguration : IEntityTypeConfiguration<PurchaseInvoiceAction>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceAction> builder)
        {
            builder.HasOne(s => s.Purchase)
                .WithMany(ad => ad.PurchaseInvoiceActions)
                .HasForeignKey(ad => ad.PurchaseInvoiceId);

            builder.HasOne(s => s.CompanyProcess)
                .WithMany(ad => ad.PurchaseInvoiceActions)
                .HasForeignKey(ad => ad.ProcessId);

            builder.HasOne(s => s.PurchasePost)
                .WithMany(ad => ad.PurchaseInvoiceActions)
                .HasForeignKey(ad => ad.PurchaseInvoicePostId);
        }
    }
}
