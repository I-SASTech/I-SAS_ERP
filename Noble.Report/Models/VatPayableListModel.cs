using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class VatPayableListModel
    {
        public List<VatPayableModel> VatPaid { get; set; }
        public List<VatPayableModel> VatPayable { get; set; }
    }
}