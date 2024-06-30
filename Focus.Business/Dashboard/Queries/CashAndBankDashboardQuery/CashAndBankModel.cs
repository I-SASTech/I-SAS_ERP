
using System;

namespace Focus.Business.Dashboard.Queries.CashAndBankDashboardQuery
{
   public class CashAndBankModel
    {
        public string DateInType { get; set; }
        public DateTime Date { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BankAmount { get; set; }
        public bool IsBank { get; set; }
    }
}
