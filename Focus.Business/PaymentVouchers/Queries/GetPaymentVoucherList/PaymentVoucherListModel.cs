using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class PaymentVoucherListModel
    {
        public IList<PaymentVoucherLookUpModel> PaymentVoucherList { get; set; }
    }
}
