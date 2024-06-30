using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class GoodReceiveItemConfiguration : IEntityTypeConfiguration<GoodReceiveNoteItem>
    {
        public void Configure(EntityTypeBuilder<GoodReceiveNoteItem> builder)
        {
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Discount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.FixDiscount).HasColumnType("decimal(18,4)");
            builder.Property(x => x.TaxRateId).IsRequired();
            builder.Property(x => x.SerialNo).IsRequired(false);

            builder.HasOne(s => s.GoodReceiveNote)
                .WithMany(ad => ad.GoodReceiveNoteItems)
                .HasForeignKey(ad => ad.GoodReceiveNoteId);
            builder.HasOne(s => s.TaxRate)
              .WithMany(ad => ad.GoodReceiveNoteItems)
              .HasForeignKey(ad => ad.TaxRateId);




        }
    }
}
