using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Focus.Business.ProductionModule.Processes;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Enums;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails
{
    public class ProductionBatchDetailLookUpModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string NoOfBatches { get; set; }
        public string Note { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid RecipeNoId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public string RecipeName { get; set; }
        public string SaleOrderNo { get; set; }
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
        public virtual ICollection<ProductionBatchItemLookupModel> ProductionBatchItems { get; set; }
        public virtual ICollection<ProcessesLookUpModel> ProcessList { get; set; }


        public static Expression<Func<ProductionBatch, ProductionBatchDetailLookUpModel>> Projection
        {
            get
            {
                return department => new ProductionBatchDetailLookUpModel
                {
                    Id = department.Id,
                    Date = department.Date,
                    RegistrationNo = department.RegistrationNo,
                    Note = department.Note,
                    NoOfBatches = department.NoOfBatches,
                    SaleOrderId = department.SaleOrderId,
                    SaleOrderNo = department.SaleOrder.RegistrationNo,
                    RecipeNoId = department.RecipeNoId,
                    RecipeName = department.RecipeNo.RecipeName,
                    IsActive = department.IsActive,
                    IsClose = department.IsClose,
                    StartTime = department.StartTime == null ? null : department.StartTime.Value.ToString("MM/dd/yyyy hh:mm"),
                    EndTime = department.EndTime == null ? null : department.EndTime.Value.ToString("MM/dd/yyyy hh:mm"),
                    CompleteBy = department.CompleteBy,
                    ProcessBy = department.ProcessBy,
                    TransferBy = department.TransferBy,
                    DamageStock = department.DamageStock,
                    RemainingStock = department.RemainingStock,
                    LateReason = department.LateReason,
                    LateReasonCompletion = department.LateReasonCompletion,
                    CompleteDate = department.CompleteDate == null ? null : department.CompleteDate.Value.ToString("MM/dd/yyyy hh:mm"),
                    ProcessDate = department.CompleteDate == null ? null : department.ProcessDate.Value.ToString("MM/dd/yyyy hh:mm"),
                    TransferDate = department.CompleteDate == null ? null : department.TransferDate.Value.ToString("MM/dd/yyyy hh:mm"),
                    RecipeQuantity = department.RecipeQuantity,


                    //ProductionBatchItems = department.ProductionBatchItems,

                };
            }
        }

        

        public static ProductionBatchDetailLookUpModel Create(ProductionBatch productionBatch)
        {
            return Projection.Compile().Invoke(productionBatch);
        }
    }
}
