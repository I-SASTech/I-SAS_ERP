
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class GoodReceiveNote : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? BomId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public PartiallyInvoices PartiallyReceived { get; set; }

        public Guid SupplierId { get; set; }
        public virtual Contact Supplier { get; set; }
        public string ApprovedBy { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public string TaxMethod { get; set; }
        public bool Raw { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual ICollection<GoodReceiveNoteItem> GoodReceiveNoteItems { get; set; }
        public virtual ICollection<PurchasePost> PurchasePosts { get; set; }


        public Guid? BranchId { get; set; }


        public bool IsCredit { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public string PoNumber { get; set; }
        public string SoNumber { get; set; }
        public DateTime? PoDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public string PaymentType { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        public string Reference { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
