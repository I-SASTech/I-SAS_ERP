using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class SaleOrder : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid CustomerId { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }

        public virtual Contact Customer { get; set; }
        public string Refrence { get; set; }
        public string PaymentMethod { get; set; }
        public string SheduleDelivery { get; set; }
        public string Days { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public string NoteAr { get; set; }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsFreight { get; set; }
        public bool IsLabour { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
        public virtual ICollection<ProductionBatch> ProductionBatchs { get; set; }
        public string InvoiceNote { get; set; }
        public virtual ICollection<DispatchNote> DispatchNotes { get; set; }
        public virtual ICollection<SaleOrderPayment> SaleOrderPayments { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Sale> QuotationSales { get; set; }
        public virtual ICollection<DeliveryChallan> DeliveryChallansForSaleOrders { get; set; }
        public string ClientPurchaseNo { get; set; }
        public bool IsQuotation { get; set; }
        public Guid? LogisticId { get; set; }
        public virtual Logistic Logistic { get; set; }
        public string CustomerAddress { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<SaleInvoiceAdvancePayment> SaleInvoiceAdvancePayments { get; set; }
        public virtual ICollection<ImportExportItem> ImportExportItems { get; set; }
        public bool IsService { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
       
        public Guid? OrderTypeId { get; set; }
        public virtual ImportExportType OrderType { get; set; } 
        public Guid? IncotermsId { get; set; }
        public virtual ImportExportType Incoterms { get; set; }
        public Guid? CommodityId { get; set; }
        public virtual ImportExportType Commodity { get; set; }
        public string Commodities { get; set; }
        public string NatureOfCargo { get; set; }
        public string Attn { get; set; }
        public string Attiendie { get; set; }
        public string For { get; set; }
        public string Subject { get; set; }
        public string Purpose { get; set; }
        public string OneTimeDescription { get; set; }
        public DateTime QuotationValidDate { get; set; }
        public DateTime? FreeTimePOL { get; set; }
        public DateTime? FreeTimePOD { get; set; }
        public virtual ICollection<SaleOrderVersion> SaleOrderVersions { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public string WarrantyLogoPath { get; set; }

        public decimal Discount { get; set; }
        public decimal TransactionLevelDiscount { get; set; }
        public bool IsDiscountOnTransaction { get; set; }
        public bool IsFixed { get; set; }
        public bool IsBeforeTax { get; set; }
        public Guid? CreatedBy { get; set; }

        public Guid? TerminalId { get; set; }

        public bool IsSaleOrderTracking { get; set; }

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
        public Guid? SaleOrderId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? InquiryId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public string NoteDescription { get; set; }
        public bool IsProcessed { get; set; }
        public string ProcessedNote { get; set; }
        public decimal TotalAfterDiscount { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsUsedForBom { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}