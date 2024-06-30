using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;
using Focus.Business.BundleCategoriesItems.Models;

namespace Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryDetails
{
    public class BundleCategoryDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Offer { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public bool isActive { get; set; }
        public int QuantityLimit { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }
      
    }
}
