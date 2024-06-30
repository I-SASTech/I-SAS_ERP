using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class StockRequestItemsConfiguration : IEntityTypeConfiguration<StockRequestItems>
    {
        public void Configure(EntityTypeBuilder<StockRequestItems> builder)
        {
            builder.Property(x => x.Quantity).HasColumnType("decimal(18,4)");

            builder.HasOne<Product>(s => s.Product)
               .WithMany(ad => ad.StockRequestItems)
               .HasForeignKey(ad => ad.ProductId);

            builder.HasOne(s => s.StockRequests)
                .WithMany(ad => ad.StockRequestItems)
                .HasForeignKey(ad => ad.StockRequestId);


        }
    }
}
