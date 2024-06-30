using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Sales.Queries.GetSaleList
{
   public class SaleListModel
    {
        public IList<SaleListLookupModel> Sales { get; set; }
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



    }

 
}
