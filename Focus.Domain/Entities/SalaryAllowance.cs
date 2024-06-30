using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
   public class SalaryAllowance : BaseEntity
   {
       public Guid? SalaryTemplateId { get; set; }
       public virtual SalaryTemplate SalaryTemplate { get; set; }
       public AmountType AmountType { get; set; }
       public TaxPlan TaxPlan { get; set; }
       public decimal Amount { get; set; }
       public Guid? AllowanceId { get; set; }
       public virtual Allowance Allowance { get; set; }

    }
}
