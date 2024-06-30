using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.ChequeBooks.Commands
{
    public class ChequeBookLookUpModel
    {
        public Guid? Id { get; set; }
        public string BankNo { get; set; }
        public string BookNo { get; set; }
        public int NoOfCheques { get; set; }
        public string StartingNo { get; set; }
        public string LastNo { get; set; }
        public int Used { get; set; }
        public int UsedCheck { get; set; }
        public int Blocked { get; set; }
        public int Remaining { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlock { get; set; }
        public DateTime Date { get; set; }
        public string Dates { get; set; }
        public Guid BankId { get; set; }
        public virtual ICollection<ChequeBookItemLookUpModel> ChequeBookItems { get; set; }


    }
}
