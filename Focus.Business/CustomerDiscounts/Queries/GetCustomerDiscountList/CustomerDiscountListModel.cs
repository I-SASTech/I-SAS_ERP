using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.CustomerDiscounts.Queries.GetCustomerDiscountList
{
    public class CustomerDiscountListModel
    {
        public IList<CustomerDiscountLookUpModel> CustomerDiscounts { get; set; }
    }
}
