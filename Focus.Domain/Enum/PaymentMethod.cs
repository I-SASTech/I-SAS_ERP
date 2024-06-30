using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
   public enum PaymentMethod
    {
        [Display(Name = "Default")]
        Default = 0,
        [Display(Name = "Cheque")]
        Cheque = 1,
        [Display(Name = "Transfer")]
        Transfer = 2,
        [Display(Name = "Deposit")]
        Deposit = 3,
        [Display(Name = "DebitCard")]
        DebitCard = 4,
        [Display(Name = "CreditCard")]
        CreditCard = 5,
        [Display(Name = "Card")]
        Card = 6,
        [Display(Name = "Int. Transfer")]
        IntTransfer = 7,
        [Display(Name = "Credit Card Master")]
        CreditCardMaster = 8,
        [Display(Name = "Credit Card Visa")]
        CreditCardVisa = 9,
        [Display(Name = "American Express")]
        AmericanExpress = 10,
    }
}
