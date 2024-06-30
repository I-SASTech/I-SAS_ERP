using System;
using System.Collections.Generic;

namespace Focus.Business.Dashboard.Queries
{
    public class YearlyTransactionLookupModel
    {
        public int MonthName { get; set; }
        public decimal Amount { get; set; }
        public List<TransactionGenericLookupModel> TransactionGeneric { get; set; }
    }
}
