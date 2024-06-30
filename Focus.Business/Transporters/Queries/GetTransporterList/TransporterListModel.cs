using System.Collections.Generic;

namespace Focus.Business.Transporters.Queries.GetTransporterList
{
   public class TransporterListModel
    {
        public IList<TransporterLookUpModel> Transporters { get; set; }
    }
}
