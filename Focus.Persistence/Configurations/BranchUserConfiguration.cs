using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class BranchUserConfiguration : IEntityTypeConfiguration<BranchUser>
    {
        public void Configure(EntityTypeBuilder<BranchUser> builder)
        {
            builder.HasOne(ed => ed.Branch)
                .WithMany(d => d.BranchUsers)
                .HasForeignKey(d => d.BranchId);
        }
    }
}
