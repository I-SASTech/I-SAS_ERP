using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DenominationConfiguration : IEntityTypeConfiguration<DenominationSetup>
    {
        public void Configure(EntityTypeBuilder<DenominationSetup> builder)
        {
            builder.Property(x => x.Number).HasColumnType("decimal(18,4)");

        }
    }
}