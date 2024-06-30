using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Terminals.Queries.GetTerminalList
{
    public class TerminalListModel
    {
        public IList<TerminalLookUpModel> Terminals { get; set; }
    }
}
