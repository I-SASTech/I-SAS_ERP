using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.Holiday.Model
{
    public class LeaveHolidayLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Country { get; set; }
    }
}
