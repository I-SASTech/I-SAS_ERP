using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class SaleInvoiceModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerVat { get; set; }
        public string EnglishName { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public DocumentType DocumentType { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public string TaxMethod { get; set; }
        public string ArabicName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameArabic { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalWithOutAmount { get; set; }
        public decimal AmountwithDiscount { get; set; }
        public decimal VATamount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal OverAllDiscount { get; set; }
        public decimal OverAllUnRegisterVAT { get; set; }
    }
    public enum DocumentType
    {
        SaleInvoice = 1,
        SaleReturn = 2,
        ProformaInvoice = 3,
        GlobalInvoice = 4,
        ReceiptInvoice = 5,
        SaleOrder = 6,
        SaleQuotation = 7,
        SaleInvoiceHold = 8,
        CreditNote = 9,
        ServiceQuotation = 10,
        ServiceSaleOrder = 11,
        ServiceProformaInvoice = 12,
        PurchaseOrder = 13,

    }
}