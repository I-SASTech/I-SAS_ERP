using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class ProductionStockTransfer : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal DamageStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal RemainingStock { get; set; }
        public string Note { get; set; }
        public Guid RemainingWareHouse { get; set; }
        public Guid DamageWareHouse { get; set; }
        public Guid ProductionBatchId { get; set; }
        public Guid ProductId { get; set; }
    }
}
