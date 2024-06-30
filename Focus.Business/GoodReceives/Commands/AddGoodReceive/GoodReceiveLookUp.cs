using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Business.GoodReceives.Commands.AddGoodReceive
{
    public class GoodReceiveLookUp
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Dates { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string RegistrationNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameArabic { get; set; }
        public Guid SupplierId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string ApprovedBy { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsClose { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool Raw { get; set; }
        public Guid? BomId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual ICollection<GoodReceiveItemLookUpModel> GoodReceiveNoteItems { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string TaxRatesName { get;  set; }
        public bool IsRaw { get;  set; }
        public decimal NetAmount { get;  set; }
        public Guid? BranchId { get;  set; }
        public bool IsCredit { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public string poNumberAndDate { get; set; }
        public string GoodsRecieveNumberAndDate { get; set; }
        public string PoNumber { get; set; }
        public DateTime? PoDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public string PaymentType { get; set; }
        public string PurchaseOrderValue { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public string Reference { get;  set; }
        public string QrCode { get; set; }
        public string SupplierVAT { get; set; }
        public string ContactNo1 { get;  set; }
        public string SupplierCommercialRegistrationNo { get;  set; }
        public string SupplierAddress { get; set; }
        public string SupplierCode { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
    }
}
