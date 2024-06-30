using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CreditNoteConfiguration : IEntityTypeConfiguration<CreditNote>
    {
        public void Configure(EntityTypeBuilder<CreditNote> builder)
        {
            builder.HasOne(s => s.Contact)
                .WithMany(ad => ad.CreditNotes)
                .HasForeignKey(ad => ad.ContactId);

            builder.HasOne(s => s.Sale)
                .WithMany(ad => ad.CreditNotes)
                .HasForeignKey(ad => ad.SaleId);

            builder.HasOne(s => s.PurchasePost)
                .WithMany(ad => ad.CreditNotes)
                .HasForeignKey(ad => ad.PurchasePostId);

        }
    }
}
