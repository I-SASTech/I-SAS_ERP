using System;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries
{
    public class RunPayrollListLookUpModel
    {
        public Guid Id { get; set; }
        public string PayPlan { get; set; }
        public bool Status { get; set; }
        public string PayPeriod { get; set; }
        public string PayDate { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetSalary { get; set; }
    }
}
