using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SalaryTaxSlabConfiguration : IEntityTypeConfiguration<SalaryTaxSlab>
    {
        public void Configure(EntityTypeBuilder<SalaryTaxSlab> builder)
        {
            builder.HasOne(s => s.TaxSalary)
                .WithMany(ad => ad.SalaryTaxSlabs)
                .HasForeignKey(ad => ad.TaxSalaryId);
        }
    }
}
