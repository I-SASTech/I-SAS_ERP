using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class EmployeeSalary : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string SalaryType { get; set; }
        public Guid? PayPeriodId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal WeekdayHourlySalary { get; set; }
        public decimal WeekendDayHourlySalary { get; set; }
        public Guid? SalaryTemplateId { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
        public DateTime StartingDate { get; set; }
        public bool IncomeTax { get; set; }
        public bool AutoIncomeTax { get; set; }
        public decimal GosiRate { get; set; }
        public virtual ICollection<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }

    }
}
