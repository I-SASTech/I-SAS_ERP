using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class AttachmentConfiguration:IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(a => a.Name);
            builder.Property(a => a.Description).HasMaxLength(150);
            builder.Property(a => a.Path).IsRequired();

            builder.HasMany(ea => ea.EmployeeAttachments)
                .WithOne(a => a.Attachment)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
