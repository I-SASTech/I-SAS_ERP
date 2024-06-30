using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class QuotationTemplate : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public bool IsService { get; set; }
        public string RegistrationNo { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<QuotationTemplateItem> QuotationTemplateItems { get; set; }
    }
}