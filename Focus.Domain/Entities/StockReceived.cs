using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class StockReceived : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    { 
        public string Code { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public StockTransferStatus StockTransferStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? FromBranchId { get; set; }
        public Guid? StockTransferId { get; set; }
        public Guid? StockRequestId { get; set; }
        public virtual StockTransfer StockTransfer { get; set; }
        public string VehicalNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string DriverNationalId { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<StockReceivedItems> StockReceivedItems { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
