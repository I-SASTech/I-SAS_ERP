using Focus.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Focus.Business.Currencies.Queries.GetCurrencyDetails
{
    public class CurrencyDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Sign { get; set; }
        public string ArabicSign { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

        public static Expression<Func<Currency, CurrencyDetailsLookUpModel>> Projection
        {
            get
            {
                return currency => new CurrencyDetailsLookUpModel
                {
                    Id = currency.Id,
                    Name = currency.Name,
                    NameArabic = currency.NameArabic,
                    Sign = currency.Sign,
                    ArabicSign = currency.ArabicSign,
                    IsActive = currency.IsActive,
                    Image = currency.Image
                };
            }
        }

        public static CurrencyDetailsLookUpModel Create(Currency currency)
        {
            return Projection.Compile().Invoke(currency);
        }

        
    }
}
