using System;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchList
{
    public class ProductionBatchLookUpModel 
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string ReciptName { get; set; }
        public string NoOfBatches { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CreatedBy { get; set; }
        public string CompleteBy { get; set; }
        public string ProcessBy { get; set; }
        public string TransferBy { get; set; }
        public decimal DamageStock { get; set; }
        public decimal RemainingStock { get; set; }
        public string LateReason { get; set; }
        public string LateReasonCompletion { get; set; }
        public string CompleteDate { get; set; }
        public string ProcessDate { get; set; }
        public string TransferDate { get; set; }
        public decimal RecipeQuantity { get; set; }


    }
}
