
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Focus.Persistence.Configurations
{
  public  class QuotationTemplateItemConfiguration : IEntityTypeConfiguration<QuotationTemplateItem>
    {
        public void Configure(EntityTypeBuilder<QuotationTemplateItem> builder)
        {
            builder.HasOne(s => s.QuotationTemplate)
                 .WithMany(ad => ad.QuotationTemplateItems)
                 .HasForeignKey(ad => ad.QuotationTemplateId);
            builder.HasOne(s => s.Product)
               .WithMany(ad => ad.QuotationTemplateItems)
               .HasForeignKey(ad => ad.ProductId);



        }
    }
}
