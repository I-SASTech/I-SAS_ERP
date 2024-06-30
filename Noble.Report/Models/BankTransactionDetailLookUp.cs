using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class BankTransactionDetailLookUp
    {
        public string BankName { get; set; }
        public string BankNameArabic { get; set; }
        public decimal Amount { get; set; }
    }
}