using System;

namespace Focus.Business.FinancialYearTemporaryClosing.Models
{
    public class AccountListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public Guid AccountId { get; set; }
        public decimal? PreviousBalance { get; set; }
    }
}
