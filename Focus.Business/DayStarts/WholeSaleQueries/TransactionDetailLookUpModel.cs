using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DayStarts.WholeSaleQueries
{
    public class TransactionDetailLookUpModel
    {
        public List<AccountDetailsLookUpModel> CashSale { get; set; }
        public List<AccountDetailsLookUpModel> BankSale { get; set; }
        public List<AccountDetailsLookUpModel> CashPurchase { get; set; }
        public List<AccountDetailsLookUpModel> BankPurchase { get; set; }
        public List<AccountDetailsLookUpModel> CashExpense { get; set; }
        public List<AccountDetailsLookUpModel> BankExpense { get; set; }
        public List<AccountDetailsLookUpModel> CashCustomerReceipt { get; set; }
        public List<AccountDetailsLookUpModel> BankCustomerReceipt { get; set; }
        public List<AccountDetailsLookUpModel> CashSupplierPay { get; set; }
        public List<AccountDetailsLookUpModel> BankSupplierPay { get; set; }
        public List<AccountDetailsLookUpModel> CashPurchaseExpense { get; set; }
        public List<AccountDetailsLookUpModel> BankPurchaseExpense { get; set; }
        public List<AccountDetailsLookUpModel> OtherCashPayments { get; set; }
        public List<AccountDetailsLookUpModel> OtherBankPayments { get; set; }
        public List<AccountDetailsLookUpModel> SaleReturn { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
