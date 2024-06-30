using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Transactions.JVTransaction
{
    public class DayWiseHeaderLookUp
    {

        public string Header { get; set; }
        public List<DayWiseTransactionLookupModel> DayWiseTransactionLookup { get; set; }
        public decimal Total { get; set; }
    }
}
