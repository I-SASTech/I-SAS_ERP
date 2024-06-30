
using System;

namespace Focus.Business.Dashboard.Queries.AccountDashboardQuery
{
   public class TransactionDashboardLookUpModel
    {
        public string Code { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterArabic { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeArabic { get; set; }

        public DateTime Date { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
