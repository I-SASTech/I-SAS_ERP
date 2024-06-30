using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class Bank : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string BankName { get; set; }
        public string NameArabic { get; set; }
        public string Code { get; set; }
        public string AccountType { get; set; }
        public string IsActive { get; set; }
        public bool Active { get; set; }
        public string AccoutName { get; set; }
        public string AccoutNameArabic { get; set; }
        public Guid? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string BranchAddress { get; set; }
        public string AccountNumber { get; set; }
        public string IBNNumber { get; set; }
        public string SwiftCode { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerContectualNumber { get; set; }
        public string AccounType { get; set; }
        public string Reference { get; set; }
        public string ShortName { get; set; }
        public virtual ICollection<PrintSetting> Banks1 { get; set; }
        public virtual ICollection<PrintSetting> Banks2 { get; set; }
        public virtual ICollection<ChequeBook> ChequeBooks { get; set; }
        public virtual ICollection<Department> Departments { get; set; }



    }
}
