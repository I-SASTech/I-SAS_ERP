using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Warehouse : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string StoreID { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiry { get; set; }
        public string CivilDefenceLicenseNo { get; set; }
        public DateTime? CivilDefenceLicenseExpiry { get; set; }
        public string CCTVLicenseNo { get; set; }
        public DateTime? CCTVLicenseExpiry { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        public virtual ICollection<PurchasePostItem> GetPurchasePostItems { get; set; }
        public virtual ICollection<StockAdjustment> StockAdjustments { get; set; }
        public virtual ICollection<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public virtual ICollection<WareHouseTransfer> WareHouseTransfers { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual ICollection<ProductionBatchItem> ProductionBatchItems { get; set; }
        public virtual ICollection<InventoryBlind> InventoryBlinds { get; set; }
        public virtual ICollection<BatchProcess> BatchProcesses { get; set; }
        public virtual ICollection<CreditNoteItem> CreditNoteItems { get; set; }
        public Guid? BranchId { get; set; }

    }
}
