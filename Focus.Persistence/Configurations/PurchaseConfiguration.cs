using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.PurchaseOrderNo);
            builder.Property(x => x.InvoiceNo);
            builder.Property(x => x.InvoiceDate);
            builder.Property(x => x.WareHouseId).IsRequired();
            builder.Property(x => x.InvoiceFixDiscount).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.Supplier)
                .WithMany(ad => ad.Purchases)
                .HasForeignKey(ad => ad.SupplierId).IsRequired();

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.Purchases)
                .HasForeignKey(ad => ad.PurchaseOrderId);
        }
    }
}
