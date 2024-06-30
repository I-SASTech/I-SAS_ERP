using System;
namespace Focus.Business.Contacts.Queries.GetContactAttachments
{
    public class ContactAttachmentLookupModel
    {
        public string CommercialRegistration { get; set; }
        public string VATCertificate { get; set; }
        public string ZakatCertificate { get; set; }
        public Guid? ContactId { get; set; }
        public DateTime? Date { get; set; }
    }
}
