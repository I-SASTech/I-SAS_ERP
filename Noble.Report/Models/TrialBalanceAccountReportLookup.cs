using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class TrialBalanceAccountReportLookup
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string CostCenter { get; set; }
        public string AccountType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Total { get; set; }
    }
}