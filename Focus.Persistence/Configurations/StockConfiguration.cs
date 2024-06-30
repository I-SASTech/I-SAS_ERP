using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasOne<Product>(s => s.Product)
                .WithMany(ad => ad.Stocks)
                .HasForeignKey(ad => ad.ProductId);

            builder.HasOne<Warehouse>(s => s.Warehouse)
                .WithMany(ad => ad.Stocks)
                .HasForeignKey(ad => ad.WareHouseId);
        }
    }
}
