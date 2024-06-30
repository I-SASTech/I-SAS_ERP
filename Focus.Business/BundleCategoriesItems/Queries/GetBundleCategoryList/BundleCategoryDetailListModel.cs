using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryList
{
    public class BundleCategoryDetailListModel
    {
        public Guid Id { get; set; }
        public string Offer { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public bool isActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ProductName { get; set; }
    }
}
