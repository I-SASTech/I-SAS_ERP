using Focus.Business.Products.Queries.GetProductList;
using System.Collections.Generic;

namespace Focus.Business.SyncRecords.Queries.GetSyncRecordsList
{
    public class SyncRecordListModel
    {
        public IList<SyncRecordLookUpModel> SyncRecords { get; set; }
    }
}
