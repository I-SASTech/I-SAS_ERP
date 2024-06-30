using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.BundleCategoriesItems.Commands.AddUpdateBundleCategory
{
    public class AddUpdateBundleCategoryCommandValidator : AbstractValidator<AddUpdateBundleCategoryCommand>
    {
        public AddUpdateBundleCategoryCommandValidator()
        {
            RuleFor(x => x.Offer).NotEmpty().MaximumLength(50);
        }
    }
}
