using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class AutoPurchaseTemplateItemConfiguration : IEntityTypeConfiguration<AutoPurchaseTemplateItem>
    {
        public void Configure(EntityTypeBuilder<AutoPurchaseTemplateItem> builder)
        {
            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.AutoPurchaseTemplateItems)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.AutoPurchaseTemplate)
                .WithMany(ad => ad.AutoPurchaseTemplateItems)
                .HasForeignKey(ad => ad.AutoPurchaseTemplateId);
        }
    }
}
