using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.TaxRates.Queries.GetTaxRateList
{
    public class TaxRateListModel
    {
        public IList<TaxRateLookUpModel> TaxRates { get; set; }
        public  string TaxMethod { get; set; }
    }
}
