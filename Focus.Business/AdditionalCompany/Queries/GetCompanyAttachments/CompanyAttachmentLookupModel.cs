using System;

namespace Focus.Business.AdditionalCompany.Queries.GetCompanyAttachments
{
    public class CompanyAttachmentLookupModel
    {
        public string CommercialRegistration { get; set; }
        public string BusinessLicence { get; set; }
        public string CivilDefenceLicense { get; set; }
        public string CctvLicence { get; set; }
        public string Logo { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime? Date { get; set; }
    }
}
