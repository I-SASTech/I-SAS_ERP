using FluentValidation;

namespace Focus.Business.Sizes.Commands.AddUpdateSize
{
    public class AddUpdateSizeCommandValidator : AbstractValidator<AddUpdateSizeCommand>
    {
        public AddUpdateSizeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
