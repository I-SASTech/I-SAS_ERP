using FluentValidation;

namespace Focus.Business.Contacts.Commands.AddUpdateContact
{
    public class AddUpdateContactCommandValidator:AbstractValidator<AddUpdateContactCommand>
    {
        public AddUpdateContactCommandValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            //RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
