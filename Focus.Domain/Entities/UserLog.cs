using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class UserLog : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public string ApprovalStatus { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
    }
}
