using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Transactions.JVTransaction
{
  public  class BankLookUpModel
    {
        public string Date { get; set; }
        public string TransactionType { get; set; }
        public string DocumentNo { get; set; }
        public string BankName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit { get;  set; }
        public decimal Credit { get;  set; }
    }
}
