using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class InquiryAttachmentLookUpModel
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Guid? DocumentId { get; set; }
    }
}
