using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.TaxRates.Commands.AddUpdateTaxRate;

namespace Focus.Business.TaxRates.Commands.AddUpdateBrand
{
    public class AddUpdateTaxRateCommandValidator : AbstractValidator<AddUpdateTaxRateCommand>
    {
        public AddUpdateTaxRateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
