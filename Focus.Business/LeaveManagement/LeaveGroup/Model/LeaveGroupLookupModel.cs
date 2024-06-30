using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveGroup.Model
{
    public class LeaveGroupLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<LeaveGroupEmployeesLookupModel> LeaveGroupEmployees { get; set; }
        public string Details { get; set; }
    }
}
