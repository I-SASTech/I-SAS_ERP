using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PaymentLimit : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    }
}
