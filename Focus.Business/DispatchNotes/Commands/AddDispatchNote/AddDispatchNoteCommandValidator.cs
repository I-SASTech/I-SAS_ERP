using FluentValidation;

namespace Focus.Business.DispatchNotes.Commands.AddDispatchNote
{
    public class AddDispatchNoteCommandValidator:AbstractValidator<AddDispatchNoteCommand>
    {
        public AddDispatchNoteCommandValidator()
        {
            RuleFor(x => x.DispatchNote.DispatchNoteItems).NotEmpty();
            RuleFor(x => x.DispatchNote.CustomerId).NotEmpty();
            RuleFor(x => x.DispatchNote.Date).NotEmpty();
            RuleFor(x => x.DispatchNote.RegistrationNo).NotEmpty();
        }
    }
}
