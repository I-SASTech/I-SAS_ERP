using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class InquiryDetailForListLookUp
    {
        public Guid Id { get; set; }
        public string InquiryNumber { get; set; }
        public string CustomerName { get; set; }
        public string MediaType { get; set; }
        public Guid? Priority { get; set; }
        public string InquiryType { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public Guid? InquiryPriorityId { get; internal set; }
    }
}
