using System;

namespace Focus.Business.PriceRecordChanges.Model
{
    public class PriceLabelsProuctsListLookupModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Price { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public bool IsActive { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string PriceLabelName { get; set; }
        public Guid? PriceLabelingId { get; set; }
        public Guid? CustomerId { get; set; }
        public bool IsCustomer { get; set; }
        public decimal? NewPrice { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string IsActiveValue { get; set; }
        public string DisplayName { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? OriginId { get; set; }
        public Guid? ProdutMasterId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
