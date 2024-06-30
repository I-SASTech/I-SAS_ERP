using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.AdvanceTrialBalanceReport.Models
{
    public class AdvanceTrialBalanceLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CompareWith { get; set; }
        public string CompareWithReport { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public decimal Debit { get; set; }
        public decimal Total { get; set; }
        public decimal Credit { get; set; }
        public DateTime Date { get; set; }
        public List<AdvanceTrialBalanceLookupModel> ListOfAdvanceTrialBalances { get; set; }
        public List<TrialBalanceLookupModel> TrialBalance { get; set; }
        public string CompareWithValue { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public Guid? BranchId { get; set; }
    }
}
