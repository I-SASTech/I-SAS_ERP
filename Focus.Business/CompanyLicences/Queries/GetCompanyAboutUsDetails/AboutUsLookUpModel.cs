using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyAboutUsDetails
{
    public class AboutUsLookUpModel
    {
        public string GroupName { get; set; }
        public string LicenseType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SupportType { get; set; }
        public string PaymentFrequency { get; set; }
        public List<string> ModuleList { get; set; }
        public bool IsTechnicalSupport { get; set; }
        public bool IsActive { get; set; }
        public bool IsGracePeriod { get; set; }
    }
}
