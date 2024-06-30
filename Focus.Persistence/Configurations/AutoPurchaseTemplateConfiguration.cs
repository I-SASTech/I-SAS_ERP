using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class AutoPurchaseTemplateConfiguration : IEntityTypeConfiguration<AutoPurchaseTemplate>
    {
        public void Configure(EntityTypeBuilder<AutoPurchaseTemplate> builder)
        {
            builder.HasOne(s => s.Supplier)
                .WithMany(ad => ad.AutoPurchaseTemplates)
                .HasForeignKey(ad => ad.SupplierId);
            
        }
    }
}
