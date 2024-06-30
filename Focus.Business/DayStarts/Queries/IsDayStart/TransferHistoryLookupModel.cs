using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DayStarts.Queries.IsDayStart
{
   public class TransferHistoryLookupModel
    {
        public string Date { get; set; }
        public Guid CounterId { get; set; }
        public string CounterName { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal CashInHand { get; set; }
        public decimal VerifyCash { get; set; }
        public decimal ExpenseCash { get; set; }
        public decimal TotalCash { get; set; }
        public decimal SupervisorCash { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool IsActive { get; set; }
        public string DayStartUser { get; set; }
        public string DayEndUser { get; set; }
        public decimal CarryCash { get; set; }
        public string Reason { get; set; }
        public bool IsExpenseDay { get; set; }
        public string StartTerminalBy { get; set; }
        public string StartTerminalFor { get; set; }
        public string CreditReason { get; set; }
        public Guid? DayStartId { get; set; }
        public decimal BankAmount { get; set; }
        public int NoOfTransaction { get; set; }
    }
}
