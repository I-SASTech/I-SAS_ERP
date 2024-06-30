using System;

namespace Focus.Business.DailyExpenses.Queries.GetDailyExpenseList
{
    public class DailyExpenseLookUpModel
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVat { get; set; }
        public string ReferenceNo { get; set; }
        public string CounterName { get; set; }
        public decimal GrossAmount { get; set; }
        public string UserName { get; set; }
        public string CreatedUser { get; set; }
        public string Reason { get; set; }
        public string PaymentMode { get; set; }
        public string BranchCode { get; set; }
        public string DateOfExpense { get; set; }
        public DateTime? ExpenseDate { get; set; }


    }
}
