using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class MobileOrder : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<MobileOrderItem> MobileOrderItems { get; set; }

    }
}
