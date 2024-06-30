using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission.Queries.GetCompanyInformation
{
    public class CompanyLookUp
    {
        public ICollection<CompanyLicenseLookUp> CompanyLicenseLookUps { get; set; }
        public ICollection<CompanyInfoLookUp> CompanyInfoLookUps { get; set; }
    }
}
