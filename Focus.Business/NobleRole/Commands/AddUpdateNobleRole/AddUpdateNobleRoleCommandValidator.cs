using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.NobleRole.Commands.AddUpdateNobleRole
{
    public class AddUpdateNobleRoleCommandValidator : AbstractValidator<AddUpdateNobleRoleCommand>
    {
        public AddUpdateNobleRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
