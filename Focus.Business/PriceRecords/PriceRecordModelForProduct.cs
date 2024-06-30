using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.PriceRecords
{
    public class PriceRecordModelForProduct
    {
        public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public bool IsActive { get; set; }
        public Guid? PriceLabelingId { get; set; }
    
    }
}
