using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Domain.Entities;

namespace Focus.Business.DailyExpenses
{
    public class DailyExpenseDetailVm
    {
        public Guid Id { get; set; }
        public Guid? VatId { get; set; }
        public string TaxMethod { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }
        public string TaxName { get; set; }
        public decimal Amount { get; set; }
        public decimal InclusiveTotalVat { get; set; }
        public decimal ExclusiveTotalVat { get; set; }
        public Guid DailyExpenseId { get; set; }
        public Guid? ExpenseAccountId { get; set; }

        public virtual DailyExpense DailyExpense { get; set; }
        public string NameArabic { get; internal set; }
    }
}
