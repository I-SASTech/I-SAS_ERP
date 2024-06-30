using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Logistics.Commands.AddUpdateLogistic
{
    public class AddUpdateLogisticCommandValidator : AbstractValidator<AddUpdateLogisticCommand>
    {
        public AddUpdateLogisticCommandValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Address).MaximumLength(500);
        }
    }
}
