using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
  public  class ContactAttachment:BaseEntity, ITenant
    {
        public string CommercialRegistration { get; set; }
        public string VATCertificate { get; set; }
        public string ZakatCertificate { get; set; }
        public DateTime Date { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
