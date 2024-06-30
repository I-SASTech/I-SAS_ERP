using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class PurchaseBillItem : BaseEntity, ISoftDelete, ITenant
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid PurchaseBillId { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
    }
}
