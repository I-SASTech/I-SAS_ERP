using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Domain.Enum;

namespace Noble.Api.Models
{
    public class ApprovalStatusPaymentVoucher
    {
        public ICollection<Guid> Id { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string UserName { get; set; }
    }
}
