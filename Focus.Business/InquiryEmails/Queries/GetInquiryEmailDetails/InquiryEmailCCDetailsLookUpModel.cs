using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails
{
    public class InquiryEmailCCDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid InquiryEmailId { get; set; }
    }
}
