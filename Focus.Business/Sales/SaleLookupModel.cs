using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.Sales
{
    public class SaleLookupModel
    {
        public Guid Id { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RegistrationNo { get; set; }
        public string TerminalId { get; set; }
        public string PoNumber { get; set; }
        public DateTime? PoDate { get; set; }
        public string CustomerInquiry { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string BillingAddress { get; set; }
        public string ReferredBy { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool IsOverWrite { get; set; }
        public bool IsTouch { get; set; }
        public bool IsPurchaseOrder { get; set; }

        public string CustomerAddressWalkIn { get; set; }
        public string VoucherNo { get; set; }
        public string CashCustomer { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string RefrenceNo { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? AreaId { get; set; }
        public Guid? UnRegisteredVatId { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid WareHouseId { get; set; }
        public bool IsCredit { get; set; }
        public bool IsRemove { get; set; }
        public bool IsEditPaidInvoice { get; set; }
        public bool IsCashCustomer { get; set; }
        public decimal Change { get; set; }
        public decimal UnRegisteredVAtAmount { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public DocumentType DocumentType { get; set; }
        public virtual ICollection<SaleItemLookupModel> SaleItems { get; set; }
        public virtual ICollection<CreditPay> CreditPays { get; set; }
        public virtual SalePaymentLookupModel SalePayment { get; set; }
        public virtual OtherCurrencyLookupModel OtherCurrency { get; set; }
        public string Email { get; set; }
        public string CashCustomerId { get; set; }
        public bool IsCashVoucher { get; set; }
        public bool DayStart { get; set; }
        public string CounterId { get; set; }
        public string BarCode { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? LogisticId { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public string SoInventoryReserve { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public decimal ReturnInvoiceAmount { get; set; }
        public string AutoPaymentVoucher { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public bool IsService { get; set; }
        public string TermAndCondition { get; set; }
        public string TermAndConditionAr { get; set; }
        public string WarrantyLogoPath { get; set; }
        public bool IsSerial { get; set; }
        public bool IsFifo { get; set; }
        public bool PartiallyQuotation { get; set; }
        public bool PartiallySaleOrder { get; set; }
        public bool ColorVariants { get; set; }
        public bool IsWarranty { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsWalkIn { get; set; }
        public bool IsBeforeTax { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public bool AllowPreviousFinancialPeriod { get; set; }
        public Guid? ProformaInvoiceId { get; set; }
        public string InvoicePrefix { get; set; }
        public string Note { get; set; }
        public string SaleOrderNo { get; set; }
        public string QuotationNo { get; set; }
        public string PeroformaInvoiceNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string InquiryNo { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string InvoiceNote { get; set; }

        public string DeliveryNoteAndDate { get; set; }
        public string QuotationValidUpto { get; set; }
        public string PerfomaValidUpto { get; set; }

        public string ClientPurchaseNo { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string Purpose { get; set; }
        public string For { get; set; }

        public SaleHoldTypes SaleHoldTypes { get; set; }
        public Guid? BranchId { get; set; }

    }
}
