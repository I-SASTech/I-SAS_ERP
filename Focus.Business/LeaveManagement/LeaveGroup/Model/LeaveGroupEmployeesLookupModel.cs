using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveGroup.Model
{
    public class LeaveGroupEmployeesLookupModel
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid? LeaveGroupId { get; set; }
    }
}
