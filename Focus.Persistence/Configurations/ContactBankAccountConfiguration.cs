using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class ContactBankAccountConfiguration : IEntityTypeConfiguration<ContactBankAccount>
    {
        public void Configure(EntityTypeBuilder<ContactBankAccount> builder)
        {
            builder.HasOne(ea => ea.Contact)
                .WithMany(a => a.ContactBankAccounts)
                .HasForeignKey(ea => ea.ContactId);
            
        }
    }
}
