using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceCostCenterLookupModel
    {
        public List<AllCostCenterLookupModel> CostCenters { get; set; }
        public List<AllCostCenterLookupModel> Assets { get; set; }
        public List<AllCostCenterLookupModel> Liabilities { get; set; }
        public List<AllCostCenterLookupModel> Equities { get; set; }
        public List<AllCostCenterLookupModel> Incomes { get; set; }
        public List<AllCostCenterLookupModel> Expenses { get; set; }
    }
}