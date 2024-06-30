using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class UserDefineFlow : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid CompanyId { get; set; }
        public bool QuotationToSaleOrder { get; set; }
        public bool QuotationToSaleInvoice { get; set; }
        public bool PartiallyQuotation { get; set; }
        public bool PartiallySaleOrder { get; set; }
    }
}
