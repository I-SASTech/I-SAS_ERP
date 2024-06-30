using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.Designations.Commands.AddUpdateDesignation
{
    public class AddUpdateDesignationCommandValidator : AbstractValidator<AddUpdateDesignationCommand>
    {
        public AddUpdateDesignationCommandValidator()
        {
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}
