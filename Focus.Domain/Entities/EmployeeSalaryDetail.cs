using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class EmployeeSalaryDetail : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public Guid ItemId { get; set; }
        public string Type { get; set; }
        public AmountType AmountType { get; set; }
        public bool TaxPlan { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public Guid EmployeeSalaryId { get; set; }
        public virtual EmployeeSalary EmployeeSalary { get; set; }
    }
}
