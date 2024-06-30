using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class EmployeeAttachmentConfiguration:IEntityTypeConfiguration<EmployeeAttachment>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttachment> builder)
        {
            builder.HasOne(ea => ea.Attachment)
                .WithMany(a => a.EmployeeAttachments)
                .HasForeignKey(ea => ea.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ea => ea.Employee)
                .WithMany(a => a.EmployeeAttachments)
                .HasForeignKey(ea => ea.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
