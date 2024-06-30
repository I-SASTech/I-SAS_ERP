using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class StockReceivedItems : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TransferAmount { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockReceivedId { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal lineTotal { get; set; }
        public virtual StockReceived StockReceived {  get; set; }
    }
}
