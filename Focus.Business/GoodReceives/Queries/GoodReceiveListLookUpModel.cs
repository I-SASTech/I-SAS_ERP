using Focus.Domain.Enum;
using System;

namespace Focus.Business.GoodReceives.Queries
{
    public class GoodReceiveListLookUpModel
    
       {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameArabic { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public string Date { get; set; }
        public PartiallyInvoices PartiallyReceived { get; set; }
        public string BranchCode { get; set; }
        public string CreatedById { get;  set; }
        public bool IsClose { get;  set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        public string Reference { get; set; }
        public string InvoiceNo { get; set; }
        public string CreatedBy { get;  set; }
        public string CreatedFrom { get;  set; }
        public Guid? SupplierQuotationId { get;  set; }
        public Guid? PurchaseOrderId { get;  set; }
        public Guid SupplierId { get;  set; }
        public bool IsDefault { get;  set; }
        public string CustomerEmail { get; set; }
    }
}
