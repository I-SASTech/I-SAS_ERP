using System;

namespace Focus.Business.Sales.Commands.AddSale
{
    public class SaleSizeAssortmentModel
    {
        public Guid? SaleItemId { get; set; }
        public Guid? PurchasePostItemId { get; set; }
        public Guid SizeId { get; set; }
        public string Name { get; set; }
        public string ItemName { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
    }
}
