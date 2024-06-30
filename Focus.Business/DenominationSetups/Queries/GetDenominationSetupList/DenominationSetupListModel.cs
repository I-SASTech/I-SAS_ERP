using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DenominationSetups.Queries.GetDenominationSetupList
{
    public class DenominationSetupListModel
    {
        public IList<DenominationSetupLookUpModel> DenominationSetups { get; set; }
    }
}
