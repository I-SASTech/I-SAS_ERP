using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Focus.Persistence.Configurations
{
   public class RecipeNoConfiguration : IEntityTypeConfiguration<RecipeNo>
    {
        public void Configure(EntityTypeBuilder<RecipeNo> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ExpireDate).IsRequired(false);
            builder.Property(x => x.RegistrationNo).IsRequired();

            builder.HasOne(s => s.Product)
                .WithMany(ad => ad.RecipeNos)
                .HasForeignKey(ad => ad.ProductId).OnDelete(DeleteBehavior.Restrict) ;

            builder.HasOne(s => s.SampleRequest)
                .WithMany(ad => ad.RecipeNos)
                .HasForeignKey(ad => ad.SampleRequestId);

        }
    }
}
