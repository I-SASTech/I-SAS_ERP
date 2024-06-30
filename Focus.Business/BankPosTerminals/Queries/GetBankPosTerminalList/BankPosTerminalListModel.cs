using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalList
{
    public class BankPosTerminalListModel
    {
        public IList<BankPosTerminalLookUpModel> BankPosTerminals { get; set; }
    }
}
