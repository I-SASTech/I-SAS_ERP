using System.Collections.Generic;

namespace Focus.Business.FinancialYears.Queries.GetCurrentYear
{
    public class FinancialYearGroupByModel
    {
        public string Year { get; set; }
        public bool IsYearlyPeriodClosed { get; set; }
        public List<CompanySubmissionVmodel> CompanySubmissionPeriod { get; set; }
    }
}
