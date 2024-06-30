using System;
using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries.RunPayrollPrintQuery
{
    public class RunPayrollPrintLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid PayrollScheduleId { get; set; }
        public bool Status { get; set; }
        public bool Hourly { get; set; }
        public List<RunPayrollDetailPrintLookUpModel> SalaryTemplateList { get; set; }
        public List<AllowanceListModel> AllowanceHeader { get; set; }
        public List<ContributionListModel> ContributionHeader { get; set; }
        public List<DeductionListModel> DeductionHeader { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalNetSalary { get; set; }
        public decimal TotalIncomeTaxAmount { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal TotalHourAmount { get; set; }
        public List<AllowanceListModel> AllowanceFooter { get; set; }
        public List<DeductionListModel> DeductionFooter { get; set; }
        public List<ContributionListModel> ContributionFooter { get; set; }
        public string PayrollSchedule { get; set; }
    }
}
