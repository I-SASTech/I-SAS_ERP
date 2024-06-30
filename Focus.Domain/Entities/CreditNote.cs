using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CreditNote : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public Guid ContactId { get; set; }
        public string InvoiceNote { get; set; }
        public virtual Contact Contact { get; set; }
        public Guid? WareHouseId { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        public Guid? PurchasePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
        public bool IsInventoryTransaction { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsService { get; set; }
        public string Narration { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Amount { get; set; }
        public bool IsCreditNote { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? CreatedBy { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<CreditNoteItem> CreditNoteItems { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
