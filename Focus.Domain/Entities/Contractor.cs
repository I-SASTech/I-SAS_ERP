using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class Contractor : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
   {
       public string EnglishName { get; set; }
       public string ArabicName { get; set; }
       public bool IsOnFactory { get; set; }
       public bool IsActive { get; set; }
       public DateTime? Date { get; set; }
       public virtual ICollection<ProcessContractor> ProcessContractors { get; set; }
       public virtual ICollection<BatchProcessContractor> BatchProcessContractors { get; set; }



    }
}
