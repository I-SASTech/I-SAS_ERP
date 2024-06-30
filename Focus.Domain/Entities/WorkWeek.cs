using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class WorkWeek : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public int Day { get; set; }
        public int Status { get; set; }
        public string Country { get; set; }
        public Guid? BranchId { get; set; }

    }
}
