using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class ApplicationUpdateVm
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Version { get; set; }
        public string Detail { get; set; }
        public DateTime DateTime { get; set; }
    }
}
