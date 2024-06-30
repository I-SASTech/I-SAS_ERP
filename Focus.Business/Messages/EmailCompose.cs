using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business
{
    public class EmailCompose
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string ButtonName { get; set; }
        public string EmailPath { get; set; }
        public byte[] PdfBytes { get; set; }
        public ICollection<EmailTo> EmailTo { get; set; }
    }
}
