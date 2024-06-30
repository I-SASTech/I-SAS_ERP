using System.Collections.Generic;

namespace Focus.Business.CostCenters.Models
{
    public class CostCenterLookupModel
    {   
        public List<AllCostCenterLookupModel> CostCenters { get; set; }
        public List<AssetLookupModel> Assets { get; set; }
        public List<LiabilitiesLookupModel> Liabilities { get; set; }
        public List<EquityLookupModel> Equities { get; set; }
        public List<IncomeLookupModel> Incomes { get; set; }
        public List<ExpensesLookupModel> Expenses { get; set; }
    }
}
