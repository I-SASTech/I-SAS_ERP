using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class Sale : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete, IRecordDailyEntry
    {
        public DateTime Date { get; set; }
        public string PoNumber { get; set; }
        public DateTime? PoDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string CustomerInquiry { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string BillingAddress { get; set; }
        public string ReferredBy { get; set; }
        public string RegistrationNo { get; set; }
        public string RefrenceNo { get; set; }
        public string TaxMethod { get; set; }
        public string DoctorName { get; set; }
        public string DeliveryNoteAndDate { get; set; }
        public string QuotationValidUpto { get; set; }
        public string PerfomaValidUpto { get; set; }
        public string HospitalName { get; set; }
        public string QuotationNo { get; set; }
        public string SaleOrderNo { get; set; }
        public string PeroformaInvoiceNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string InquiryNo { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public virtual Contact Customer { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
        public Guid? AreaId { get; set; }
        public virtual Region Area { get; set; }
        public Guid? CashCustomerId { get; set; }
        public virtual CashCustomer CashCustomer { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCredit { get; set; }
        public bool IsPurchaseOrder { get; set; }
        public Guid WareHouseId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public DocumentType DocumentType { get; set; }
        //public bool IsSaleReturn { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SalePayment> SalePayments { get; set; }
        public virtual ICollection<SaleSizeAssortment> SaleSizeAssortments { get; set; }
        public Guid? OtherCurrencyId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public Guid? SaleReturnInvoiceId { get; set; }
        public bool IsSaleReturn { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public Guid? PeriodId { get; set; }
        public virtual OtherCurrency OtherCurrency { get; set; }
        public decimal Change { get; set; }
        public Guid? UnRegisteredVatId { get; set; }
        public virtual TaxRate UnRegisteredVAT { get; set; }
        public decimal UnRegisteredVAtAmount { get; set; }
        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsVatChange { get; set; }
        public string BarCode { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }

        public Guid? QuotationId { get; set; }
        public virtual SaleOrder Quotation { get; set; }
        public Guid? LogisticId { get; set; }
        public virtual Logistic Logistic { get; set; }
        public string CustomerAddressWalkIn { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<SaleInvoiceAdvancePayment> SaleInvoiceAdvancePayments { get; set; }
        public virtual ICollection<SaleInvoiceDiscount> SaleInvoiceDiscounts { get; set; }
        public virtual ICollection<DeliveryChallan> DeliveryChallansForSales { get; set; }
        public virtual ICollection<CreditNote> CreditNotes { get; set; }

        public string Description { get; set; }
        public string TermAndCondition { get; set; }
        public string TermAndConditionAr { get; set; }
        public string WarrantyLogoPath { get; set; }
        public string ImageSizeHeight { get; set; }
        public string ImageSizeWidth { get; set; }
        public string InvoiceNote { get; set; }
        public string Note { get; set; }
        public bool IsCashCustomer { get; set; }
        public bool IsOverWrite { get; set; }
        public bool IsService { get; set; }
        public bool IsWarranty { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public bool IsProformaInvoice { get; set; }
        public bool MarkAsCompleted { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        public string ProcessedNote { get; set; }
        public bool IsSendMsg { get; set; }
        public bool IsMsgSended { get; set; }
        public bool IsClose { get; set; }

        public bool IgnoreCashCreditForNumber { get; set; }
        public SaleHoldTypes SaleHoldTypes { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
    }

    public enum InvoiceType
    {
        Hold,
        Paid,
        Credit,
        SaleReturn, 
        ProformaInvoice,
        GlobalInvoice,
        ReceiptInvoice,
        PurchaseOrder,
    }
    public enum DocumentType
    {
        SaleInvoice=1,
        SaleReturn=2,
        ProformaInvoice=3,
        GlobalInvoice=4,
        ReceiptInvoice=5,
        SaleOrder=6,
        SaleQuotation=7,
        SaleInvoiceHold = 8,
        CreditNote = 9,
        ServiceQuotation = 10,
        ServiceSaleOrder = 11,
        ServiceProformaInvoice = 12,
        PurchaseOrder = 13,

    }
}
