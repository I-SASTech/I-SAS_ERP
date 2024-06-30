using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.PriceLabelings.Queries.GetPriceLabelingDetails
{
    public class PriceLabelingDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public decimal Price { get; set; }

        public static Expression<Func<PriceLabeling, PriceLabelingDetailsLookUpModel>> Projection
        {
            get
            {
                return priceLabeling => new PriceLabelingDetailsLookUpModel
                {
                    Id = priceLabeling.Id,
                    Name = priceLabeling.Name,
                    NameArabic = priceLabeling.NameArabic,
                    Description = priceLabeling.Description,
                    Code = priceLabeling.Code,
                    Price = priceLabeling.Price,
                    isActive = priceLabeling.IsActive
                };
            }
        }

        public static PriceLabelingDetailsLookUpModel Create(PriceLabeling priceLabeling)
        {
            return Projection.Compile().Invoke(priceLabeling);
        }
    }
}
