using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.Permission.Commands.AddUpdateNoblePermission
{
    public class CompanyLicenseLookUp
    {
        public Guid CompanyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsTechnicalSupport { get; set; }
        public bool IsUpdateTechnicalSupport { get; set; }
        public string TechnicalSupportPeriod { get; set; }
        public string PaymentFrequency { get; set; }
        public bool GracePeriod { get; set; }
        public string LicenseType { get; set; }
        public string ActivationPlatform { get; set; }
    }
}
