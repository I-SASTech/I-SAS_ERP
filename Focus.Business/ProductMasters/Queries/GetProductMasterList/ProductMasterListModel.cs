using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ProductMasters.Queries.GetProductMasterList
{
    public class ProductMasterListModel
    {
        public IList<ProductMasterLookUpModel> ProductMasters { get; set; }
    }
}
