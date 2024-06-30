using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Business.PurchasePosts.Commands;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.PurchasePosts
{
    public class PurchasePostLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Dates { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RegistrationNo { get; set; }
        public string GoodReceiveNo { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierVAT { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public bool AllowPreviousFinancialPeriod { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public decimal InvoiceFixDiscount { get; set; }
        public string QrCode { get; set; }
        public decimal NetAmount { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool IsRaw { get; set; }
        public bool IsMultiUnit { get; set; }
        public virtual List<PurchasePostItemLookupModel> PurchasePostItems { get; set; }
        public virtual List<ActionLookUpModel> ActionProcess { get; set; }
        public virtual List<PurchaseAttachment> PurchaseAttachments { get; set; }
        public virtual List<PaymentVoucherLookUpModel> PaymentVoucher { get; set; }
        public virtual List<PurchasePostExpenseLookUp> PurchasePostExpense { get; set; }
        public string WareHouseName { get; set; }
        public string TaxRatesName { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public bool IsPurchasePost { get; set; }
        public bool PartiallyPurchase { get; set; }
        public bool AutoPurchaseVoucher { get; set; }
        public decimal DueAmount { get; set; }
        public bool ExpenseToGst { get; set; }
        public List<PurchaseInvoiceAttachment> PurchaseInvoiceAttachments { get; set; }
        public List<PurchaseInvoiceActionLookUp> PurchaseInvoiceActions { get; set; }
        public List<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public bool IsClose { get; set; }
        public bool IsCost { get; set; }
        public bool InternationalPurchase { get; set; }
        public bool IsCredit { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public string PaymentType { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public bool ColorVariants { get; set; }
        public Guid? GoodReceiveNoteId { get;  set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public string PoNumberAndDate { get; set; }
        public string GoodsRecieveNumberAndDate { get; set; }
        public string Note { get; set; }
        public string Reference { get; set; }
        public string PurchseOrderValue { get; set; }
        public string SupplierContact { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCRN { get; set; }
        public string SupplierRegNo { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierCNIC { get; set; }
        public string SupplierPrefix { get; set; }
        public string SupplierEnglishName { get; set; }
        public string SupplierArabicName { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
    }
}
