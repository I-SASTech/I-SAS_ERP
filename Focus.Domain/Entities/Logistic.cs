using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class Logistic : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string LicenseNo { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TermsAndCondition { get; set; }
        public string Xcoordinates { get; set; }
        public string Ycoordinates { get; set; }
        public SeaPort Ports { get; set; }
        public string ServiceFor { get; set; }
        public Guid? ClearanceAgent { get; set; }
        public virtual Account Account { get; set; }
        public Logistics LogisticsForm { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public Guid? BranchId { get; set; }
    }
}
