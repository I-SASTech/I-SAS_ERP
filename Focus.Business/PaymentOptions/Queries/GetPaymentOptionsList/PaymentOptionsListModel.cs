using System.Collections.Generic;

namespace Focus.Business.PaymentOptions.Queries.GetPaymentOptionsList
{
    public class PaymentOptionsListModel
    {
        public IList<PaymentOptionsLookUpModel> PaymentOptions { get; set; }
    }
}
