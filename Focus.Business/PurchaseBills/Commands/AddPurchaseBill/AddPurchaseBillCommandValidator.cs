using FluentValidation;

namespace Focus.Business.PurchaseBills.Commands.AddPurchaseBill
{
    public class AddPurchaseBillCommandValidator:AbstractValidator<AddPurchaseBillCommand>
    {
        public AddPurchaseBillCommandValidator()
        {
            RuleFor(x => x.PurchaseBill.PurchaseBillItems).NotEmpty();
            RuleFor(x => x.PurchaseBill.Date).NotEmpty();
            RuleFor(x => x.PurchaseBill.RegistrationNo).NotEmpty();
        }
    }
}
