using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class AutoPurchaseTemplate : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public int Day { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Contact Supplier { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid WareHouseId { get; set; }
        public string TaxMethod { get; set; }
        public bool Raw { get; set; }
        public virtual ICollection<AutoPurchaseTemplateItem> AutoPurchaseTemplateItems { get; set; }
        public Guid? BranchId { get; set; }
    }
}
