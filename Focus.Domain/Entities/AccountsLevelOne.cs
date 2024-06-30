using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class AccountsLevelOne : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


    }
}
