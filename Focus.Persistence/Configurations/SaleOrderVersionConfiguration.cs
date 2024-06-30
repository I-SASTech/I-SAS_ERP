using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleOrderVersionConfiguration : IEntityTypeConfiguration<SaleOrderVersion>
    {
        public void Configure(EntityTypeBuilder<SaleOrderVersion> builder)
        {
            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.SaleOrderVersions)
                .HasForeignKey(ad => ad.SaleOrderId);
        }
    }
}
