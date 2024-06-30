using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TerminalCategoryConfiguration : IEntityTypeConfiguration<TerminalCategory>
    {
        public void Configure(EntityTypeBuilder<TerminalCategory> builder)
        {
            builder.HasOne(s => s.Category)
                .WithMany(ad => ad.TerminalCategories)
                .HasForeignKey(ad => ad.CategoryId);

            builder.HasOne(s => s.Terminal)
                .WithMany(ad => ad.TerminalCategories)
                .HasForeignKey(ad => ad.TerminalId);
        }
    }
}
