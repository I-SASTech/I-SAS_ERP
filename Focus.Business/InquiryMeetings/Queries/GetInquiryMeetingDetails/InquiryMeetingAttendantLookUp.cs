using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails
{
    public class InquiryMeetingAttendantLookUp
    {
        public Guid Id { get; set; }
        public Guid MeetingId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeArabicName { get; set; }
    }
}
