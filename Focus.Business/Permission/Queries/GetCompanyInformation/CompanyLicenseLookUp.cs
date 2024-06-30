using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission.Queries.GetCompanyInformation
{
    public class CompanyLicenseLookUp
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
    }
}
