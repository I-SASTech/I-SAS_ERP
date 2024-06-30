using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class MonthlyCostItem : BaseEntity
    {
        public string Description { get; set; }
        public decimal MonthlyCosts { get; set; }
        public decimal YearlyCost { get; set; }
        public decimal Daily { get; set; }
        public decimal Total { get; set; }
        public Guid MonthlyCostId { get; set; }
        public virtual MonthlyCost MonthlyCost { get; set; }
    }
}
