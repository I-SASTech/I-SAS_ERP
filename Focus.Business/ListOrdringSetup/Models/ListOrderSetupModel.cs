using System.Collections.Generic;

namespace Focus.Business.ListOrdringSetup.Models
{
    public class ListOrderSetupModel
    {
        public List<ListOrderSetupLookupModel> CustomerListView { get; set; }
        public List<ListOrderSetupLookupModel> SupplierListView { get; set; }
    }
}
