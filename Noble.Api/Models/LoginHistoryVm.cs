using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class LoginHistoryVm
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsLogin { get; set; }
        public string OperatingSystem { get; set; }
        public string UserName { get; set; }
        public string LoginDate { get; set; }
        public string LogoutDate { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
    }
}
