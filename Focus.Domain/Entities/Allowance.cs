using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Allowance : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid AllowanceTypeId { get; set; }
        public virtual AllowanceType AllowanceType { get; set; }
        public AmountType AmountType { get; set; }
        public TaxPlan TaxPlan { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SalaryAllowance> SalaryAllowances { get; set; }

    }
}
