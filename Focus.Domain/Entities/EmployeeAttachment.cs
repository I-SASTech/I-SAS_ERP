using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class EmployeeAttachment : BaseEntity, ITenant
    {
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid AttachmentId { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
