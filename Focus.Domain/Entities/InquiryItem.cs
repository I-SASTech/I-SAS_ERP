using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryItem : BaseEntity, ITenant
    {
        public Guid ItemId { get; set; }
        public Guid InquiryId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Inquiry Inquiry { get; set; }
    }
}
