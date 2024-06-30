using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BundleCategoryConfiguration : IEntityTypeConfiguration<BundleCategory>
    {
        public void Configure(EntityTypeBuilder<BundleCategory> builder)
        {
            builder.HasOne(ed => ed.Product)
                .WithMany(d => d.BundleCategories)
                .HasForeignKey(d => d.ProductId);
        }
    }
}
