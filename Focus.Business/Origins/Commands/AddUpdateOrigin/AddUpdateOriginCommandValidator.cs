using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.Origins.Commands.AddUpdateOrigin;

namespace Focus.Business.Origins.Commands.AddUpdateBrand
{
    public class AddUpdateOriginCommandValidator : AbstractValidator<AddUpdateOriginCommand>
    {
        public AddUpdateOriginCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
