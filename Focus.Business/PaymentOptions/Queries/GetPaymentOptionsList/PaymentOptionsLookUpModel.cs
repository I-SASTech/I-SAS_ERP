using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;

namespace Focus.Business.PaymentOptions.Queries.GetPaymentOptionsList
{
    public class PaymentOptionsLookUpModel : IMapFrom<PaymentOption>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentOption, PaymentOptionsLookUpModel>()
                .ForMember(x => x.Image, prod => prod.MapFrom(z => z.Image));
        }
    }
}
