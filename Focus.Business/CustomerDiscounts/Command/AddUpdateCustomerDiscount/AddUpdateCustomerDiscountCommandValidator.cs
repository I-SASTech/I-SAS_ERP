using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;

namespace Focus.Business.CustomerDiscounts.Command.AddUpdateCustomerDiscount
{
    public class AddUpdateCustomerDiscountCommandValidator:AbstractValidator<AddUpdateCustomerDiscountCommand>
    {
        public AddUpdateCustomerDiscountCommandValidator()
        {
            RuleFor(c => c.DiscountName).NotEmpty().MaximumLength(100);
        }
    }
}
