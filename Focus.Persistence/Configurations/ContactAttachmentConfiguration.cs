using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
  public  class ContactAttachmentConfiguration : IEntityTypeConfiguration<ContactAttachment>
    {
        public void Configure(EntityTypeBuilder<ContactAttachment> builder)
        {
           


            builder.HasOne(ea => ea.Contact)
                .WithMany(a => a.ContactAttachments)
                .HasForeignKey(ea => ea.ContactId);
               
        }
    }
}
