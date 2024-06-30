using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class HoldSetup : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string HoldRecordType { get; set; }
        public bool IsActive { get; set; }
    }
}
