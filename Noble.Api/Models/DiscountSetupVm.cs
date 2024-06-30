using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class DiscountSetupVm
    {
        public Guid Id { get; set; }
        public bool LineItemBeforeVat { get; set; }
        public bool LineItemAfterVat { get; set; }
        public bool OverAllBeforeVat { get; set; }
        public bool OverAllAfterVat { get; set; }
        public bool DiscountOverQty { get; set; }
        public bool DiscountOver1Qty { get; set; }
    }
}
