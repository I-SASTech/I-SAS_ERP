using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequest.Commands.AddUpdateInquiry
{
    public class AddUpdateInquiryCommandValidator : AbstractValidator<AddUpdateInquiryCommand>
    {
        public AddUpdateInquiryCommandValidator()
        {
        }
    }
}
