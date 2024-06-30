using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;

namespace Focus.Business.DayStarts.Queries.IsDayStart
{
  public  class DayStartLookUpModel
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string CounterName { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal CashInHand { get; set; }
        public decimal VerifyCash { get; set; }
        public decimal ExpenseCash { get; set; }
        public decimal TotalCash { get; set; }
        public decimal SupervisorCash { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string DuringSaleCloseReason { get; set; }
        public bool IsActive { get; set; }
        public string DayStartUser { get; set; }
        public string DayEndUser { get; set; }
        public decimal CarryCash { get; set; }
        public string Reason { get; set; }
        public bool IsExpenseDay { get; set; }
        public bool IsDayStart { get; set; }
        public string StartTerminalBy { get; set; }
        public string StartTerminalFor { get; set; }
        public string EndTerminalBy { get; set; }
        public string EndTerminalFor { get; set; }
        public string CreditReason { get; set; }
        public string SuperVisorName { get; set; }
        public decimal BankAmount { get; set; }
        public int NoOfTransaction { get; set; }
        public bool IsReceived { get; set; }
        public decimal ReceivingAmount { get; set; }
        public decimal DraftExpense { get; set; }
        public decimal PostExpense { get; set; }
        public decimal BankExpense { get; set; }
        public ICollection<BankDetailLookUpModel> BankDetails { get; set; }
        public Guid? CounterId { get; set; }
    }
}
