using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.StockAdjustments.Commands.AddUpdateStockAdjustment
{
    public class AddUpdateStockAdjustmentCommandValidator : AbstractValidator<AddUpdateStockAdjustmentCommand>
    {
        public AddUpdateStockAdjustmentCommandValidator()
        {
            RuleFor(j => j.Narration).MaximumLength(200);
            RuleFor(j => j.Date);
        }
    }
}
