using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class DiscountSetup:BaseEntity, ITenant
    {
        public bool LineItemBeforeVat { get; set; }
        public bool LineItemAfterVat { get; set; }
        public bool OverAllBeforeVat { get; set; }
        public bool OverAllAfterVat { get; set; }
        public bool DiscountOverQty { get; set; }
        public bool DiscountOver1Qty { get; set; }
    }
}
