using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class MultiLineItemConfiguration : IEntityTypeConfiguration<MultiUPSLineItem>
    {
        public void Configure(EntityTypeBuilder<MultiUPSLineItem> builder)
        {


            builder.HasOne(ed => ed.MultiUp)
                .WithMany(d => d.MultiUPSLineItems)
                .HasForeignKey(d => d.MultiUpId);




            builder.HasOne(ed => ed.Problem)
             .WithMany(d => d.MultiProblems)
             .HasForeignKey(d => d.ProblemId);

            builder.HasOne(ed => ed.WarrantyCategory)
                .WithMany(d => d.MultiWarrantyCategories)
                .HasForeignKey(d => d.WarrantyCategoryId);

            builder.HasOne(ed => ed.UpsDescription)
               .WithMany(d => d.MultiUpsDescriptions)
               .HasForeignKey(d => d.UpsDescriptionId);

            builder.HasOne(ed => ed.AcessoryInclude)
               .WithMany(d => d.MultiAcessoryIncludes)
               .HasForeignKey(d => d.AcessoryIncludedId);



        }
    }
}
