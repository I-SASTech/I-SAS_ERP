using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
  public  class SalaryContribution : BaseEntity
    {
        public Guid? SalaryTemplateId { get; set; }
        public virtual SalaryTemplate SalaryTemplate { get; set; }
        public Guid? ContributionId { get; set; }
        public virtual Contribution Contribution { get; set; }
        public AmountType AmountType { get; set; }
        public decimal Amount { get; set; }
    }
}
