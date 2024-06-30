using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails
{
    public class InquiryMeetingDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Agenda { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid InquiryId { get; set; }
        public ICollection<InquiryMeetingAttendantLookUp> InquiryMeetingAttendantLookUp { get; set; }


    }
}
