using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class QuotationTemplateConfiguration : IEntityTypeConfiguration<QuotationTemplate>
    {
        public void Configure(EntityTypeBuilder<QuotationTemplate> builder)
        {
            builder.Property(x => x.Date).IsRequired();

            

        }
    }
}
