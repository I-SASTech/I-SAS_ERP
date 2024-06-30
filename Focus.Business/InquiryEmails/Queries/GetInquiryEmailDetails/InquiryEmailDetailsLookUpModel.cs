using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails
{
    public class InquiryEmailDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsReceived { get; set; }
        public Guid InquiryId { get; set; }
        public ICollection<InquiryEmailCCDetailsLookUpModel> InquiryEmailCcDetails { get; set; }


    }
}
