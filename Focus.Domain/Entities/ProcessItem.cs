using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class ProcessItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid ProcessId { get; set; }
        public virtual Process Process { get; set; }
      

    }
}
