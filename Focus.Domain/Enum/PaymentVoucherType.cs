
using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum PaymentVoucherType
    {
        [Display(Name = "CashReceipt")]
        CashReceipt = 1,
        [Display(Name = "BankReceipt")]
        BankReceipt = 2,
        [Display(Name = "CashPay")]
        CashPay = 3,
        [Display(Name = "BankPay")]
        BankPay = 4,
        [Display(Name = "PettyCash")]
        PettyCash = 5,
        [Display(Name = "AdvanceReceipt")]
        AdvanceReceipt = 6,
        [Display(Name = "AdvancePay")]
        AdvancePay = 7,

        [Display(Name = "AdvanceExpense")]
        AdvanceExpense = 8,
        [Display(Name = "TemporaryCashAllocation")]
        TemporaryCashAllocation = 9,
        [Display(Name = "ContractorAdvancePay")]
        ContractorAdvancePay = 10,
        [Display(Name = "AdvancePurchase")]
        AdvancePurchase = 11,
    }
}
