using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class TransactionApplicationLog : BaseEntity
    {
        public int FreshLogMovingDays { get; set; }
        public int DeleteFromHistory { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public Guid? LocationId { get; set; }
        public virtual Company Company { get; set; }


    }
}
