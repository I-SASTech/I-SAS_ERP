using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class BatchProcess: BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string Color { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public Guid? WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public Guid? ProductionBatchId { get; set; }
        public virtual ProductionBatch ProductionBatch { get; set; }
        public Guid? ProcessId { get; set; }
        public virtual Process Process { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpectedEndDay { get; set; }
        public DateTime? ActualEndDay { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<BatchProcessContractor> BatchProcessContractors { get; set; }

    }
}
