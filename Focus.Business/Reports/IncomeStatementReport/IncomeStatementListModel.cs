using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.IncomeStatementReport
{
    public class IncomeStatementListModel
    {
        public List<IncomeStatementModel> Income { get; set; }
        public List<IncomeStatementModel> Expenses { get; set; }

    }
}