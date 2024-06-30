using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class IncomeStatementListModel
    {
        public List<IncomeStatementModel> Income { get; set; }
        public List<IncomeStatementModel> Expenses { get; set; }

    }
}