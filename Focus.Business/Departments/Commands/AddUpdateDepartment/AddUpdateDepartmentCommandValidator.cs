using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.Departments.Commands.AddUpdateDepartment
{
    public class AddUpdateDepartmentCommandValidator : AbstractValidator<AddUpdateDepartmentCommand>
    {
        public AddUpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Description).MaximumLength(150);
        }
    }
}
