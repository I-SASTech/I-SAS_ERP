using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class ReceivingCashModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Reason { get; set; }
    }
}
