using Focus.Domain.Enums;
using System;
using System.Collections.Generic;
using Focus.Business.DispatchNotes.Queries.GetDispatchNoteDetails;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.DispatchNotes
{
    public class DispatchNoteLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Contact Customer { get; set; }
        public string Refrence { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<DispatchNoteItemLookupModel> DispatchNoteItems { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? BranchId { get; set; }


    }
}
