using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Citys.Commands.AddUpdateCity
{
    public class AddUpdateCityCommandValidator : AbstractValidator<AddUpdateCityCommand>
    {
        public AddUpdateCityCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
