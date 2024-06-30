using System.Collections.Generic;
using Focus.Business.Transactions.Commands;

namespace Focus.Business.Transactions.JVTransaction
{
    public class AccountLedgerReportLookUp
    {
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<TransactionDto> TransactionList { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
    }
}
