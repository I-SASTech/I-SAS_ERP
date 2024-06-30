using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Categories.Queries.GetCategoryList
{
    public class CategoryListModel
    {
        public IList<CategoryLookUpModel> Categories { get; set; }
    }
}
