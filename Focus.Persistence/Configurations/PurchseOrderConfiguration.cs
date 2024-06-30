
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PurchseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.InvoiceNo);
            builder.Property(x => x.InvoiceDate);

            builder.HasOne(s => s.Supplier)
                .WithMany(ad => ad.PurchaseOrders)
                .HasForeignKey(ad => ad.SupplierId);
        }
    }
}
