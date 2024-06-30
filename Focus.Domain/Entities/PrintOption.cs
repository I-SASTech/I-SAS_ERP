using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class PrintOption : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
   {
       public Guid? PrintSettingId { get; set; }
       public virtual PrintSetting PrintSetting { get; set; }
        public string Label { get; set; }
        public string LabelNameArabic { get; set; }
       public bool Value { get; set; }
       public string Type { get; set; }
       public string Description { get; set; }
       public Guid? BranchId { get; set; }
   }
}
