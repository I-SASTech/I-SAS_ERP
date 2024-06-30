using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries.AccountDashboardQuery
{
  public  class PayableReceivableLookUpModel
    {
        public string DateIn { get; set; }
        public DateTime Date { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal ReceivableAmount { get; set; }
        public bool IsPayable { get; set; }
    }
}
