using System;
using System.Collections.Generic;

namespace Focus.Business.FinancialYearTemporaryClosing.Models
{
    public class FinancialYearClosingLookUpModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string PeriodName { get; set; }
        public DateTime Date { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<CostCenterListModel> CostCenterList { get; set; }
    }
}
