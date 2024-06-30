using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DenominationSetups.Commands.AddUpdateDenominationSetup
{
    public class AddUpdateDenominationSetupCommandValidator : AbstractValidator<AddUpdateDenominationSetupCommand>
    {
        public AddUpdateDenominationSetupCommandValidator()
        {
            //RuleFor(x => x.Number).NotEmpty().MaximumLength(100);
        }
    }
}
