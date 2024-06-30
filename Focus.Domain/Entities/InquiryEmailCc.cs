using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryEmailCc:BaseEntity, ITenant
    {
        public string Email { get; set; }
        public Guid InquiryEmailId { get; set; }
        public bool IsCc { get; set; }
        public virtual InquiryEmail InquiryEmail { get; set; }
    }
}
