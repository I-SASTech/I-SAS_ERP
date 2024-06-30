using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Regions.Commands.AddUpdateRegion
{
    public class AddUpdateRegionCommandValidator : AbstractValidator<AddUpdateRegionCommand>
    {
        public AddUpdateRegionCommandValidator()
        {
            RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
