using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PromotionOfferItemConfiguration : IEntityTypeConfiguration<PromotionOfferItem>
    {
        public void Configure(EntityTypeBuilder<PromotionOfferItem> builder)
        {
            builder.HasOne(d => d.PromotionOffer)
                .WithMany(d => d.PromotionOfferItems)
                .HasForeignKey(d => d.PromotionOfferId);

            builder.HasOne(d => d.Product)
                .WithMany(d => d.PromotionOfferItems)
                .HasForeignKey(d => d.ProductId);

            builder.HasOne(d => d.GetProduct)
                .WithMany(d => d.GetPromotionOfferItems)
                .HasForeignKey(d => d.GetProductId);

        }
    }
}
