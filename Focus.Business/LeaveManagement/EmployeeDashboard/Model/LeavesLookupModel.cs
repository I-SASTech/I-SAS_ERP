using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.EmployeeDashboard.Model
{
    public class LeavesLookupModel
    {
        public string LeaveType { get; set; }
        public int TotLeaves { get; set; }  
        public int CarriedForwardLeaves { get; set; }  
    }
}
