using System;

namespace Focus.Domain.Entities
{
    public class CompanyAttachment : BaseEntity
    {
        public string CommercialRegistration { get; set; }
        public string BusinessLicence { get; set; }
        public string CivilDefenceLicense { get; set; }
        public string CctvLicence { get; set; }
        public string Logo { get; set; }
        public DateTime Date { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
