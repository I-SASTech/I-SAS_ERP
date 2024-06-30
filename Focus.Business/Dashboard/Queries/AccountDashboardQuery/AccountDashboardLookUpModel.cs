using Focus.Business.Reports.IncomeStatementReport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries.AccountDashboardQuery
{
   public class AccountDashboardLookUpModel
    {
   
        public decimal Cash { get; set; }
        public decimal Banks { get; set; }
        public decimal VatPayable { get; set; }
        public decimal VatReceivable { get; set; }
        public decimal AdvancePayable { get; set; }
        public decimal AdvanceReceivable { get; set; }
        public decimal AccountReceivable { get; set; }
        public decimal AccountPayable { get; set; }
        public List<PayableReceivableLookUpModel> PayableReceivableLookUpModel { get; set; }
        public List<IncomeStatementModel> IncomeList { get; set; }
        public List<IncomeStatementModel> ExpenseList { get; set; }
        public List<IncomeAndExpenseLookUpModel> IncomesAndExpenseList { get; set; }

    }
}
