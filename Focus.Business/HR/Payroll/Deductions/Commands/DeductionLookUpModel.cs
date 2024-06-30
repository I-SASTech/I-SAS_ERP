using Focus.Domain.Enum;
using System;

namespace Focus.Business.HR.Payroll.Deductions.Commands
{
    public class DeductionLookUpModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string NameInPayslip { get; set; }
        public string NameInPayslipArabic { get; set; }
        public AmountType AmountType { get; set; }
        public TaxPlan TaxPlan { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
