using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class RecipeItemConfiguration : IEntityTypeConfiguration<RecipeItem>
    {
        public void Configure(EntityTypeBuilder<RecipeItem> builder)
        {

            builder.Property(x => x.Quantity);
            builder.Property(x => x.HighQuantity);
            builder.Property(x => x.Waste).HasColumnType("decimal(18,2)");
            builder.Property(x => x.RecipeNoId).IsRequired();

            builder.HasOne(s => s.RecipeNo)
                .WithMany(ad => ad.RecipeItems)
                .HasForeignKey(ad => ad.RecipeNoId);

            builder.HasOne(s => s.WareHouse)
         .WithMany(ad => ad.RecipeItems)
         .HasForeignKey(ad => ad.WareHouseId);



        }
    }
}
