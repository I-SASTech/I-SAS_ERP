using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class BankPosTerminalVm
    {
        public Guid? Id { get; set; }
        public string TerminalId { get; set; }
        public Guid BankId { get; set; }
        public bool isActive { get; set; }
    }
}
