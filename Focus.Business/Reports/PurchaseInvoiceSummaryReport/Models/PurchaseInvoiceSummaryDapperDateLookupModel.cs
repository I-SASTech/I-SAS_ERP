using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.PurchaseInvoiceSummaryReport.Models
{
    public class PurchaseInvoiceSummaryDapperDateLookupModel
    {
        public Guid PurchaseId { get; set; }
        public Guid SupplierId { get; set; }
        public bool IsPurchasePost { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string SuplierDisplayName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CompareWith { get; set; }
        public Guid? BranchId { get; set; }
    }
}
