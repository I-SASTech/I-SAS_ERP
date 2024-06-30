using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceSalesInvoiceLookupModel
    {
        public Guid SaleId { get; set; }
        public Guid CustomerId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public bool SaleReturnPost { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string CustomerDisplayName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CompareWith { get; set; }
    }


}