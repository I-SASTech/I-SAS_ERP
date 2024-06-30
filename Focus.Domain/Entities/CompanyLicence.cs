using System;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
  public  class CompanyLicence:BaseEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfUsers { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyType CompanyType { get; set; }
        public decimal NoOfTransactionsAllow { get; set; }
        public bool IsTechnicalSupport { get; set; }
        public bool IsUpdateTechnicalSupport { get; set; }
        public string TechnicalSupportPeriod { get; set; }
        public string PaymentFrequency { get; set; }
        public bool GracePeriod { get; set; }
        public string LicenseType { get; set; }
        public virtual Company Company { get; set; }
        public ActivationPlatform ActivationPlatform { get; set; }
    }
}
