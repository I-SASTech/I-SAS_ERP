using System;

namespace Focus.Business.ProductGroups.Model
{
    public class ProductListForGroupModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string CategoryName { get; set; }
        public string DisplayName { get; set; }
    }
}
