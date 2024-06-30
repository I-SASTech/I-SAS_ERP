using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Enums;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class StockAdjustment : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public string TaxMethod { get; set; }
        public string Reason { get; set; }
        public bool isDraft { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public StockAdjustmentType StockAdjustmentType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public bool IsSerial { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
