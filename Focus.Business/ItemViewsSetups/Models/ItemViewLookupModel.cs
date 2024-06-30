using System.Collections.Generic;

namespace Focus.Business.ItemViewsSetups.Models
{
    public class ItemViewLookupModel
    {
        public List<ItemViewSetupLookupModel> ItemViewSetupList { get; set; }
        public List<ItemViewSetupLookupModel> itemViewSetupListForPrint { get; set; }
        public List<ItemViewSetupLookupModel> ItemListViewSetup { get; set; }
    }
}
