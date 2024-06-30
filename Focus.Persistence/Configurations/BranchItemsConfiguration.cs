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
    public class BranchItemsConfiguration : IEntityTypeConfiguration<BranchItems>
    {
        public void Configure(EntityTypeBuilder<BranchItems> builder)
        {

            builder.HasOne(s => s.Products)
                .WithMany(ad => ad.BranchItems)
                .HasForeignKey(ad => ad.ProductId);        }
    }
}
