using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.FinancialYears.Queries.GetCurrentYear
{
  public  class PeriodModel
  {
      public List<string> YearToList { get; set; }
      public List<FinancialYearGroupByModel> FinancialYearList { get; set; }
      public string MonthName { get; set; }

  }
}
