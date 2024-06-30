using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.WorkWeek.Model
{
    public class WorkWeekLookupModel
    {
        public Guid Id { get; set; }
         public int Day { get; set; }
        public int Status { get; set; }
        public string Country { get; set; }
    }
}
