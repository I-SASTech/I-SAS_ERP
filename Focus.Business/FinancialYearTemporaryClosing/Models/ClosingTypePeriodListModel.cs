using System;

namespace Focus.Business.FinancialYearTemporaryClosing.Models
{
    public class ClosingTypePeriodListModel
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsClose { get; set; }
        public bool IsSelect { get; set; }
    }
}
