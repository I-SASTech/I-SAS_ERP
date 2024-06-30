using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class LoanPay : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
      
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingLoan { get; set; }
        public string Comments { get; set; }
        public Guid? LoanPaymentId { get; set; }
        public virtual LoanPayment LoanPayment { get; set; }


    }
}
