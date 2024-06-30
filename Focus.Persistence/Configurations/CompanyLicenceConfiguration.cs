using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class CompanyLicenceConfiguration : IEntityTypeConfiguration<CompanyLicence>
    {
        public void Configure(EntityTypeBuilder<CompanyLicence> builder)
        {
            builder.HasOne(ea => ea.Company)
                .WithMany(a => a.CompanyLicences)
                .HasForeignKey(ea => ea.CompanyId);
        }
    }
}
