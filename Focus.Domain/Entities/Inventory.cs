using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Inventory : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete, IRecordDailyEntry
    {
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CurrentStockValue { get; set; }
        public decimal AveragePrice { get; set; } // (old stock + currentStock)/2
        public decimal Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid DocumentId { get; set; }
        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid WareHouseId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? BundleId { get; set; }
        public decimal OfferQuantity { get; set; }
        public decimal Get { get; set; }
        public decimal Buy { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpen { get; set; }
        public string BatchNumber { get; set; }
        public int AutoNumbering { get; set; }
        public decimal RemainingQuantity { get; set; }
        public string Serial { get; set; }
        public Guid? BranchId { get; set; }
        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
