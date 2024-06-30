using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class InquiryItemLookUpModel
    {
        public Guid ItemId { get; set; }
        public Guid InquiryId { get; set; }
    }
}
