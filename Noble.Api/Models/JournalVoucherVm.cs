using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;
using Focus.Web.ViewModel;

namespace Noble.Api.Models
{
    public class JournalVoucherVm
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Comments { get; set; }
        public string Narration { get; set; }
        public bool OpeningCash { get; set; }
        public bool View { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public ICollection<JournalVoucherItemVm> JournalVoucherItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BranchId { get; set; }

    }
}
