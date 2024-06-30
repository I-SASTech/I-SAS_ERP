using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class BundleOfferBranchesConfiguration : IEntityTypeConfiguration<BundleOfferBranches>
    {
        public void Configure(EntityTypeBuilder<BundleOfferBranches> builder)
        {

            builder.HasOne(s => s.BundleCategories)
                .WithMany(ad => ad.BundleOfferBranches)
                .HasForeignKey(ad => ad.BundleCategoriesId);
            
            builder.HasOne(s => s.PromotionOffer)
                .WithMany(ad => ad.BundleOfferBranches)
                .HasForeignKey(ad => ad.PromotionOfferId);
        }
    }
}
