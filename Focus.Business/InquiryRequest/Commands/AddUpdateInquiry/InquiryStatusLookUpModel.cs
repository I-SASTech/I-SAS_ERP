using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequest.Commands.AddUpdateInquiry
{
    public class InquiryStatusLookUpModel
    {
        public string Reason { get; set; }
        public string DateTime { get; set; }
        public Guid? InquiryStatusDynamicId { get; set; }
        public string Status { get; set; }
        public Guid InquiryId { get; set; }
    }
}
