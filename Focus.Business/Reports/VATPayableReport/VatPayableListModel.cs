using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.VATPayableReport
{
    public class VatPayableListModel
    {
        public List<VatPayableModel> VatPaid{ get; set; }
        public List<VatPayableModel> VatPayable { get; set; }
    }
}
