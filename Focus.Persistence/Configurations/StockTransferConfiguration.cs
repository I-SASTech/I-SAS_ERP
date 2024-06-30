using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Focus.Persistence.Configurations
{
    public class StockTransferConfiguration : IEntityTypeConfiguration<StockTransfer>
    {
        public void Configure(EntityTypeBuilder<StockTransfer> builder)
        {
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,4)");

            builder.HasOne<StockRequest>(s => s.StockRequest)
               .WithMany(ad => ad.StockTransfer)
               .HasForeignKey(ad => ad.StockRequestId);

        }
    }
}
