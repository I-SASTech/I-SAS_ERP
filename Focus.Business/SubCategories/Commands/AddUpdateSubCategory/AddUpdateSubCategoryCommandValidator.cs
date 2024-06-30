using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.SubCategories.Commands.AddUpdateSubCategory
{
    public class AddUpdateSubCategoryCommandValidator : AbstractValidator<AddUpdateSubCategoryCommand>
    {
        public AddUpdateSubCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
