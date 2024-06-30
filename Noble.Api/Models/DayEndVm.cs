using System;

namespace Noble.Api.Models
{
    public class DayEndVm
    {
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal CashInHand { get; set; }
        public decimal Bank { get; set; }
        public decimal Expense { get; set; }
        public decimal VerifyCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
