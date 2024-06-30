using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceCustomerLedgerLookupModel
    {
        public string CompareWith { get; set; }
        public string ContactCode { get; set; }
        public string ContactName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DocumentDate { get; set; }
        public string VatNo { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public bool IsCustomer { get; set; }
        public decimal Amount { get; set; }
        public List<AdvanceCustomerLedgerLookupModel> CompareWithList { get; set; }
    }
}