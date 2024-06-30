using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Focus.Business.JournalVouchers.Queries.NetDrCr;
using Focus.Domain.Entities;

namespace Focus.Business.JournalVouchers.Queries.GetJournalVoucherList
{
    public class JournalVoucherLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUser { get; set; }
        public decimal TotalDr { get; set; }
        public decimal TotalCr { get; set; }
        public string ApprovalStatus { get; set; }
        public  bool OneTimeEntry { get; set; }
        public string BranchCode { get; set; }
    }
}
