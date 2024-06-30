using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Business.QuotationTemplates.Commands.AddQuotationTemplate
{
    public class QuotationTemplateLookUp
    {
        public Guid Id { get; set; }
        public bool IsService { get; set; }

        public string Dates { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<QuotationTemplateItemLookUpModel> QuotationTemplateItems { get; set; }

    }
}
