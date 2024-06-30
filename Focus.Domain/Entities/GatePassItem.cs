using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class GatePassItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid? WareHouseId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Guid GatePassId { get; set; }
        public virtual GatePass GatePass { get; set; }
        public int HighQuantity { get; set; }
    }
}
