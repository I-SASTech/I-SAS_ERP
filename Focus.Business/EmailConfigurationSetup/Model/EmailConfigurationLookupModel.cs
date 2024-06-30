using Focus.Business.Emails.Models;
using System;
using System.Collections.Generic;

namespace Focus.Business.EmailConfigurationSetup.Model
{
    public class EmailConfigurationLookupModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public List<AttachmentBytesLookupModel> Attachments { get; set; }
        public List<EmailTo> Cc { get; set; }
    }
}
