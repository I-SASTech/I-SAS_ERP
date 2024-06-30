using System;
using System.Collections.Generic;
using Focus.Domain.Entities;

namespace Focus.Business.DayStarts.Queries.IsDayStart
{
    public class IsDayStartLookupModel
    {
        public bool IsDayStart { get; set; }
        public bool IsFirstUser { get; set; }
        public bool IsDayStartPermission { get; set; }
        public DateTime? Date { get; set; }
        public Guid DayStartId { get; set; }
        public bool IsExpenseDay { get; set; }
        public object Token { get; set; }
        public string CounterCode { get; set; }
        public decimal CashInHand { get; set; }
        public decimal Expense { get; set; }
        public decimal Bank { get; set; }
        public decimal TotalCash { get; set; }
        public decimal OpeningCash { get; set; }
        public ICollection<DayStartLookUpModel> DayStarts { get; set; }
        public ICollection<DayStartLookUpModel> InacticeDayStarts { get; set; }
        public ICollection<TransferHistoryLookupModel> TransferHistories { get; set; }
        public DateTime? FromTime { get; set; }
        public string PrinterName { get; set; }
        public decimal DraftExpense { get; set; }
        public decimal PostExpense { get; set; }
        public decimal BankExpense { get; set; }
        public List<List<DayStartLookUpModel>> DayWiseList { get; set; }
        public decimal OutStandingBalance { get; set; }

        public decimal TotalReturnValue { get; set; }
        public int TotalReturnCount { get; set; }
        public int TotalInvoices { get; set; }
        public string FromInvoice { get; set; }
        public string ToInvoice { get; set; }
    }
}
