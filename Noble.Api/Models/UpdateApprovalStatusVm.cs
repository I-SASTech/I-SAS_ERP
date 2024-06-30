using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Noble.Api.Models
{
    public class UpdateApprovalStatusVm
    {
        public ICollection<Guid> SelectedId { get; set; }
        public ApprovalStatus Action { get; set; }
        public Guid? AccountId { get; set; }
        public string Reason { get; set; }
        public string PaymentType { get; set; }

    }
}
