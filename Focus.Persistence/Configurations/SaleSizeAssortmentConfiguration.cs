using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleSizeAssortmentConfiguration : IEntityTypeConfiguration<SaleSizeAssortment>
    {
        public void Configure(EntityTypeBuilder<SaleSizeAssortment> builder)
        {
            builder.HasOne(s => s.SaleItem)
                .WithMany(ad => ad.SaleSizeAssortments)
                .HasForeignKey(ad => ad.SaleItemId);
            
            builder.HasOne(s => s.PurchasePostItem)
                .WithMany(ad => ad.SaleSizeAssortments)
                .HasForeignKey(ad => ad.PurchasePostItemId);

            builder.HasOne(s => s.Size)
                .WithMany(ad => ad.SaleSizeAssortments)
                .HasForeignKey(ad => ad.SizeId);
        }
    }
}
