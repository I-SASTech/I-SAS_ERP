using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InventoryBlinds.ViewModel
{
    public class InventoryBlindDetailVm
    {
        public Guid ProductId { get; set; }
        public int CurrentQuantity { get; set; }
        public int PhysicalQuantity { get; set; }
        public string Remarks { get; set; }
    }
}
