using System;
using System.Collections.Generic;
using Focus.Business.HR.Payroll.EmployeeSalaries.Queries;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.EmployeeSalaries.Commands
{
    public class EmployeeSalaryLookUpModel
    {
        public Guid Id { get; set; }
        public string SalaryType { get; set; }
        public Guid? PayPeriodId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal WeekdayHourlySalary { get; set; }
        public decimal WeekendDayHourlySalary { get; set; }
        public Guid? SalaryTemplateId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartingDate { get; set; }
        public bool IncomeTax { get; set; }
        public bool AutoIncomeTax { get; set; }
        public decimal GosiRate { get; set; }
        public ICollection<SalaryTaxSlab> SalaryTaxSlabList { get; set; }
        public List<SalaryDetailLookUpModel> SalaryDetailList { get; set; }
    }
}
