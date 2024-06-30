using Focus.Domain.Enum;
using System;

namespace Focus.Business.HR.Payroll.LoanPays.Commands
{
    public class LoanPayLookUpModel
    {
        public Guid? Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string RecoveryDate { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingLoan { get; set; }

        public string Comments { get; set; }
        public Guid? LoanPaymentId { get; set; }
    }
}
