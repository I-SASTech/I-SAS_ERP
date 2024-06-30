using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class Zone: BaseEntity,ITenant,IAuditedEntityBase,ITenantFilterableEntity
    {
        public string City { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Guid? BranchId { get; set; }

    }
}
