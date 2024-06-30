using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ChequeBooks.Commands
{
   public class ChequeBookItemLookUpModel
    {
        public Guid? Id { get; set; }
        public string BookNo { get; set; }
        public string SerialNo { get; set; }
        public string BankNo { get; set; }
        public string ChequeNo { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlock { get; set; }
        public bool IsCashed { get; set; }
        public DateTime Date { get; set; }
        public Guid BankId { get; set; }
        public DateTime? ChequeDate { get; set; }
        public DateTime? AlertDate { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public Guid? IssuedTo { get; set; }
        public string Remarks { get; set; }
        public string IssuedToName { get; set; }
        public string ShortDetail { get; set; }
        public decimal Amount { get; set; }
        public ChequeType ChequeType { get; set; }
        public StatusType StatusType { get; set; }
        public CashType CashType { get; set; }
        public Guid? IssuerAccount { get; set; }
        public Guid? ReservedAccount { get; set; }
        public bool IsPaid { get; set; }



        public DateTime? ChequeTypeDate { get; set; }
        public DateTime? StatusTypeDate { get; set; }
        public DateTime? CashTypeDate { get; set; }

        public Guid ChequeBookId { get; set; }
        public string Code { get; set; }
        public string ChequeTypes { get; set; }
        public string StatusTypes { get; set; }
        public string CashTypes { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }


    }
}
