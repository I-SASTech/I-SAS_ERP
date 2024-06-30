using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class PurchaseAttachment : BaseEntity, ITenant
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual  PurchaseOrder PurchaseOrder { get; set; }
       
    }
}
