using Focus.Domain.Entities;
using System;
using Focus.Domain.Enum;
using System.Collections.Generic;
using NPOI.HSSF.Util;

namespace Focus.Business.Sales.Queries.GetSaleList
{
    public class SaleListLookupModel
    {
        public Guid Id { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? CustomerId { get; set; }
        public string RegistrationNumber { get; set; }
        public string InvoiceNote { get; set; }
        public string SaleOrderNo { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string CustomerNameArabic { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsCredit { get; set; }
        public bool IsCashCustomer { get; set; }
        public DateTime Date { get; set; }
        public string RefrenceNo { get; set; }
        public string CounterName { get; set; }
        public string PaymentTerms { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public string UserName { get; set; }
        public string CreatedUser { get; set; }
        public string BarCode { get; set; }
        public string InvoiceNo { get; set; }
        public bool  IsSaleReturn { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public Guid CounterId { get; set; }
        public Guid UserId { get; set; }
        public Guid ContactAccountId { get; set; }
        public string QuotationNo { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
        public string TaxMethod { get; set; }
        public decimal DueAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsService { get; set; }
        public bool IsProformaInvoice { get; set; }
        public bool MarkAsCompleted { get; set; }
        public bool IsSendMsg { get; set; }
        public bool IsDefault { get; set; }
        public bool IsMsgSended { get; set; }
        public Guid? CashCustomerId { get; set; }

        public string CanvasSaleOrderId { get; set; }
        public Guid? QutationId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? DeliveryChallanId { get; set; }
        public string CanvasDate { get; set; }
        public string Code { get; set; }
          
        public bool DiscountType { get; set; }
        public bool IsDeleted { get; set; }
        public string CanvasTaxMethod { get; set; }
        public string VAT { get; set; }
        public SaleHoldTypes SaleHoldTypes { get; set; }
        public bool IsClose { get; set; }
        public string Reason { get; set; }
        public bool IsProcessed { get; set; }
        public string BranchCode { get; set; }
        public string CustomerEmail { get; set; }

        public Guid? EditProformaId { get; set; }
        public Guid? EditDeliveryChallanId { get; set; }
        public Guid? EditPurchaseOrderId { get; set; }
    }
}
