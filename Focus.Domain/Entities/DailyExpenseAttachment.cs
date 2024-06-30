using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class DailyExpenseAttachment : BaseEntity, ITenant
   {
       public string Name { get; set; }
       public string NameArabic { get; set; }
       public string Path { get; set; }
       public string Description { get; set; }
       public DateTime? Date { get; set; }
       public Guid? DailyExpenseId { get; set; }
       public virtual DailyExpense DailyExpense { get; set; }

   }
}
