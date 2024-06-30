using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.TemporaryCashIssues.Commands
{
    public class TemporaryCashIssueLookUp
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid? BranchId { get; set; }
        public string NewUser { get; set; }
        public string Note { get; set; }
        public string Reason { get; set; }
        public string RegistrationNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public Guid? CashIssuerId { get; set; }
        public string CashIssuerNameEn { get; set; }
        public string CashIssuerNameAr { get; set; }
        public Guid? TemporaryCashRequestId { get; set; }
        public virtual ICollection<TemporaryCashIssueItemModel> TemporaryCashIssueItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
    }
}
