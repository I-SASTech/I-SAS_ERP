using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class SalaryDeduction : BaseEntity
    {
        public Guid? SalaryTemplateId { get; set; }
        public virtual SalaryTemplate SalaryTemplate { get; set; }
        public Guid? DeductionId { get; set; }
        public virtual Deduction Deduction { get; set; }
        public AmountType AmountType { get; set; }
        public TaxPlan TaxPlan { get; set; }
        public decimal Amount { get; set; }
    }
}
