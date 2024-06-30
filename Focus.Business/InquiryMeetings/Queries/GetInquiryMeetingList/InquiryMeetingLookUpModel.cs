using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails;

namespace Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingList
{
    public class InquiryMeetingLookUpModel 
    {
        public Guid Id { get; set; }
        public string Agenda { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid InquiryId { get; set; }
        public bool ViewAllAttendant { get; set; }
        public ICollection<InquiryMeetingAttendantLookUp> InquiryMeetingAttendantLookUp { get; set; }
    }
}
