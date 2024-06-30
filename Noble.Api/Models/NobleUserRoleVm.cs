using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class NobleUserRoleVm
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}