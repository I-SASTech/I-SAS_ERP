using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class Process : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
   {
       public string Code { get; set; }
       public string EnglishName { get; set; }
       public string Color { get; set; }
       public string ArabicName { get; set; }
       public string Description { get; set; }
       public bool IsActive { get; set; }
       public DateTime? Date { get; set; }
       public virtual ICollection<ProcessContractor> ProcessContractors { get; set; }
       public virtual ICollection<ProcessItem> ProcessItems { get; set; }



    }
}
