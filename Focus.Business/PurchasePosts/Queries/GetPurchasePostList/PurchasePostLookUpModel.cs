using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostList
{
    public class PurchasePostLookUpModel
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string InvoiceNo { get; set; }
        public string SupplierName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFrom { get; set; }
        public string SupplierNameArabic { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ExpenseUse { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public bool IsInvoice { get; set; }
        public string Date { get; set; }
        public string PurchaseInvoiceNo { get; set; }
        public virtual ICollection<PurchasePostItemLookupModel> PurchasePostItems { get; set; }
        public bool IsCost { get; set; }
        public Guid SupplierId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public string BranchCode { get; set; }

        public string PoNumberAndDate { get; set; }
        public string GoodsRecieveNumberAndDate { get; set; }
        public string Reference { get; set; }
        public Guid? GoodReceiveNoteId { get;  set; }
        public bool IsPurchaseReturn { get;  set; }
        public bool IsClose { get;  set; }
        public bool IsDefault { get; set; }
        public string CustomerEmail { get; set; }
    }
}
