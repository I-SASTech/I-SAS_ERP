using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TemporaryCashRequestItemConfiguration : IEntityTypeConfiguration<TemporaryCashRequestItem>
    {
        public void Configure(EntityTypeBuilder<TemporaryCashRequestItem> builder)
        {
            builder.HasOne(ed => ed.TemporaryCashRequest)
                .WithMany(d => d.TemporaryCashRequestItems)
                .HasForeignKey(d => d.TemporaryCashRequestId);
        }
    }
}
