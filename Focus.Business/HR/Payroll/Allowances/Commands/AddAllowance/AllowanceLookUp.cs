using System;
using Focus.Domain.Enum;

namespace Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance
{
    public class AllowanceLookUp
    {
        public Guid? Id { get; set; }
        public Guid AllowanceTypeId { get; set; }
        public string AllowanceNameEn { get; set; }
        public string AllowanceNameAr { get; set; }
        public AmountType AmountType { get; set; }
        public TaxPlan TaxPlan { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
