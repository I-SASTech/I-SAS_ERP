using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class MobileOrderItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    
        public int Quantity { get; set; }
        public decimal Price { get; set; }
       
        public Guid MobileOrderId { get; set; }
        public virtual MobileOrder MobileOrder { get; set; }
    }
}
    