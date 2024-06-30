using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries
{
   public class SalePurchaseLookUpModel
    {
        public string Week { get; set; }
        public int DateIn { get; set; }
        public DateTime Date { get; set; }
        public decimal SaleAmount { get; set; }
        public decimal SaleDiscount { get; set; }
        public decimal SaleVAT { get; set; }
        public decimal PurchaseAmount { get; set; }
        public bool IsPurchase { get; set; }
    }
}
