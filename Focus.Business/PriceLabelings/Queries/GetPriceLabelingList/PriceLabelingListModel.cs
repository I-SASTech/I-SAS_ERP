using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PriceLabelings.Queries.GetPriceLabelingList
{
    public class PriceLabelingListModel
    {
        public IList<PriceLabelingLookUpModel> PriceLabelings { get; set; }
    }
}
