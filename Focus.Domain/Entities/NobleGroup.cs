using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class NobleGroup: ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid Id { get; set; }
        public String GroupName { get; set; }
        public GroupType GroupType { get; set; }

        public virtual ICollection<CompanyPermission> CompanyPermissions { get; set; }
    }
}
