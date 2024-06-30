using FluentValidation;

namespace Focus.Business.ProductMasters.Commands.AddUpdateProductMaster
{
    public class AddUpdateProductMasterCommandValidator : AbstractValidator<AddUpdateProductMasterCommand>
    {
        public AddUpdateProductMasterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
