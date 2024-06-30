using Focus.Domain.Enum;
using System;

namespace Focus.Business.Inventories.Commands.AddInventory
{
    public class InventoryLookupModel
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal AveragePrice { get; set; } // (old stock + currentStock)/2
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid DocumentId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WareHouseId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal CurrentQuantity { get; set; }
    }
}
