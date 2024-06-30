using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {


            builder.Property(d => d.Code).HasMaxLength(150);
            builder.Property(d => d.EnglishName).HasMaxLength(250);
            builder.Property(d => d.Assortment).HasMaxLength(200);
            builder.Property(d => d.StyleNumber).HasMaxLength(200);

            builder.HasOne(d => d.Brand).WithMany(d => d.Products).HasForeignKey(d => d.BrandId);
            builder.HasOne(d => d.Color).WithMany(d => d.Products).HasForeignKey(d => d.ColorId);
            builder.HasOne(d => d.Size).WithMany(d => d.Products).HasForeignKey(d => d.SizeId);
            builder.HasOne(d => d.Origin).WithMany(d => d.Products).HasForeignKey(d => d.OriginId);
            builder.HasOne(d => d.TaxRate).WithMany(d => d.Products).HasForeignKey(d => d.TaxRateId);
            builder.HasOne(d => d.Category).WithMany(d => d.Products).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Unit).WithMany(d => d.Products).HasForeignKey(d => d.UnitId);
            builder.HasOne(d => d.PromotionOffer).WithMany(d => d.Products).HasForeignKey(d => d.PromotionOfferId);

            builder.HasOne(d => d.ProductMaster)
                .WithMany(d => d.Products)
                .HasForeignKey(d => d.ProductMasterId);
            
            builder.HasOne(d => d.ProductGroup)
                .WithMany(d => d.Products)
                .HasForeignKey(d => d.ProductGroupId);


        }
    }

}

