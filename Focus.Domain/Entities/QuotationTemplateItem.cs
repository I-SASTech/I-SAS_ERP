using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class QuotationTemplateItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public Guid QuotationTemplateId { get; set; }
        public virtual QuotationTemplate QuotationTemplate { get; set; }
    }
}
