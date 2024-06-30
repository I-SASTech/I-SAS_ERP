using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceBalanceSheet
{
    public class AdvanceBalanceSheetLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterArabic { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeArabic { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public string Year3 { get; set; }
        public string Year4 { get; set; }
        public string Year5 { get; set; }
        public string Month1 { get; set; }
        public string Month2 { get; set; }
        public string Month3 { get; set; }
        public string Month4 { get; set; }
        public string Month5 { get; set; }
        public string Month6 { get; set; }
        public string Month7 { get; set; }
        public string Month8 { get; set; }
        public string Month9 { get; set; }
        public string Month10 { get; set; }
        public string Month11 { get; set; }
        public string Month12 { get; set; }
        public List<DaysLookupModel> Days { get; set; } 
    }
}
