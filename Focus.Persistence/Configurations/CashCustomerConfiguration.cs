using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class CashCustomerConfiguration : IEntityTypeConfiguration<CashCustomer>
    {
        public void Configure(EntityTypeBuilder<CashCustomer> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Mobile).IsRequired(false);
            builder.Property(x => x.Code);
        }
    }
}
