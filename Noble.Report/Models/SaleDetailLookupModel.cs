using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class SaleDetailLookupModel
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PoDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string InvoiceNo { get; set; }
        public string TerminalId { get; set; }
        public string PoNumber { get; set; }
        public string InvoiceNote { get; set; }
        public string CustomerAddressWalkIn { get; set; }
        public string RegistrationNo { get; set; }
        public string CashCustomer { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerVat { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTelephone { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal UnRegisteredVAtAmount { get; set; }
        public string CreditCustomer { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? AreaId { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid WareHouseId { get; set; }
        public string WareHouseName { get; set; } //For showing detail purpose
        public string WareHouseNameAr { get; set; } //For showing detail purpose
        public bool IsCredit { get; set; }
        public bool DiscountType { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public virtual ICollection<SaleItemLookupModel> SaleItems { get; set; }
        public virtual SalePaymentLookupModel SalePayment { get; set; }
        public string CustomerName { get; set; } //For showing detail purpose
        public string Change { get; set; }
        public string PaymentAmount { get; set; }
        public string RefrenceNo { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public string Email { get; set; }
        public string CashCustomerId { get; set; }
        public string CashCustomerIdForeign { get; set; }
        public CashVoucherLookUpModel CashVoucher { get; set; }
        public string BarCode { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public string LogisticNameEn { get; set; }
        public string LogisticNameAr { get; set; }
        public decimal ReturnInvoiceAmount { get; set; }
        public string ReturnInvoiceRegNo { get; set; }

        public Guid? LogisticId { get; set; }
        public Guid? UnRegisteredVatId { get; set; }
        public decimal UnRegisteredRate { get; set; }

        public List<PaymentTypeLookupModel> PaymentTypes { get; set; }
        public List<PaymentVoucher> PaymentVoucher { get; set; }
        public string PartiallyInvoice { get; set; }
        public string SaleOrderNo { get; set; }
        public string QuotationNo { get; set; }
        public string DeliveryNo { get; set; }
        public string CustomerCategory { get; set; }
        public string CounterCode { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string TermAndCondition { get; set; }
        public string WarrantyLogoPath { get; set; }
        public string TermAndConditionAr { get; set; }
        public string TaxMethod { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public string HospitalName { get; set; }
        public string DoctorName { get; set; }
        public string CustomerCRN { get; set; }
        public bool IsService { get; set; }
        public Guid? TaxRateId { get; set; }
        public decimal Discount { get; set; }
        public bool IsWarranty { get; set; }
        public string ContactPerson1 { get; internal set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public string TaxRateName { get; set; }
        public string ShippingAddress { get; internal set; }
        public string AreaName { get; set; }

        public string CustomerInquiry { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string BillingAddress { get; set; }
        public string ReferredBy { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public string SaleOrderNoAndDate { get; set; }
        public string QuotationNoAndDate { get; set; }
        public string PeroformaInvoiceNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string InquiryNoAndDate { get; set; }
        public string EmployeeName { get; internal set; }
        public string DeliveryNoteAndDate { get; set; }
        public string QuotationValidUpto { get; set; }
        public string PerfomaValidUpto { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public string QRCode { get; set; }
        public string Note { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCountry { get; set; }
    }
}