using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class DeliveryChallanItem : BaseEntity, ITenant
    {
        public Guid? ServiceProductId { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool IsActive { get; set; }
        public Guid DeliveryChallanId { get; set; }
        public virtual DeliveryChallan DeliveryChallan { get; set; }
    }
}
