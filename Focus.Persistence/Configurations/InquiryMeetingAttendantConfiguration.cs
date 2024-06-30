using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryMeetingAttendantConfiguration:IEntityTypeConfiguration<InquiryMeetingAttendant>
    {
        public void Configure(EntityTypeBuilder<InquiryMeetingAttendant> builder)
        {
            builder.HasOne(x => x.EmployeeRegistration).WithMany(x => x.InquiryMeetingAttendants)
                .HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.InquiryMeeting).WithMany(x => x.InquiryMeetingAttendants)
                .HasForeignKey(x => x.MeetingId);
        }
    }
}
