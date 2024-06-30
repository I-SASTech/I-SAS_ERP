using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.EmployeeDashboard.Model
{
    public class HrEmployeeDashboardLookupModel
    {
        public Guid Id { get; set;}
        public int LeavePerLeaveYear { get; set;}
        public string Leavetype { get; set;}
        public string LeaveGroup { get; set;}
        public string LeavePeriod { get; set;}
        public string EmployeeCode { get; set;}
        public string Department { get; set;}
        public string Status { get; set;}
        public string Gender { get; set;}
        public string Born { get; set;}
        public string EmpoloyeeFor { get; set;}
        public List<LeavesLookupModel> Leaves { get; set;}
        public List<HolidaysLookupModel> Holidays { get; set;}
    }
}
