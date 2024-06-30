using Focus.Domain.Enums;
using System;
using System.Collections.Generic;
using Focus.Business.ProductionModule.Processes;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionBatchs
{
    public class ProductionBatchLookupModel
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string NoOfBatches { get; set; }
        public string Note { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid RecipeNoId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsActive { get; set; }
        public decimal RecipeQuantity { get; set; }
        public bool IsClose { get; set; }
        public Guid? EmployeeRegistrationId { get; set; }
        public Guid? BranchId { get; set; }

        public virtual ICollection<ProductionBatchItemLookupModel> ProductionBatchItems { get; set; }
        public virtual ICollection<ProcessesLookUpModel> ProcessList { get; set; }
     

    }
}
