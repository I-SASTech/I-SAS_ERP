using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class DayStart : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal CashInHand { get; set; }
        public decimal VerifyCash { get; set; }
        public decimal ExpenseCash { get; set; }
        public decimal TotalCash { get; set; }
        public decimal SupervisorCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public DuringSaleClose DuringSaleClose { get; set; }
        public string DuringSaleCloseReason { get; set; }
        public bool IsActive { get; set; }
        public string DayStartUser { get; set; }
        public string DayEndUser { get; set; }
        public decimal CarryCash { get; set; }
        public string Reason { get; set; }
        public bool IsExpenseDay { get; set; }
        public bool IsDayStart { get; set; }
        public string StartTerminalBy { get; set; }
        public string SuperVisorName { get; set; }
        public string StartTerminalFor { get; set; }
        public string EndTerminalBy { get; set; }
        public string EndTerminalFor { get; set; }
        public string CreditReason { get; set; }
        public Guid? DayStartId { get; set; }
        public decimal BankAmount { get; set; }
        public int NoOfTransaction { get; set; }
        public decimal ReceivingAmount { get; set; }
        public bool IsReceived { get; set; }
    }
}
