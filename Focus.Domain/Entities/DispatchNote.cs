using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Enums;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class DispatchNote : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Contact Customer { get; set; }
        public string Refrence { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<DispatchNoteItem> DispatchNoteItems { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
