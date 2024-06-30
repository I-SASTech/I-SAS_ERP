using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Business.StockAdjustments.Commands.AddUpdateStockAdjustment;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Noble.Api.Models
{
    public class StockAdjustmentVm
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public bool isDraft { get; set; }
        public StockAdjustmentType stockAdjustmentType { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public string Reason { get; set; }
        public bool IsSerial { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public Guid? ToBranchId { get; set; }
        public Guid? FromBranchId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<StockAdjustmentDetailLookUpModel> StockAdjustmentDetails { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BranchId { get; set; }

    }
}
