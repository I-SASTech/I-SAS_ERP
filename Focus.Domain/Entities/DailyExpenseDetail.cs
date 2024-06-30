using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class DailyExpenseDetail : BaseEntity, ISoftDelete, ITenant
    {
        
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid DailyExpenseId { get; set; }
        public virtual DailyExpense DailyExpense { get; set; }
        public string TaxMethod { get; set; }
        public Guid? VatId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid? ExpenseAccountId { get; set; }
        public virtual Account ExpenseAccount { get; set; }

    }

}
