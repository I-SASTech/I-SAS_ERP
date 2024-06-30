using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.RunPayrolls
{
    public class PayrollBodyModel
    {
        public int Sr { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }
        public decimal BaseSalary { get; set; }
        public List<AllowancebodyModel> AllowanceList { get; set; }
        public decimal IncomeTaxAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal NetSalary { get; set; }
    }
}
