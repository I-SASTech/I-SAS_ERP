using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SaleSizeAssortment : BaseEntity, ISoftDelete, IRecordDailyEntry, ITenant
    {
        public Guid? SaleItemId { get; set; }
        public virtual SaleItem SaleItem { get; set; }
        public Guid? PurchasePostItemId { get; set; }
        public virtual PurchasePostItem PurchasePostItem { get; set; }
        public Guid SizeId { get; set; }
        public virtual Size Size { get; set; }
        public int Quantity { get; set; }
    }
}
