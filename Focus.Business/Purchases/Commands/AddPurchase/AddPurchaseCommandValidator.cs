using FluentValidation;

namespace Focus.Business.Purchases.Commands.AddPurchase
{
    public class AddPurchaseCommandValidator:AbstractValidator<AddPurchaseCommand>
    {
        public AddPurchaseCommandValidator()
        {
            RuleFor(x => x.Purchase.PurchaseItems).NotEmpty();
            RuleFor(x => x.Purchase.SupplierId).NotEmpty();
            RuleFor(x => x.Purchase.WareHouseId).NotEmpty();
            RuleFor(x => x.Purchase.Date).NotEmpty();
            RuleFor(x => x.Purchase.RegistrationNo).NotEmpty();
        }
    }
}
