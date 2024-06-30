using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.VATPayableReport
{
   public class VatPayableModel
    {
        public string CostCenter { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public decimal Amount { get; set; }
        public Guid? BranchId { get; set; }
    }
}
