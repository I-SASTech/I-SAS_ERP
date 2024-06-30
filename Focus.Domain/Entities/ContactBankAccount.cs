using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class ContactBankAccount : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string AccountTitle { get; set; }
        public string AccountNo { get; set; }
        public string Iban { get; set; }
        public string NameOfBank { get; set; }
        public string BranchName { get; set; }
        public string RoutingCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Address { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
