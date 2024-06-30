using System;

namespace Focus.Business.Reports.AdvanceBalanceSheet
{
    public class IncomeStatementLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterArabic { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeArabic { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
