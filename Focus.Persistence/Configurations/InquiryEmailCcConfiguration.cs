using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryEmailCcConfiguration:IEntityTypeConfiguration<InquiryEmailCc>
    {
        public void Configure(EntityTypeBuilder<InquiryEmailCc> builder)
        {
            builder.HasOne(x => x.InquiryEmail)
                .WithMany(x => x.InquiryEmailCcs)
                .HasForeignKey(x => x.InquiryEmailId);
        }
    }
}
