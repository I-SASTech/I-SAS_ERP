using Focus.Business.SaleOrders.Queries.NetAmount;
using System;
using System.Linq;

namespace Focus.Business.DispatchNotes.Queries.DispatchNoteList
{
    public class DispatchNoteLookUpModel 
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        
        public static DispatchNoteLookUpModel Create(Focus.Domain.Entities.DispatchNote dispatchNote)
        {
            var netAmount = new TotalAmount();
            var lookUpModel = new DispatchNoteLookUpModel
            {
                Id = dispatchNote.Id,
                RegistrationNumber= dispatchNote.RegistrationNo,
                CustomerName = dispatchNote.Customer?.EnglishName,
                Quantity = dispatchNote.DispatchNoteItems.Sum(x=>x.Quantity),
                Date = dispatchNote.Date.ToString("MM/dd/yyyy hh:mm tt")
            };
            return lookUpModel;
        }
    }
}
