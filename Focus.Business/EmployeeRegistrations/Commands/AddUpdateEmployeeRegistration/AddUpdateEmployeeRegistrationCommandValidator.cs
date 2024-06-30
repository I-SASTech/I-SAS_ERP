using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.EmployeeRegistrations.Commands.AddUpdateEmployeeRegistration
{
    public class AddUpdateEmployeeRegistrationCommandValidator : AbstractValidator<AddUpdateEmployeeRegistrationCommand>
    {
        public AddUpdateEmployeeRegistrationCommandValidator()
        {
            RuleFor(x => x.EnglishName).NotEmpty().MaximumLength(50);
        }
    }
}
