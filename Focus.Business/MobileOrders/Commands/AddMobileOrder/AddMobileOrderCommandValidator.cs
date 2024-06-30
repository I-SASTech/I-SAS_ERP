using FluentValidation;

namespace Focus.Business.MobileOrders.Commands.AddMobileOrder
{
    public class AddMobileOrderCommandValidator : AbstractValidator<AddMobileOrderCommand>
    {
        public AddMobileOrderCommandValidator()
        {
            RuleFor(x => x.mobileOrder.MobileOrderItemLookupModels).NotEmpty();
            RuleFor(x => x.mobileOrder.CustomerId).NotEmpty();
            RuleFor(x => x.mobileOrder.OrderNo).NotEmpty();
            RuleFor(x => x.mobileOrder.OrderDate).NotEmpty();
        }
    }
}
