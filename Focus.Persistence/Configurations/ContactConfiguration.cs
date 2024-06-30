using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasOne(ea => ea.Account)
                .WithMany(a => a.Contacts)
                .HasForeignKey(ea => ea.AccountId);
            builder.HasOne(ea => ea.Region)
               .WithMany(a => a.Contacts)
               .HasForeignKey(ea => ea.RegionId);

            builder.HasOne(ea => ea.AdvanceAccount)
                .WithMany(a => a.ContactAdvanceAccount)
                .HasForeignKey(ea => ea.AdvanceAccountId);

            builder.HasOne(ea => ea.SupplierCashAccount)
                .WithMany(a => a.ContactCashAccount)
                .HasForeignKey(ea => ea.SupplierCashAccountId);
            
            builder.HasOne(ea => ea.Currency)
                .WithMany(a => a.Contacts)
                .HasForeignKey(ea => ea.CurrencyId);
            
            builder.HasOne(ea => ea.TaxRate)
                .WithMany(a => a.Contacts)
                .HasForeignKey(ea => ea.TaxRateId); 
            
            builder.HasOne(ea => ea.CustomerGroups)
                .WithMany(a => a.Contacts)
                .HasForeignKey(ea => ea.CustomerGroupId);

        }
    }
}

