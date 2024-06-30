using Focus.Business.Sales.Queries.GetSaleList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.SaleOrders.Queries.DashboardQuery
{
    public class SaleOrderDashboardLookUpModel
    {
        public int Draft { get; set; }
        public decimal TotalDraft { get; set; }
        public int Approved { get; set; }
        public int Processing { get; set; }
        public decimal TotalPartially { get; set; }
        public decimal TotalApproved { get; set; }
        public List<DashboardLookUp> CreditListByAmount { get; set; }
        public List<DashboardLookUp> PaidListByAmount { get; set; }
        public List<DashboardLookUp> CreditList { get; set; }
        public List<DashboardLookUp> PaidList { get; set; }
    }
}
