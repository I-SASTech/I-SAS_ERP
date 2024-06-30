using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.TemporaryCashRequests.Commands
{
    public class TemporaryCashRequestModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid? BranchId { get; set; }
        public string UserName { get; set; }
        public string NewUser { get; set; }
        public string Note { get; set; }
        public string RegistrationNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsCashRequesterUser { get; set; }

        public virtual ICollection<TemporaryCashRequestItemModel> TemporaryCashItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }

    }
}
