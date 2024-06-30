using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class CashVoucherLookUpModel
    {
        public Guid Id { get; set; }
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public Guid? SaleReturnId { get; set; }
        public Guid? SaleInvoiceId { get; set; }
    }
}