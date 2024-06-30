using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class StockRequest : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public string Code { get; set; }
        public StockRequestStatus StockRequestStatus { get; set; }
        public Guid? FromWareHouse { get; set; }
        public Guid? FromBranchId { get; set; }
        public Guid? ToBranchId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
        public virtual ICollection<StockRequestItems> StockRequestItems { get; set; }
        public virtual ICollection<StockTransfer> StockTransfer { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
