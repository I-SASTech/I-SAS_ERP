using System;

namespace Focus.Business.FinancialYearSettings.Models
{
    public class FinancialYearSettingModel
    {
        public Guid Id { get; set; }
        public string ClosingType { get; set; }
        public bool IsAutoClosing { get; set; }
    }
}
