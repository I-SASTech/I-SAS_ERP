using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class NobleModule: ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ModuleName { get; set; }
        public string ArabicName { get; set; }
        public virtual ICollection<NoblePermission> NoblePermissions { get; set; }
        public virtual ICollection<CompanyPermission> CompanyPermissions { get; set; }
    }
}
