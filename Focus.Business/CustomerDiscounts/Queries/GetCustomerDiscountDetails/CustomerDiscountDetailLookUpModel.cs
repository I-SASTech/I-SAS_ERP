using System;
using System.Linq.Expressions;
using Focus.Domain.Entities;

namespace Focus.Business.CustomerDiscounts.Queries.GetCustomerDiscountDetails
{
    public class CustomerDiscountDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; }
        public double Discount { get; set; }
        public double FreightDiscount { get; set; }
        public double ExtraDiscount { get; set; }
        public double OtherDiscount { get; set; }
        public double OpenDiscount { get; set; }

        public static Expression<Func<CustomerDiscount, CustomerDiscountDetailLookUpModel>> Projection
        {
            get
            {
                return customerDiscount => new CustomerDiscountDetailLookUpModel
                {
                    Id = customerDiscount.Id,
                    DiscountName = customerDiscount.DiscountName,
                    Discount = customerDiscount.Discount,
                    ExtraDiscount = customerDiscount.ExtraDiscount,
                    FreightDiscount = customerDiscount.FreightDiscount,
                    OpenDiscount = customerDiscount.OpenDiscount,
                    OtherDiscount = customerDiscount.OtherDiscount
                };
            }
        }
        public static CustomerDiscountDetailLookUpModel Create(CustomerDiscount customerDiscount)
        {
            return Projection.Compile().Invoke(customerDiscount);
        }
    }
}
