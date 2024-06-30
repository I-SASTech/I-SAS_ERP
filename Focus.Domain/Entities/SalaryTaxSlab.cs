using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SalaryTaxSlab : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public decimal IncomeFrom { get; set; }
        public decimal IncomeTo { get; set; }
        public decimal FixedTax { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public Guid? TaxSalaryId { get; set; }
        public virtual TaxSalary TaxSalary { get; set; }

    }
}
