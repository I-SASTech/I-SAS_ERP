using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Currencies.Queries.GetCurrencyList
{
    public class CurrencyListModel
    {
        public IList<CurrencyLookUpModel> Currencies { get; set; }
    }
}
