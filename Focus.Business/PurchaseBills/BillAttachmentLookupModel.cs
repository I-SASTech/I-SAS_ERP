using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PurchaseBills
{
   public class BillAttachmentLookupModel
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Guid? PurchaseBillId { get; set; }
    }
}
