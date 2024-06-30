using FluentValidation;

namespace Focus.Business.PurchaseOrders.Commands.AddPurchaseOrder
{
    public class AddPurchaseOrderCommandValidator:AbstractValidator<AddPurchaseOrderCommand>
    {
        public AddPurchaseOrderCommandValidator()
        {
            RuleFor(x => x.PurchaseOrder.PurchaseOrderItems).NotEmpty();
            RuleFor(x => x.PurchaseOrder.SupplierId).NotEmpty();
            RuleFor(x => x.PurchaseOrder.Date).NotEmpty();
            RuleFor(x => x.PurchaseOrder.RegistrationNo).NotEmpty();
        }
    }
}
