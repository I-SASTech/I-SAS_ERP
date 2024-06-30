using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.BalanceSheetReport
{
   public class BalanceSheetListModel
    {
        public List<BalanceSheetModel> Asset { get; set; }
        public List<BalanceSheetModel> Liability { get; set; }
        public List<BalanceSheetModel> Equity { get; set; }
        public List<BalanceSheetComparisonLookupModel> BalanceSheetComparison { get; set; }
        public List<BalanceSheetModel> Income { get; set; }
        public List<BalanceSheetModel> Expense { get; set; }

        public decimal YearlyIncome { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal TotalEquities { get; set; }

    }
}
