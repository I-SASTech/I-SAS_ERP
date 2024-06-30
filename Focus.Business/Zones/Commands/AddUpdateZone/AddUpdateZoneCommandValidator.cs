using FluentValidation;

namespace Focus.Business.Zones.Commands.AddUpdateZone
{
    public class AddUpdateZoneCommandValidator : AbstractValidator<AddUpdateZoneCommand>
    {
        public AddUpdateZoneCommandValidator()
        {
            RuleFor(z => z.Name).NotEmpty().MaximumLength(50);
            RuleFor(z => z.City).NotEmpty().MaximumLength(50);
        }
    }
}
