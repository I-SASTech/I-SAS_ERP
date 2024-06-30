using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Bom : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string ApprovalStatus { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual ICollection<BomSaleOrderItems> BomSaleOrderItems { get; set; }
    }
}
