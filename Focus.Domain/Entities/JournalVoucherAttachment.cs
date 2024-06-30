using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class JournalVoucherAttachment: BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Guid? JournalVoucherId { get; set; }
        public virtual JournalVoucher JournalVoucher { get; set; }
    }
}
