using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class MultiUpConfiguration : IEntityTypeConfiguration<MultiUp>
    {
        public void Configure(EntityTypeBuilder<MultiUp> builder)
        {

            builder.HasOne(ed => ed.Customer)
              .WithMany(d => d.MultiUps)
              .HasForeignKey(d => d.CustomerId);

            builder.HasOne(ed => ed.Employee)
             .WithMany(d => d.MultiUps)
             .HasForeignKey(d => d.EmployeeId);











        }
    }
}
