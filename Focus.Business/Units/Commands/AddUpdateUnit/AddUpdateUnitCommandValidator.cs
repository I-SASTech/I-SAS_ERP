using FluentValidation;

namespace Focus.Business.Units.Commands.AddUpdateUnit
{
    public class AddUpdateUnitCommandValidator : AbstractValidator<AddUpdateUnitCommand>
    {
        public AddUpdateUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
