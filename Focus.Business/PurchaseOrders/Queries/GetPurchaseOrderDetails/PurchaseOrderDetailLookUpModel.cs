using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Focus.Business.Attachments.Commands;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderDetails
{
    public class PurchaseOrderDetailLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string QrCode { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public string Prefix { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Note { get; set; }
        public string SupplierVAT { get; set; }
        public bool IsClose { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string Path { get; set; }
        public bool IsRaw { get; internal set; }
        public virtual ICollection<PurchaseAttachment> PurchaseAttachments { get; set; }
        public virtual ICollection<PurchaseOrderItemLookupModel> PurchaseOrderItems { get; set; }
        public virtual ICollection<ActionLookUpModel> ActionProcess { get; set; }
        public virtual ICollection<PaymentVoucherLookUpModel> PaymentVoucher { get; set; }
        public List<Domain.Entities.PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        public string TaxRatesName { get; set; }
        public string SupplierName { get; set; }
        public Guid? AdvanceAccountId { get; set; }
        public string Version { get; set; }
        public decimal ExpenseUse { get; set; }
        public List<PurchaseOrderVersion> PurchaseOrderVersion { get; set; }
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
        

        public static Expression<Func<PurchaseOrder, PurchaseOrderDetailLookUpModel>> Projection
        {
            get
            {
                return department => new PurchaseOrderDetailLookUpModel
                {
                    Id = department.Id,
                    Date = department.Date,
                    RegistrationNo = department.RegistrationNo,
                    SupplierId = department.SupplierId,
                    InvoiceNo = department.InvoiceNo,
                    InvoiceDate = department.InvoiceDate,
                    Note = department.Note
                };
            }
        }

        public string DocumentType { get;  set; }
        public Guid? SupplierQuotationId { get; set; }
        public decimal TotalAfterDiscount { get;  set; }

        public static PurchaseOrderDetailLookUpModel Create(PurchaseOrder purchaseOrder)
        {
            return Projection.Compile().Invoke(purchaseOrder);
        }

        public string TaxRateName { get; set; }
        public string SupplierCRN { get; set; }
        public string CustomerAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string SupplierCode { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }

        public Guid? DeliveryId { get; set; }
        public Guid? ShippingId { get; set; }
        public Guid? BillingId { get; set; }
        public Guid? NationalId { get; set; }
    }
}
