using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails;

namespace Focus.Business.InquiryEmails.Queries.GetInquiryEmailList
{
    public class InquiryEmailLookUpModel 
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsReceived { get; set; }
        public bool IsViewEmail { get; set; }
        public Guid InquiryId { get; set; }
        public string DateTime { get; set; }
        public ICollection<InquiryEmailCCDetailsLookUpModel> InquiryEmailCcDetails { get; set; }
    }
}
