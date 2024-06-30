using System;
using System.Collections.Generic;

namespace Focus.Business.Dashboard.Queries
{
    public class TransactionLookupModel
    {
        public Guid Id { get; set; }
        public int TotalInvoices { get; set; }
        public int TotalCreditInvoices { get; set; }
        public int TotalCashInvoices { get; set; }
        public int TotalBankInvoices { get; set; }
        public int TotalReturn { get; set; }
        public decimal UnPaidInvoices { get; set; }
        public decimal PartiallyInvoices { get; set; }
        public decimal FullyInvoices { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Purchase { get; set; }
        public decimal MonthlyCash { get; set; }
        public decimal MonthlyBank { get; set; }
        public decimal MonthlyExpense { get; set; }
        public List<EarningLookUpModel> EarningList { get; set; }
        public List<SalePurchaseLookUpModel> SalePurchaseLookUpModel { get; set; }


    }
}
