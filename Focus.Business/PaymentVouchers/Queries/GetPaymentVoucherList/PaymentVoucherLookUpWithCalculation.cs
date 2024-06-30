using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class PaymentVoucherLookUpWithCalculation
    {
        public List<PaymentVoucherLookUpModel> PaymentVouchers { get; set; }
        public int Posted { get; set; }
        public int Draft { get; set; }
        public int Rejected { get; set; }
        public int TotalReceipt { get; set; }
        public int CashReceipt { get; set; }
        public decimal TotalCashReceipt { get; set; }
        public int BankReceipt { get; set; }
        public decimal TotalBankReceipt { get; set; }
        public decimal TotalCashBank { get; set; }
        public decimal Normal { get; set; }
        public decimal Advance { get; set; }
        public decimal Security { get; set; }
        public decimal TotalNormal { get; set; }
        public decimal Advancebalacne { get; set; }

    }
}
