using System;
using System.Collections.Generic;
using Focus.Business.HR.Payroll.EmployeeSalaries.Queries;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.RunPayrolls.Commands
{
    public class RunPayrollDetailLookUpModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeArabicName { get; set; }
        public string EmployeeEnglishName { get; set; }
        public decimal BaseSalary { get; set; }
        public string SalaryType { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal Hour { get; set; }
        public decimal HourAmount { get; set; }
        public decimal WeekdayHourlySalary { get; set; }
        public decimal WeekendDayHourlySalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal ContributionAmount { get; set; }
        public decimal IncomeTaxAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal NetSalary { get; set; }
        public Guid RunPayrollId { get; set; }
        public bool ZeroSalary { get; set; }
        public string Reason { get; set; }
        public bool IncomeTax { get; set; }
        public bool AutoIncomeTax { get; set; }
        public List<SalaryDetailLookUpModel> SalaryDetailList { get; set; }
        public ICollection<SalaryTaxSlab> SalaryTaxSlabList { get; set; }

    }
}
