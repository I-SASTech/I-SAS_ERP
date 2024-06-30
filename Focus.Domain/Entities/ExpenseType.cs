using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class ExpenseType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string ExpenseTypeName { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string ExpenseTypeArabic { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        
    }
}
