using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.Transporters.Commands.AddUpdateTransporter
{
   public class AddUpdateTransporterCommandValidator : AbstractValidator<AddUpdateTransporterCommand>
    {
        public AddUpdateTransporterCommandValidator()
        {
            RuleFor(z => z.Name).NotEmpty().MaximumLength(50);
            RuleFor(z => z.City).NotEmpty().MaximumLength(50);
        }
    }
}
