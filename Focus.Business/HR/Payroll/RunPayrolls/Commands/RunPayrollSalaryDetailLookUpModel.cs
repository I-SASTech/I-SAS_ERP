using System;
using Focus.Domain.Enum;

namespace Focus.Business.HR.Payroll.RunPayrolls.Commands
{
    public class RunPayrollSalaryDetailLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string NameInPayslip { get; set; }
        public string NameInPayslipArabic { get; set; }
        public string Type { get; set; }
        public AmountType AmountType { get; set; }
        public bool TaxPlan { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public Guid RunPayrollDetailId { get; set; }
    }
}
