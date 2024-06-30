using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.SaleOrders
{
    public class SaleOrderLookupModel
    {
        public Guid Id { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string RegistrationNo { get; set; }
        public string Mobile { get; set; }
        public string ClientPurchaseNo { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid CustomerId { get; set; }
        public string Refrence { get; set; }
        public string PaymentMethod { get; set; }
        public string SheduleDelivery { get; set; }
        public string Days { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public string NoteAr { get; set; }
        public bool IsActive { get; set; }
        public bool IsFreight { get; set; }
        public bool IsLabour { get; set; }
        public bool IsEditPaidInvoice { get; set; }
        public bool IsClose { get; set; }
        public bool IsQuotation { get; set; }
        public string SoInventoryReserve { get; set; }
        public virtual ICollection<SaleOrderItemLookupModel> SaleOrderItems { get; set; }
        public virtual ICollection<ImportExportItemLookUpModel> ImportExportItems { get; set; }
        public string BarCode { get;  set; }
        public string CustomerAddress { get;  set; }
        public Guid? LogisticId { get;  set; }
        public string Description { get;  set; }
        public bool IsService { get;  set; }
        public bool IsSerial { get; set; }
        public bool IsFifo { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? OrderTypeId { get; set; }
        public Guid? IncotermsId { get; set; }
        public Guid? CommodityId { get; set; }
        public string Commodities { get; set; }
        public string NatureOfCargo { get; set; }
        public string Attn { get; set; }
        public DateTime? QuotationValidDate { get; set; }
        public DateTime? FreeTimePOL { get; set; }
        public DateTime? FreeTimePOD { get; set; }
        public string For { get; set; }
        public string Subject { get; set; }
        public string Purpose { get; set; }
        public string OneTimeDescription { get; set; }
        public string Attiendie { get; set; }

        public string WarrantyLogoPath { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public bool IsRemove { get; set; }
        public bool IsSaleOrderTracking { get; set; }
        public Guid? TerminalId { get; set; }
        public string InvoicePrefix { get; set; }
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
        public string QuotationNo { get; set; }
        public string SaleOrderNo { get; set; }
        public string PeroformaInvoiceNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string InquiryNo { get; set; }
        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? DueDate { get; set; }
        public string NoteDescription { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public string InvoiceNote { get; set; }
        public decimal GrossAmounts { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public Guid? BranchId { get; set; }
        public string Reason { get; set; }


    }
}
