using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.BundleCategoriesItems.Models;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class BundleCategoryVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Offer { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public bool isActive { get; set; }
        public int quantityLimit { get; set; }
        public Guid? BranchId { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }
    }
}
