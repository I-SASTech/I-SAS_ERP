using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AttachmentLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? DocumentId { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}