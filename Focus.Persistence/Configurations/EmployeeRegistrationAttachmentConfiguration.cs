using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class EmployeeRegistrationAttachmentConfiguration : IEntityTypeConfiguration<EmployeeRegistrationAttachment>
    {
        public void Configure(EntityTypeBuilder<EmployeeRegistrationAttachment> builder)
        {
            builder.HasOne(ea => ea.Employee)
                .WithMany(a => a.EmployeeAttachments)
                .HasForeignKey(ea => ea.EmployeeId);
        }
    }
}