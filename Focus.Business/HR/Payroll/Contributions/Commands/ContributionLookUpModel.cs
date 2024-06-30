using Focus.Domain.Enum;
using System;

namespace Focus.Business.HR.Payroll.Contributions.Commands
{
    public class ContributionLookUpModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string NameInPayslip { get; set; }
        public string NameInPayslipArabic { get; set; }
        public AmountType AmountType { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
