using Focus.Business.Attachments.Commands;
using System;
using System.Collections.Generic;

namespace Focus.Business.Emails.Models
{
    public class SendEmailFromListLookupModel
    {
        public Guid DocumentId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsInvoice { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<EmailTo> Cc { get; set; }
    }
}
