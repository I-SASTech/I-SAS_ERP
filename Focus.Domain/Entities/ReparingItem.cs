using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class ReparingItem : BaseEntity,  ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Waste { get; set; }
        public Guid? ReparingOrderId { get; set; }
        public virtual ReparingOrder ReparingOrder { get; set; }


    }
}
