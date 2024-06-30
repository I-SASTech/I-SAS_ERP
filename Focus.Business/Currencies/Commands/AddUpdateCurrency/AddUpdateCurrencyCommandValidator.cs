using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Currencies.Commands.AddUpdateCurrency
{
    public class AddUpdateCurrencyCommandValidator : AbstractValidator<AddUpdateCurrencyCommand>
    {
        public AddUpdateCurrencyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
