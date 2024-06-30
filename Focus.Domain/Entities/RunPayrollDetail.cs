using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class RunPayrollDetail : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public Guid EmployeeId { get; set; }
        public string SalaryType { get; set; }
        public decimal WeekendDayHourlySalary { get; set; }
        public bool IncomeTax { get; set; }
        public bool AutoIncomeTax { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal ContributionAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal NetSalary { get; set; }
        public Guid RunPayrollId { get; set; }
        public virtual RunPayroll RunPayroll { get; set; }
        public decimal WeekdayHourlySalary { get; set; }
        public decimal HourAmount { get; set; }
        public decimal Hour { get; set; }
        public bool ZeroSalary { get; set; }
        public string Reason { get; set; }
        public virtual ICollection<RunPayrollSalaryDetail> RunPayrollSalaryDetails { get; set; }

    }
}
