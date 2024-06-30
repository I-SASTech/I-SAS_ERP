using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class LoanPayment : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid EmployeeRegistrationId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public string Description { get; set; }
        public LoanType LoanType { get; set; }
        public RecoveryMethod RecoveryMethod { get; set; }
        public AmountType InstallmentMethod { get; set; }
        public DateTime LoanTakenDate { get; set; }
        public DateTime PaymentStartDate { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal RemainingLoan { get; set; }
        public decimal Payment { get; set; }

        public decimal EmployeeSalary { get; set; }
        public decimal DeductionValue { get; set; }
        public decimal RecoveryLoanAmount { get; set; }
        public decimal InstallmentBaseSalary { get; set; }
        public bool IsActive { get; set; }
        public bool ProvidentFundType { get; set; }
        public virtual ICollection<LoanPay> LoanPays { get; set; }


    }
}
