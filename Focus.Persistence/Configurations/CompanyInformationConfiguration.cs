using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CompanyInformationConfiguration : IEntityTypeConfiguration<CompanyInformation>
    {
        public void Configure(EntityTypeBuilder<CompanyInformation> builder)
        {
            builder.HasOne(ed => ed.Company)
                .WithOne(d => d.CompanyInformation).HasForeignKey<CompanyInformation>(e => e.CompanyId);
        }
    }
}