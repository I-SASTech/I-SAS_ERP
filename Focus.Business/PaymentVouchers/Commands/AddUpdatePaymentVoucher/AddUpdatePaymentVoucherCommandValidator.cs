//using System;
//using System.Collections.Generic;
//using System.Text;
//using FluentValidation;

//namespace Focus.Business.PaymentVouchers.Commands.AddUpdatePaymentVoucher
//{
//    public class AddUpdatePaymentVoucherCommandValidator : AbstractValidator<AddUpdatePaymentVoucherCommand>
//    {
//        public AddUpdatePaymentVoucherCommandValidator()
//        {
//            RuleFor(j => j.VoucherNumber).MaximumLength(30);
//            RuleFor(j => j.Narration).MaximumLength(200);
//            RuleFor(j => j.Date);
//        }
//    }
//}
