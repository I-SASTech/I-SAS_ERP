using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
   public class PurchaseOrder : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Contact Supplier { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool Raw { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public bool IsProcessed { get; set; }
        public PartiallyInvoices PartiallyReceived { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchasePost> PurchasePosts { get; set; }
        public virtual ICollection<PurchaseAttachment> PurchaseAttachments { get; set; }
        public virtual ICollection<PurchaseOrderAction> PurchaseOrderActions { get; set; }
        public virtual ICollection<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        public virtual ICollection<PurchaseOrderVersion> PurchaseOrderVersions { get; set; }
        public virtual ICollection<GoodReceiveNote> GoodReceiveNotes { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DocumentType { get; set; }
        public string Reason { get; set; }
        public string Reference { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
