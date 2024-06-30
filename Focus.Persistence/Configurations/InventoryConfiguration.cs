using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.SalePrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.CurrentStockValue).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.AveragePrice).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Quantity);
            builder.Property(x => x.CurrentQuantity);
            builder.Property(x => x.Date);
            builder.Property(x => x.TransactionType).IsRequired();
            builder.Property(x => x.DocumentId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.WareHouseId).IsRequired();
            builder.Property(x => x.ExpiryDate);
            builder.Property(x => x.AutoNumbering);

            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.Inventories)
                .HasForeignKey(ad => ad.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
