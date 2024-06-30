using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceTrialBalanceCompLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CompareWith { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public decimal Debit { get; set; }
        public decimal Total { get; set; }
        public decimal Credit { get; set; }
        public DateTime Date { get; set; }
        public List<TrialBalanceLookupModel> TrialBalance { get; set; }
        public string CompareWithValue { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
    }
}