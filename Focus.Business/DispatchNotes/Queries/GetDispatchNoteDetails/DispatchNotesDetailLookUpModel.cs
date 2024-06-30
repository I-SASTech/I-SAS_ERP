using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Enums;

namespace Focus.Business.DispatchNotes.Queries.GetDispatchNoteDetails
{
    public class DispatchNotesDetailLookUpModel
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
        public Guid? SaleOrderId { get; set; }

        public virtual ICollection<DispatchNoteItemLookupModel> DispatchNoteItems { get; set; }
      

        public static Expression<Func<DispatchNote, DispatchNotesDetailLookUpModel>> Projection
        {
            get
            {
                return dispatchNote => new DispatchNotesDetailLookUpModel
                {
                    Id = dispatchNote.Id,
                    Date = dispatchNote.Date,
                    RegistrationNo = dispatchNote.RegistrationNo,
                    CustomerId = dispatchNote.CustomerId,
                    Note = dispatchNote.Note,
                    Refrence = dispatchNote.Refrence,
                    //SaleOrderItems = department.SaleOrderItems,

                };
            }
        }


        public static DispatchNotesDetailLookUpModel Create(DispatchNote dispatchNote)
        {
            return Projection.Compile().Invoke(dispatchNote);
        }
    }
}
