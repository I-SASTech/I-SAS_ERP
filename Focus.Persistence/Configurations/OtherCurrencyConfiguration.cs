using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class OtherCurrencyConfiguration : IEntityTypeConfiguration<OtherCurrency>
    {
        public void Configure(EntityTypeBuilder<OtherCurrency> builder)
        {
            builder.HasOne(ed => ed.Currency)
                .WithMany(d => d.OtherCurrencies)
                .HasForeignKey(d => d.CurrencyId);
        }
    }
}
