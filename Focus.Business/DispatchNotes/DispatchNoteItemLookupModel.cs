using System;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;

namespace Focus.Business.DispatchNotes
{
    public class DispatchNoteItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Guid DispatchNoteId { get; set; }
        public virtual DispatchNote DispatchNote { get; set; }
    }
}
