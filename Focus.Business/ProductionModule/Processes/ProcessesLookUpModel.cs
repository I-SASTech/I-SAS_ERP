using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionModule.Processes
{
    public class ProcessesLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ProductionBatchId { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisable { get; set; }
        public DateTime? Date { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        public virtual ICollection<ProcessItemLookUpModel> ProcessItems { get; set; }
        public virtual ICollection<ProcessContractorLookUpModel> ProcessContractors { get; set; }
        

    }
}
