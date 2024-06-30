using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryMeetingAttendant:BaseEntity, ITenant
    {
        public Guid MeetingId { get; set; }
        public virtual InquiryMeeting InquiryMeeting { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }

    }
}
