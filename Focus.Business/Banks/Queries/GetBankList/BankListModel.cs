using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Banks.Queries.GetBankList
{
    public class BankListModel
    {
        public IList<BankLookUpModel> Banks { get; set; }
    }
}
