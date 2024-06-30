using System;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class InventoryBatchLookUpModel
    {
        public decimal Price { get; set; }
        public decimal RemainingQuantity { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int AutoNumbering { get; set; }
        public bool IsOpen { get; set; }
        public Guid ProductId { get; set; }
    }
}
