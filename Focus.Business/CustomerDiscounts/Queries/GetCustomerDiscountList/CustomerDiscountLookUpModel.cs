using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.CustomerDiscounts.Queries.GetCustomerDiscountList
{
    public class CustomerDiscountLookUpModel:IMapFrom<CustomerDiscount>
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; }
        public double Discount { get; set; }
        public double FreightDiscount { get; set; }
        public double ExtraDiscount { get; set; }
        public double OtherDiscount { get; set; }
        public double OpenDiscount { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerDiscount, CustomerDiscountLookUpModel>();
        }
    }
}
