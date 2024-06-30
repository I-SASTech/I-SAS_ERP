using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class AutoPurchaseSetting : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public int Day { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? WareHouseId { get; set; }
    }
}
