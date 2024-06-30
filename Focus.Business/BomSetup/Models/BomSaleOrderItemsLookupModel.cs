using Focus.Business.SaleOrders;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.BomSetup.Models
{
    public class BomSaleOrderItemsLookupModel
    {
        public Guid Id { get; set; }
        public string TaxRateName { get; set; }
        public string For { get; set; }
        public string Subject { get; set; }
        public string Purpose { get; set; }
        public string OneTimeDescription { get; set; }
        public string Attiendie { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string WarrantyLogoPath { get; set; }
        public string Dates { get; set; }
        public string RegistrationNo { get; set; }
        public Guid CustomerId { get; set; }
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public string Refrence { get; set; }
        public string PaymentMethod { get; set; }
        public string SheduleDelivery { get; set; }
        public string Days { get; set; }
        public string CustomerCode { get; set; }
        public bool IsFreight { get; set; }
        public bool IsLabour { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsQuotation { get; set; }
        public string ClientPurchaseNo { get; set; }
        public Guid? QuotationId { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameEn { get; set; }
        public string QuotationNo { get; set; }
        public string Email { get; set; }
        public List<SaleOrderItemLookupModel> SaleOrderItems { get; set; }
        public Guid? WareHouseId { get; set; }
        public string WareHouseName { get; set; }
        public string WareHouseNameAr { get; set; }
        public Guid? CustomerAccountId { get; set; }
        public string BarCode { get; set; }
        public string QRCode { get; set; }
        public Guid? LogisticId { get; set; }
        public string CustomerVat { get; set; }
        public string CustomerAddress { get; set; }
        public string LogisticNameEn { get; set; }
        public string LogisticNameAr { get; set; }
        public string Mobile { get; set; }
        public string CustomerBilingAddress { get; set; }
        public string CustomerShippingAddress { get; set; }
        public Guid? AdvanceAccountId { get; set; }
        public bool IsService { get; set; }
        public string Description { get; set; }
        public string NoteAr { get; set; }
        public Guid? OrderTypeId { get; set; }
        public Guid? IncotermsId { get; set; }
        public Guid? CommodityId { get; set; }
        public string Commodities { get; set; }
        public string NatureOfCargo { get; set; }
        public string Attn { get; set; }
        public DateTime QuotationValidDate { get; set; }
        public DateTime? FreeTimePOL { get; set; }
        public DateTime? FreeTimePOD { get; set; }
        public string OrderTypeName { get; set; }
        public string OrderTypeNameAr { get; set; }
        public string IncotermsName { get; set; }
        public string IncotermsNameAr { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public string DeliveryNoteAndDate { get; set; }
        public string QuotationValidUpto { get; set; }
        public string PerfomaValidUpto { get; set; }
        public string CustomerInquiry { get; set; }
        public string PoNumber { get; set; }
        public DateTime? PoDate { get; set; }
        public string ReferredBy { get; set; }
        public string RefrenceNo { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string QuotationNoAndDate { get; set; }
        public string SaleOrderNo { get; set; }
        public string PeroformaInvoiceNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string inquiryNoAndDate { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? DueDate { get; set; }
        public string NoteDescription { get; set; }
        public string SaleOrderNoAndDate { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string InvoiceNote { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
