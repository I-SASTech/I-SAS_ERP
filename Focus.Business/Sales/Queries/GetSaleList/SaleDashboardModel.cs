using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Sales.Queries.GetSaleList
{
    public class SaleDashboardModel
    {
        public int Hold { get; set; }
        public decimal TotalHold { get; set; }
        public int UnPaid { get; set; }
        public decimal TotalUnPaid { get; set; }

        public int FullyPaid { get; set; }
        public decimal TotalFullyPaid { get; set; }

        public int Partially { get; set; }
        public decimal TotalPartially { get; set; }

        public int Credit { get; set; }
        public decimal TotalCredit { get; set; }

        public int Cash { get; set; }
        public decimal TotalCash { get; set; }
        public List<DashboardLookUp> CreditListByAmount { get; set; }
        public List<DashboardLookUp> PaidListByAmount { get; set; }
        public List<DashboardLookUp> CreditList { get; set; }
        public List<DashboardLookUp> PaidList { get; set; }
       
    }
  
}
