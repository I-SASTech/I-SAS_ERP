using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Brands.Commands.AddUpdateBrand
{
    public class AddUpdateBrandCommandValidator : AbstractValidator<AddUpdateBrandCommand>
    {
        public AddUpdateBrandCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
