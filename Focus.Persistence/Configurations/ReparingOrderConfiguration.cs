using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ReparingOrderConfiguration : IEntityTypeConfiguration<ReparingOrder>
    {
        public void Configure(EntityTypeBuilder<ReparingOrder> builder)
        {


            builder.HasOne(ed => ed.Problem)
                .WithMany(d => d.Problems)
                .HasForeignKey(d => d.ProblemId); 

            builder.HasOne(ed => ed.WarrantyCategory)
                .WithMany(d => d.WarrantyCategories)
                .HasForeignKey(d => d.WarrantyCategoryId);

            builder.HasOne(ed => ed.UpsDescription)
               .WithMany(d => d.UpsDescriptions)
               .HasForeignKey(d => d.UpsDescriptionId); 

            builder.HasOne(ed => ed.AcessoryInclude)
               .WithMany(d => d.AcessoryIncludes)
               .HasForeignKey(d => d.AcessoryIncludedId);






        }
    }
}
