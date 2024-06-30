using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CreditNoteItemConfiguration : IEntityTypeConfiguration<CreditNoteItem>
    {
        public void Configure(EntityTypeBuilder<CreditNoteItem> builder)
        {
            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.CreditNoteItems)
                .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.CreditNoteItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.WareHouse)
                .WithMany(ad => ad.CreditNoteItems)
                .HasForeignKey(ad => ad.WareHouseId);

            builder.HasOne(s => s.CreditNote)
                .WithMany(ad => ad.CreditNoteItems)
                .HasForeignKey(ad => ad.CreditNoteId);

        }
    }
}
