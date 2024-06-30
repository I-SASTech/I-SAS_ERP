using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BatchCostingConfiguration : IEntityTypeConfiguration<BatchCosting>
    {
        public void Configure(EntityTypeBuilder<BatchCosting> builder)
        {

            

            builder.HasOne(s => s.RecipeNo)
                   .WithMany(ad => ad.BatchCostings)
                   .HasForeignKey(ad => ad.RecipeNoId);




        }
    }
}
