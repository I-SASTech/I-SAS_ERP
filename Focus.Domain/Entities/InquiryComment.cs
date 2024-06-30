using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryComment:BaseEntity, ITenant, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid InquiryId { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public Guid? ReplyCommentedId { get; set; }
        public virtual InquiryComment InquiryCommentParent { get; set; }
        public virtual ICollection<InquiryComment> InquiryCommentChild { get; set; }
        public DateTime DateTime { get; set; }
    }
}
