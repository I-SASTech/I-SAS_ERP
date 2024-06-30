using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.IncomeStatementReport
{
    public class IncomeStatementModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterArabic { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeArabic { get; set; }
        public decimal Amount { get; set; }
        public decimal DAmount { get; set; }
        public decimal CAmount { get; set; }
        public List<ComparisonParameterLookupModel> ComparisonParameters { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
    }
}
