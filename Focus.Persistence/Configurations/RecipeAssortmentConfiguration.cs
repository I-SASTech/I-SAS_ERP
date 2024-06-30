using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class RecipeAssortmentConfiguration : IEntityTypeConfiguration<RecipeAssortment>
    {
        public void Configure(EntityTypeBuilder<RecipeAssortment> builder)
        {
            builder.HasOne(s => s.RecipeNo)
                .WithMany(ad => ad.RecipeAssortments)
                .HasForeignKey(ad => ad.RecipeNoId);
        }
    }
}
