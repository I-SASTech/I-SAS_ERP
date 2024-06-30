using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryStatus:BaseEntity,ITenant
    {
        public string Reason { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? InquiryStatusDynamicId { get; set; }
        public virtual InquiryStatusDynamic InquiryStatusDynamic { get; set; }
        public Guid InquiryId { get; set; }
        public virtual Inquiry Inquiry { get; set; }
    }
}
