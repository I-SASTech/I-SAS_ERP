using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryModule:BaseEntity,ITenant, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool Compulsory { get; set; }
        public bool AttachmentCompulsory { get; set; }
        public int RowNumber { get; set; }
        public virtual ICollection<ModuleQuestion> ModuleQuestions { get; set; }
        public virtual ICollection<InquiryModuleQuestion> InquiryModuleQuestions { get; set; }
    }
}
