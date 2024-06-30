using FluentValidation;

namespace Focus.Business.Accounting.Commands.AddUpdateAccount
{
    public class AddUpdateAccountCommandValidator : AbstractValidator<AddUpdateAccountCommand>
    {
        public AddUpdateAccountCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(100);
            RuleFor(x => x.IsActive);
        }
    }
}