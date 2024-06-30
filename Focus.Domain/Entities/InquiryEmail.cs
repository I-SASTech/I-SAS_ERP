using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryEmail:BaseEntity, ITenant,ITenantFilterableEntity
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsReceived { get; set; }
        public Guid InquiryId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual ICollection<InquiryEmailCc> InquiryEmailCcs { get; set; }
    }
}
