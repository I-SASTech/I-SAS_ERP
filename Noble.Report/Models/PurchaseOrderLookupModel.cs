using Noble.Report.Enums;
using System;
using System.Collections.Generic;

namespace Noble.Report.Models
{
    public class PurchaseOrderLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string QrCode { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Note { get; set; }
        public string Prefix { get; set; }
        public bool IsClose { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string Path { get; set; }
        public bool IsRaw { get; internal set; }
        public virtual ICollection<PurchaseOrderItemLookupModel> PurchaseOrderItems { get; set; }
        public virtual ICollection<PaymentVoucherLookUpModel> PaymentVoucher { get; set; }
      
        public string TaxRatesName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public Guid? AdvanceAccountId { get; set; }
        public string Version { get; set; }
        public decimal ExpenseUse { get; set; }
      
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public string SupplierQuotationNo { get; set; }
        public string Reference { get; set; }
        public string DisplatName { get; set; }

        public string TaxRateName { get; set; }
        public string SupplierVAT { get; set; }
        public string SupplierCRN { get; set; }
        public string CustomerAddress { get; set; }
        public string DeliveryAddress { get; set; }

        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
    }
}
