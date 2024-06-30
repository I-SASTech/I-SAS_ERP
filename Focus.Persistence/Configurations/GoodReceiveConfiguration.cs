using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class GoodReceiveConfiguration : IEntityTypeConfiguration<GoodReceiveNote>
    {
        public void Configure(EntityTypeBuilder<GoodReceiveNote> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.InvoiceNo);
            builder.Property(x => x.InvoiceDate);

            builder.HasOne(s => s.Supplier)
                .WithMany(ad => ad.GoodReceiveNotes)
                .HasForeignKey(ad => ad.SupplierId);

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.GoodReceiveNotes)
                .HasForeignKey(ad => ad.PurchaseOrderId);




        }
    }
}
