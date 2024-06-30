using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Contribution : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string NameInPayslip { get; set; }
        public AmountType AmountType { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
        public string NameInPayslipArabic { get; set; }
        public virtual ICollection<SalaryContribution> SalaryContributions { get; set; }

    }
}
