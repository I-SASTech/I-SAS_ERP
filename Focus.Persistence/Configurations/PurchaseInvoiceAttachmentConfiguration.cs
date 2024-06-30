using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseInvoiceAttachmentConfiguration : IEntityTypeConfiguration<PurchaseInvoiceAttachment>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceAttachment> builder)
        {
            builder.HasOne(s => s.Purchase)
                .WithMany(ad => ad.PurchaseInvoiceAttachments)
                .HasForeignKey(ad => ad.PurchaseInvoiceId);

            builder.HasOne(s => s.PurchasePost)
                .WithMany(ad => ad.PurchaseInvoiceAttachments)
                .HasForeignKey(ad => ad.PurchaseInvoicePostId);
        }
    }
}
