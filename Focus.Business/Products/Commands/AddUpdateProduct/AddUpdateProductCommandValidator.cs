using FluentValidation;

namespace Focus.Business.Products.Commands.AddUpdateProduct
{
    public class AddUpdateProductCommandValidator : AbstractValidator<AddUpdateProductCommand>
    {
        public AddUpdateProductCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.EnglishName).MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
