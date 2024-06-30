using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Designation : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<EmployeeRegistration> EmployeeRegistrations { get; set; }
        public virtual ICollection<LeaveRules> LeaveRules { get; set; } 
    }
}
