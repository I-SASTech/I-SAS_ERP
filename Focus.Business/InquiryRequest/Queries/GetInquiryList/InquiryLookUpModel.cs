using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryList
{
    public class InquiryLookUpModel
    {
        public Guid Id { get; set; }
        public string InquiryNumber { get; set; }
        public string CreatedOn { get; set; }
        public string ReceivedBy { get; set; }
        public string Process { get; set; }
        public string Status { get; set; }
        
    }
}
