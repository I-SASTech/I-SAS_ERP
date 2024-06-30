using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class StockTransfer : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public string Code { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public StockTransferStatus StockTransferStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid? WarehouseId {get; set;}
        public Guid? BranchId {get; set;}
        public Guid? StockRequestId {get; set;}
        public Guid? StockRequesBranchtId {get; set;}
        public Guid? VoucherId {get; set;}
        public virtual StockRequest StockRequest { get; set; }
        public string VehicalNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string DriverNationalId { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<StockTransferItems> StockTransferItems { get; set; }
        public virtual ICollection<StockReceived> StockReceived { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
