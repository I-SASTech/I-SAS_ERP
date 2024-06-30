using FluentValidation;

namespace Focus.Business.SaleOrders.Commands.AddSaleOrder
{
    public class AddSaleOrderCommandValidator:AbstractValidator<AddSaleOrderCommand>
    {
        public AddSaleOrderCommandValidator()
        {
            RuleFor(x => x.SaleOrder.SaleOrderItems).NotEmpty();
            RuleFor(x => x.SaleOrder.CustomerId).NotEmpty();
            RuleFor(x => x.SaleOrder.Date).NotEmpty();
            RuleFor(x => x.SaleOrder.RegistrationNo).NotEmpty();
        }
    }
}
