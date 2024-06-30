using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PaymentOptions.Commands.AddUpdatePaymentOptions
{
    public class AddUpdatePaymentOptionsCommandValidator : AbstractValidator<AddUpdatePaymentOptionsCommand>
    {
        public AddUpdatePaymentOptionsCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
