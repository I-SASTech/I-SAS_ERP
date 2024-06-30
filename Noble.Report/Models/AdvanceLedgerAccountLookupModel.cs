using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceLedgerAccountLookupModel
    {
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<TransactionDto> TransactionList { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public List<AdvanceLedgerAccountLookupModel> CompareWithList { get; set; }
        public string CompareWith { get; set; }
    }
}