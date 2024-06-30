using System.Collections.Generic;

namespace Focus.Business.FinancialYearTemporaryClosing.Models
{
    public class CostCenterListModel
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public List<AccountListModel> AccountList { get; set; }
    }
}
