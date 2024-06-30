using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.MonthlyCosts.Commands
{
   public class MonthlyCostItemLookUpModel
    {
        public string Description { get; set; }
        public decimal MonthlyCosts { get; set; }
        public decimal YearlyCost { get; set; }
        public decimal Daily { get; set; }
        public decimal Total { get; set; }
        public Guid MonthlyCostId { get; set; }
        public Guid Id { get; set; }
    }
}
