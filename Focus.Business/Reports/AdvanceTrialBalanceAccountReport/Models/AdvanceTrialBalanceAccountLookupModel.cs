using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Models
{
    public class AdvanceTrialBalanceAccountLookupModel
    { 
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string CostCenter { get; set; }
        public string AccountType { get; set; }
        public string CompareWith { get; set; }
        public string CompairWithReport { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
        public List<AdvanceTrialBalanceAccountLookupModel> CompareWithList { get; set; }

    }
}
