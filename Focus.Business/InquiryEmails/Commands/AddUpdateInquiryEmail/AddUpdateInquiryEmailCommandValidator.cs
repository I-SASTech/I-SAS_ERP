using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.InquiryEmails.Commands.AddUpdateInquiryEmail;

namespace Focus.Business.InquiryEmails.Commands.AddUpdateInquiryEmail
{
    public class AddUpdateInquiryEmailCommandValidator : AbstractValidator<AddUpdateInquiryEmailCommand>
    {
        public AddUpdateInquiryEmailCommandValidator()
        {
            
        }
    }
}
