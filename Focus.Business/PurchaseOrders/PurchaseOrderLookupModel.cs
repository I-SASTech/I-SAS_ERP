using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseOrders
{
    public class PurchaseOrderLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string Note { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public bool IsClose { get; set; }
        public string Path { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool IsRaw { get; set; }
        public bool IsMultiUnit { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool InternationalPurchase { get; set; }
        public virtual ICollection<PurchaseOrderItemLookupModel> PurchaseOrderItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }

        public decimal TotalAfterDiscount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public string DocumentType { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }

        public Guid? SupplierQuotationId { get; set; }
        public Guid? BranchId { get; set; }
        public string Reference { get; set; }
        public string DocumentTypeForClose { get; set; }

        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }


    }
}
