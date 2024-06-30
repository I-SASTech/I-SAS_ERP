using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PriceLabelings.Commands.AddUpdatePriceLabeling
{
    public class AddUpdatePriceLabelingCommandValidator : AbstractValidator<AddUpdatePriceLabelingCommand>
    {
        public AddUpdatePriceLabelingCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
