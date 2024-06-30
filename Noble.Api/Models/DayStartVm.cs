using System;

namespace Noble.Api.Models
{
    public class DayStartVm
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal VerifyCash { get; set; }
        public decimal ExpenseCash { get; set; }
        public decimal SupervisorCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Reason { get; set; }
        public decimal CarryCash { get; set; }
        public decimal CashInHand { get; set; }
        public bool IsExpenseDay { get; set; }
        public bool IsFirstUser { get; set; }
        public decimal BankAmount { get; set; }
        public string NumberOfTransaction { get; set; }
        public string ToUser { get; set; }
        public string ToPassword { get; set; }
        public bool IsTransfer { get; set; }
        public string EndTerminalBy { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorPassword { get; set; }
        public string CreditReason { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsSupervisorLogin { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
