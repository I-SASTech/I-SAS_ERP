using System.Collections.Generic;

namespace Focus.Business.CompanyLicences.Queries.GetCompanyLicenseForClientManagement
{
    public class GetCompanyDataLookUp
    {
        public ICollection<CompanyInfoLookUp> Companies { get; set; }
        public ICollection<CompanyInfoLookUp> Businesses { get; set; }
        public ICollection<CompanyInfoLookUp> Locations { get; set; }
        
    }
}
