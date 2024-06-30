using System.Collections.Generic;

namespace Focus.Business.Transactions.JVTransaction
{
    public class BankTransactionLookUpModel
    {
        public List<BankLookUpModel> BankLook { get; set; }
        public decimal Opening { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal RunningBalance { get; set; }
    }
}
