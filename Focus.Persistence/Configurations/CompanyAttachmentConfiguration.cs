using Microsoft.EntityFrameworkCore;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class CompanyAttachmentConfiguration : IEntityTypeConfiguration<CompanyAttachment>
    {
        public void Configure(EntityTypeBuilder<CompanyAttachment> builder)
        {
            builder.HasOne(ea => ea.Company)
                .WithMany(a => a.CompanyAttachments)
                .HasForeignKey(ea => ea.CompanyId);
        }
    }
}
