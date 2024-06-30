using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using Focus.Business.HR.Payroll.LoanPays.Commands;

namespace Focus.Business.HR.Payroll.LoanPayments.Commands
{
    public class LoanPaymentLookUpModel
    {
        public Guid? Id { get; set; }
        public Guid EmployeeRegistrationId { get; set; }
        public string Description { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNameArabic { get; set; }
        public LoanType LoanType { get; set; }
        public RecoveryMethod RecoveryMethod { get; set; }
        public AmountType InstallmentMethod { get; set; }
        public DateTime LoanTakenDate { get; set; }
        public string LoanDate { get; set; }
        public DateTime PaymentStartDate { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Payment { get; set; }
        public decimal EmployeeSalary { get; set; }
        public decimal DeductionValue { get; set; }
        public decimal RecoveryLoanAmount { get; set; }
        public decimal InstallmentBaseSalary { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<LoanPayLookUpModel> LoanPays { get; set; }
        public bool ProvidentFundType { get;  set; }
        public decimal RemainingLoan { get;  set; }
    }
}
