using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Transactions.JVTransaction
{
    public class DayWiseTransactionLookupModel
    {
        public string AccountType { get; set; }
        public string CostCentre { get; set; }
        public string AccountName { get; set; }
        public string DocumentNumber { get; set; }
        public string TransactionType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Date { get; set; }
    }
}
