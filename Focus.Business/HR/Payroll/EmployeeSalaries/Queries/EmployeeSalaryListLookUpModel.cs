using System;
using System.Collections.Generic;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.EmployeeSalaries.Queries
{
    public class EmployeeSalaryListLookUpModel
    {
        public Guid Id { get; set; }
        public string EmployeeArabicName { get; set; }
        public string EmployeeEnglishName { get; set; }
        public string SalaryType { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal ContributionAmount { get; set; }
        public decimal NetSalary { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal IncomeTaxAmount { get; set; }
        public decimal WeekendDayHourlySalary { get; set; }
        public decimal WeekdayHourlySalary { get; set; }
        public bool IncomeTax { get; set; }
        public bool AutoIncomeTax { get; set; }
        public decimal LoanAmount { get; set; }
        public List<SalaryDetailLookUpModel> SalaryDetailList { get; set; }
        public ICollection<SalaryTaxSlab> SalaryTaxSlabList { get; set; }
    }
}
