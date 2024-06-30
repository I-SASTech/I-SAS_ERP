using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class DenominationSetupVm
    {
        public Guid? Id { get; set; }
        public decimal Number { get; set; }
        public bool isActive { get; set; }
    }
}
