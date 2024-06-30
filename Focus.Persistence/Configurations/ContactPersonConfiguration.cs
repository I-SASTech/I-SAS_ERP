using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> builder)
        {
            builder.HasOne(ea => ea.Contact)
                .WithMany(a => a.ContactPersons)
                .HasForeignKey(ea => ea.ContactId);
        }
    }
}
