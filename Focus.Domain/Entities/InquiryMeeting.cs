using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryMeeting: BaseEntity,ITenant, ITenantFilterableEntity
    {
        public string Agenda { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid InquiryId { get; set; }
        public InquiryMeetingStatus InquiryMeetingStatus { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual ICollection<InquiryMeetingAttendant> InquiryMeetingAttendants { get; set; }
        
    }
}
