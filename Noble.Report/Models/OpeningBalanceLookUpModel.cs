using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class OpeningBalanceLookUpModel
    {
        public decimal OpeningBalance { get; set; }
        public decimal DraftExpense { get; set; }
        public decimal PostExpense { get; set; }
        public decimal CashInHand { get; set; }
        public decimal Bank { get; set; }
        public decimal CarryCash { get; set; }
        public int NoOfTransaction { get; set; }
        public string TerminalCode { get; set; }
        public string StartTime { get; set; }
        public string StartDate { get; set; }
        public string Date { get; set; }
        public string EndTime { get; set; }
        public string EndDate { get; set; }
        public string StartBy { get; set; }
        public string StartFor { get; set; }
        public decimal OpeningCash { get; internal set; }
        public ICollection<BankDetailLookUpModel> BankDetails { get; set; }
        public int TotalInvoices { get; set; }
        public string FromInvoices { get; set; }
        public string ToInvoices { get; set; }
        public decimal TotalVat { get; internal set; }
        public List<BankTransactionDetailLookUp> BankDetailList { get; set; }
        public decimal BankDetailListTotal { get; set; }
        public decimal BankExpense { get; set; }
        public decimal VerifyCash { get; set; }
        public decimal TotalCash { get; set; }
    }
}