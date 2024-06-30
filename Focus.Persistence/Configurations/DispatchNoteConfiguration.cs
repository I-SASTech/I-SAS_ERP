using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DispatchNoteConfiguration : IEntityTypeConfiguration<DispatchNote>
    {
        public void Configure(EntityTypeBuilder<DispatchNote> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();

            builder.HasOne(s => s.Customer)
                .WithMany(ad => ad.DispatchNotes)
                .HasForeignKey(ad => ad.CustomerId);

            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.DispatchNotes)
                .HasForeignKey(ad => ad.SaleOrderId);
        }
    }
}
