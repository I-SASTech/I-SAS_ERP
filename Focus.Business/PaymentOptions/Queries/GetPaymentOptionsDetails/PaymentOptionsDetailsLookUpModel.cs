using Focus.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Focus.Business.PaymentOptions.Queries.GetPaymentOptionsDetails
{
    public class PaymentOptionsDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }


        public static Expression<Func<PaymentOption, PaymentOptionsDetailsLookUpModel>> Projection
        {
            get
            {
                return paymentOptions => new PaymentOptionsDetailsLookUpModel
                {
                    Id = paymentOptions.Id,
                    Name = paymentOptions.Name,
                    NameArabic = paymentOptions.NameArabic,
                    Image = paymentOptions.Image,
                    IsActive = paymentOptions.IsActive
                };
            }
        }

        public static PaymentOptionsDetailsLookUpModel Create(PaymentOption paymentOptions)
        {
            return Projection.Compile().Invoke(paymentOptions);
        }
    }
}
