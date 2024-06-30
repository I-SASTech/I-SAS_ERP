using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.SaleInvoiceReport
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
        public decimal AmountwithDiscount{ get; set; }
        public decimal VATamount { get; set; }
        public decimal TotalAmount{ get; set; }
        public decimal Amount{ get; set; }
        public decimal OverAllDiscount{ get; set; }
        public decimal OverAllUnRegisterVAT{ get; set; }
        public Guid? BranchId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? SupplierId { get; set; }
    }
}
