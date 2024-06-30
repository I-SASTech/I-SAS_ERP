using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionModule.SampleRequests
{
    public class SampleRequestLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string NoOfSampleRequests { get; set; }
        public Guid CustomerId { get; set; }
        public string ReferredBy { get; set; }
        public string RequestGenerated { get; set; }
        public string SampleDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ProductId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<SampleRequestItemLookupModel> SampleRequestItems { get; set; }
        public string EnglishName { get; set; }
    }
}
