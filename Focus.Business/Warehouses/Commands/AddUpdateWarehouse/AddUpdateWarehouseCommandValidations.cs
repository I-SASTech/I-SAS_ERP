using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Warehouses.Commands.AddUpdateWarehouse
{
    public class AddUpdateWarehouseCommandValidations : AbstractValidator<AddUpdateWarehouseCommands>
    {
        public AddUpdateWarehouseCommandValidations()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        }
    }
}
