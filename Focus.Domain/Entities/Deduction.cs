using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Deduction : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string NameInPayslip { get; set; }
        public string NameInPayslipArabic { get; set; }
        public AmountType AmountType { get; set; }
        public TaxPlan TaxPlan { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<SalaryDeduction> SalaryDeductions { get; set; }

    }
}
