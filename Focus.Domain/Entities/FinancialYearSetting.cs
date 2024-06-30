using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class FinancialYearSetting : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string ClosingType { get; set; }
        public bool IsAutoClosing { get; set; }
    }
}
