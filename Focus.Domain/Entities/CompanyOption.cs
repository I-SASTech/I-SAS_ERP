using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CompanyOption : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid LocationId { get; set; }
        public string Label { get; set; }
        public bool Value { get; set; }
        public string OptionValue { get; set; }
    }
}
