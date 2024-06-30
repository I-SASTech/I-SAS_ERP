using System;
using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Attachment:BaseEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Guid? DocumentId { get; set; }
        public Guid? ModuleId { get; set; }

        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
      
    }
}
