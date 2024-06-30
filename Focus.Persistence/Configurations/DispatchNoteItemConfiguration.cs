using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DispatchNoteItemConfiguration : IEntityTypeConfiguration<DispatchNoteItem>
    {
        public void Configure(EntityTypeBuilder<DispatchNoteItem> builder)
        {
            builder.HasOne(s => s.DispatchNote)
                .WithMany(ad => ad.DispatchNoteItems)
                .HasForeignKey(ad => ad.DispatchNoteId);
        }
    }
}
