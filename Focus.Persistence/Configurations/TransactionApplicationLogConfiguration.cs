using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Focus.Persistence.Configurations
{
    public class TransactionApplicationLogConfiguration : IEntityTypeConfiguration<TransactionApplicationLog>
    {
        public void Configure(EntityTypeBuilder<TransactionApplicationLog> builder)
        {
            builder.HasOne(ed => ed.Company)
                .WithMany(d => d.TransactionApplicationLogs)
                .HasForeignKey(f => f.LocationId);

           

        }
    }
}
