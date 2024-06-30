using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PromotionOffersFolder.Commands.AddUpdatePromotionOffer
{
    public class AddUpdatePromotionOfferCommandValidator : AbstractValidator<AddUpdatePromotionOfferCommand>
    {
        public AddUpdatePromotionOfferCommandValidator()
        {
            RuleFor(x => x.Offer).NotEmpty().MaximumLength(50);
        }
    }
}
