using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersList
{
    public class PromotionOffersLookUpModel : IMapFrom<PromotionOffer>
    {
        public Guid Id { get; set; }
        public string Offer { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal FixedDiscount { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PromotionOffer, PromotionOffersLookUpModel>();
        }
    }
}
