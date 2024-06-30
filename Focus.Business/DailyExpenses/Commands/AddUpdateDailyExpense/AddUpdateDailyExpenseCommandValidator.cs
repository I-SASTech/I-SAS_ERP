using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.DailyExpenses.Commands.AddUpdateDailyExpense
{
    public class AddUpdateDailyExpenseCommandValidator:AbstractValidator<AddUpdateDailyExpenseCommand>
    {
        public AddUpdateDailyExpenseCommandValidator()
        {
          
        }
    }
}
