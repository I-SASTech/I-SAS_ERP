using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.TaxRates.Queries.GetTaxRateDetails
{
    public class TaxRateDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<TaxRate, TaxRateDetailsLookUpModel>> Projection
        {
            get
            {
                return TaxRate => new TaxRateDetailsLookUpModel
                {
                    Id = TaxRate.Id,
                    Name = TaxRate.Name,
                    NameArabic = TaxRate.NameArabic,
                    Rate = TaxRate.Rate,
                    Description = TaxRate.Description,
                    Code = TaxRate.Code,
                    isActive = TaxRate.isActive
                };
            }
        }

        public static TaxRateDetailsLookUpModel Create(TaxRate TaxRate)
        {
            return Projection.Compile().Invoke(TaxRate);
        }
    }
}
