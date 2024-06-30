using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PromotionOfferConfiguration : IEntityTypeConfiguration<PromotionOffer>
    {
        public void Configure(EntityTypeBuilder<PromotionOffer> builder)
        {
            builder.HasOne(ed => ed.Product)
                .WithMany(d => d.PromotionOffers)
                .HasForeignKey(d => d.ProductId);

            builder.HasOne(ed => ed.ProductGroup)
                .WithMany(d => d.PromotionOffers)
                .HasForeignKey(d => d.ProductGroupId);

            builder.HasOne(ed => ed.GetProduct)
                .WithMany(d => d.GetPromotionOffers)
                .HasForeignKey(d => d.GetProductId);
        }
    }
}
