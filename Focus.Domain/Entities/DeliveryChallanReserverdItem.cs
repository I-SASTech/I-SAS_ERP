using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class DeliveryChallanReserverdItem : BaseEntity
    {
        public string UnitName { get; set; }
        public Guid? ServiceProductId { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public Guid DeliveryChallanReservedId { get; set; }
        public bool IsActive { get; set; }
        public virtual DeliveryChallanReserved DeliveryChallanReserved { get; set; }
    }
}
