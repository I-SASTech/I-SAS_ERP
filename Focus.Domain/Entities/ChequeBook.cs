using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class ChequeBook : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string BookNo { get; set; }
        public string BankNo { get; set; }
        public int NoOfCheques { get; set; }
        public string StartingNo { get; set; }
        public string LastNo { get; set; }
        public int Used { get; set; }
        public int Remaining { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlock { get; set; }
        public DateTime Date { get; set; }
        public Guid BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<ChequeBookItem> GetChequeBookItems { get; set; }




    }
}
