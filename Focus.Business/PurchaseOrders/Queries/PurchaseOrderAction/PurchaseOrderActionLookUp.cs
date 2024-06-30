using System;
using Focus.Domain.Entities;

namespace Focus.Business.PurchaseOrders.Queries.PurchaseOrderAction
{
    public class PurchaseOrderActionLookUp
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string ProcessNameArabic { get; set; }
    }
}
