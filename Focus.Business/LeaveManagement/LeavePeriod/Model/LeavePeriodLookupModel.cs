using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeavePeriod.Model
{
    public class LeavePeriodLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
