using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Configurations
{
    public class StockReceivedConfiguration : IEntityTypeConfiguration<StockReceived>
    {
        public void Configure(EntityTypeBuilder<StockReceived> builder)
        {
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,4)");

            builder.HasOne<StockTransfer>(s => s.StockTransfer)
               .WithMany(ad => ad.StockReceived)
               .HasForeignKey(ad => ad.StockTransferId);

        }
    }
}
