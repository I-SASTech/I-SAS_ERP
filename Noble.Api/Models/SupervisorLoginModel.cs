using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class SupervisorLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsFlushData { get; set; }
    }
}
