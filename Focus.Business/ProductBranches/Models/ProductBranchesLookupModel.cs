using Focus.Business.BundleCategoriesItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.ProductBranches.Models
{
    public class ProductBranchesLookupModel
    {
        public bool isSelectAll { get; set; }
        public List<BundlesOffersBranchesLookupModel> branchIds { get; set; }
        public List<BundlesOffersBranchesLookupModel> productIds { get; set; }
    }
}
