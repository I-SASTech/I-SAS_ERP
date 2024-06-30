using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Focus.Business.JournalVouchers.Commands.AddUpdateJournalVoucher
{
    public class AddUpdateJournalVoucherCommandValidator:AbstractValidator<AddUpdateJournalVoucherCommand>
    {
        public AddUpdateJournalVoucherCommandValidator()
        {
            RuleFor(j => j.VoucherNumber).MaximumLength(30);
            RuleFor(j => j.Comments).MaximumLength(200);
            RuleFor(j => j.Narration).MaximumLength(200);
            RuleFor(j => j.Date);
            RuleFor(j => j.JournalVoucherItems);
        }
    }
}
