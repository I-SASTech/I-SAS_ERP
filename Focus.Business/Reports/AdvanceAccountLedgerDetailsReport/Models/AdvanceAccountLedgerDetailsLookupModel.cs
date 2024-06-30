using Focus.Business.Reports.TrialBalanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceAccountLedgerDetailsReport.Models
{
    public class AdvanceAccountLedgerDetailsLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CompareWith { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Total { get; set; }
        public decimal Opening { get; set; }
        public decimal Closing { get; set; }

        public DateTime Date { get; set; }
        public List<TrialBalanceModel> TrialBalanceModel { get; set; }
        public List<AdvanceAccountLedgerDetailsLookupModel> CompareWithList { get; set; }
    }
}
