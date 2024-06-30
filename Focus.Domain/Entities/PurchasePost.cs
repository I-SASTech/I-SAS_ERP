using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class PurchasePost : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Contact Supplier { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public Guid? GoodReceiveNoteId { get; set; }
        public virtual GoodReceiveNote GoodReceiveNote { get; set; }
        public Guid WareHouseId { get; set; }
        public decimal InvoiceFixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public string TaxMethod { get; set; }
        public bool Raw { get; set; }
        public bool IsClose { get; set; }
        public bool IsPurchasePost { get; set; }
        public decimal ExpenseUse { get; set; }
        public bool IsCost { get; set; }
        public Guid? PeriodId { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public string PaymentType { get; set; }
        public Guid? BankCashAccountId { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public virtual ICollection<PurchasePostItem> PurchasePostItems { get; set; }
        public virtual ICollection<PurchaseInvoiceAction> PurchaseInvoiceActions { get; set; }
        public virtual ICollection<PurchaseInvoiceAttachment> PurchaseInvoiceAttachments { get; set; }
        public virtual ICollection<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public virtual ICollection<CreditNote> CreditNotes { get; set; }
        public bool IsCredit { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public string poNumberAndDate { get; set; }
        public string GoodsRecieveNumberAndDate { get; set; }
        public Guid? BranchId { get; set; }

        public string Note { get; set; }
        public string Reference { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }

        public string Year { get; set; }
    }
}
