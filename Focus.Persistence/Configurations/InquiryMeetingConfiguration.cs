using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryMeetingConfiguration:IEntityTypeConfiguration<InquiryMeeting>
    {
        public void Configure(EntityTypeBuilder<InquiryMeeting> builder)
        {

            builder.HasOne(x => x.Inquiry)
                .WithMany(x => x.InquiryMeetings)
                .HasForeignKey(x => x.InquiryId);
        }
    }
}
