using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;

namespace Focus.Business.Currencies.Queries.GetCurrencyList
{
    public class CurrencyLookUpModel : IMapFrom<Currency>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public string ArabicSign { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string NameArabic { get;  set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Currency, CurrencyLookUpModel>()
                .ForMember(x => x.Image, prod => prod.MapFrom(z => z.Image));
        }
    }
}
