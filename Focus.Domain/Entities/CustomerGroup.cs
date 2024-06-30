using Focus.Domain.Interface;
using System.Collections;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class CustomerGroup : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
