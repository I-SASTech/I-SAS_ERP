using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryCommentConfiguration:IEntityTypeConfiguration<InquiryComment>
    {
        public void Configure(EntityTypeBuilder<InquiryComment> builder)
        {
            builder.HasOne(x => x.InquiryCommentParent)
                .WithMany(x => x.InquiryCommentChild)
                .HasForeignKey(x => x.ReplyCommentedId);

            builder.HasOne(x => x.Inquiry)
                .WithMany(x => x.InquiryComments)
                .HasForeignKey(x => x.InquiryId);
        }
    }
}
