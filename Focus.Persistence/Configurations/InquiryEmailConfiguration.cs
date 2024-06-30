using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryEmailConfiguration:IEntityTypeConfiguration<InquiryEmail>
    {
        public void Configure(EntityTypeBuilder<InquiryEmail> builder)
        {

            builder.HasOne(x => x.Inquiry)
                .WithMany(x => x.InquiryEmails)
                .HasForeignKey(x => x.InquiryId);
        }
    }
}
