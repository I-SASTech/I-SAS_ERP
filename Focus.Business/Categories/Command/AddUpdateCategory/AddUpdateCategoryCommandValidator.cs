using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Categories.Command.AddUpdateCategory
{
    public class AddUpdateCategoryCommandValidator : AbstractValidator<AddUpdateCategoryCommand>
    {
        public AddUpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
