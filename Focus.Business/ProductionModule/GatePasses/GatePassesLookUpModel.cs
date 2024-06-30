using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionModule.GatePasses
{
    public class GatePassesLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FromDates { get; set; }
        public string ToDates { get; set; }

        public DateTime Date { get; set; }
        public string ContractorName { get; set; }
        public string ContractorNameArabic { get; set; }
        public string Dates { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Refrence { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<GatePassesItemLookUpModel> GatePassItems { get; set; }
        public Guid? ProductionBatchId { get; set; }
        public bool IsActive { get; set; }
        public bool IsGatePass { get; set; }


    }
}
