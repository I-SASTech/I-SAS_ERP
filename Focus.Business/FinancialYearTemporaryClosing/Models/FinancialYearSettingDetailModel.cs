using System;
using System.Collections.Generic;

namespace Focus.Business.FinancialYearTemporaryClosing.Models
{
    public class FinancialYearSettingDetailModel
    {
        public Guid Id { get; set; }
        public string ClosingType { get; set; }
        public bool IsAutoClosing { get; set; }
        public List<ClosingTypePeriodListModel> ClosingTypePeriodList { get; set; }
    }
}
