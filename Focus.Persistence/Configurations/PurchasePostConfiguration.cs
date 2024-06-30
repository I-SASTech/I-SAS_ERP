using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchasePostConfiguration : IEntityTypeConfiguration<PurchasePost>
    {
        public void Configure(EntityTypeBuilder<PurchasePost> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.PurchaseOrderNo);
            builder.Property(x => x.InvoiceNo);
            builder.Property(x => x.InvoiceDate);
            builder.Property(x => x.WareHouseId).IsRequired();
            builder.Property(x => x.InvoiceFixDiscount).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.Supplier)
                .WithMany(ad => ad.PurchasePosts)
                .HasForeignKey(ad => ad.SupplierId).IsRequired();

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchasePosts)
                .HasForeignKey(ad => ad.PurchaseOrderId);
            builder.HasOne(s => s.GoodReceiveNote)
                .WithMany(ad => ad.PurchasePosts)
                .HasForeignKey(ad => ad.GoodReceiveNoteId);
        }
    }
}
