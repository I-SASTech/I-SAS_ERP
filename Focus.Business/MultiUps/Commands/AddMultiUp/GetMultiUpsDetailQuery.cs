using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.MultiUps.Commands.AddMultiUp
{
    public class GetMultiUpDetailQuery : IRequest<MultiUpLookUp>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }
        public string IsPrint { get; set; }
        public DateTime dateTime { get; set; }


        public class Handler : IRequestHandler<GetMultiUpDetailQuery, MultiUpLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetMultiUpDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<MultiUpLookUp> Handle(GetMultiUpDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.MultiUps.FindAsync(request.Id);


                    if (purchaseOrder != null)
                    {








                        if(request.IsPrint== "In Progress")
                        {
                            return new MultiUpLookUp
                            {
                                Id = purchaseOrder.Id,
                                Date = purchaseOrder.Date,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                CustomerId = purchaseOrder.CustomerId,
                                EmployeeId = purchaseOrder.EmployeeId,
                                CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                                Mobile = purchaseOrder.Customer?.ContactNo1,
                                Address = purchaseOrder.Customer?.Address,
                                EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                                Status = purchaseOrder.Status,
                                Remarks = purchaseOrder.Remarks,
                                EstimateAmount = purchaseOrder.EstimateAmount,
                                ReceivedDate = purchaseOrder.ReceivedDate,
                                ExpectedDate = purchaseOrder.ExpectedDate,
                                CompleteDate = purchaseOrder.CompleteDate,
                                JobAssignId = purchaseOrder.JobAssignId,
                                RegisteredById = purchaseOrder.RegisteredById,
                                IsComplete = purchaseOrder.IsComplete,
                                CradNumber = purchaseOrder.CradNumber,
                                PurchaseDate = purchaseOrder.PurchaseDate,
                                DealerRef = purchaseOrder.DealerRef,
                                FaultInfo = purchaseOrder.FaultInfo,
                                IsCollected = purchaseOrder.IsCollected,
                                IsReturn = purchaseOrder.IsReturn,
                                IsRepared = purchaseOrder.IsRepared,
                                Dates = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                AdvanceAmount = purchaseOrder.AdvanceAmount,
                                Payment = purchaseOrder.AdvanceAmount - purchaseOrder.CashAmount,
                                CashAmount = purchaseOrder.CashAmount,
                                IsCashed = purchaseOrder.IsCashed,
                                PaymentType = purchaseOrder.PaymentType,
                                MultiUpsLineItems = purchaseOrder.MultiUPSLineItems.Where(x=>x.Status== "In Progress" && x.CompleteDate==request.dateTime).Select(x =>
                                               new MultiUpsLineItemLookUp()
                                               {
                                                   Id = x.Id,
                                                   WarrantyCategoryId = x.WarrantyCategoryId,
                                                   WarrantyCategory = x.WarrantyCategory?.Name,
                                                   CompleteDate = x.CompleteDate,
                                                   Status = x.Status,
                                                   UpsDescriptionId = x.UpsDescriptionId,
                                                   UpsDescription = x.UpsDescription?.Name,
                                                   AcessoryIncludedId = x.AcessoryIncludedId,
                                                   AcessoryIncluded = x.AcessoryInclude?.Name,
                                                   ProblemId = x.ProblemId,
                                                   Problem = x.Problem?.Name,
                                                   JobAssignId = x.JobAssignId,
                                                   SerialNo = x.SerialNo,
                                                   IsComplete = x.IsComplete,
                                                   EstimateAmount = x.EstimateAmount,

                                               }).ToList(),



                            };
                        } 
                        else if(request.IsPrint== "Complete")
                        {
                            return new MultiUpLookUp
                            {
                                Id = purchaseOrder.Id,
                                Date = purchaseOrder.Date,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                CustomerId = purchaseOrder.CustomerId,
                                EmployeeId = purchaseOrder.EmployeeId,
                                CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                                Mobile = purchaseOrder.Customer?.ContactNo1,
                                Address = purchaseOrder.Customer?.Address,
                                EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                                Status = purchaseOrder.Status,
                                Remarks = purchaseOrder.Remarks,
                                EstimateAmount = purchaseOrder.EstimateAmount,
                                ReceivedDate = purchaseOrder.ReceivedDate,
                                ExpectedDate = purchaseOrder.ExpectedDate,
                                CompleteDate = purchaseOrder.CompleteDate,
                                JobAssignId = purchaseOrder.JobAssignId,
                                RegisteredById = purchaseOrder.RegisteredById,
                                IsComplete = purchaseOrder.IsComplete,
                                CradNumber = purchaseOrder.CradNumber,
                                PurchaseDate = purchaseOrder.PurchaseDate,
                                DealerRef = purchaseOrder.DealerRef,
                                FaultInfo = purchaseOrder.FaultInfo,
                                IsCollected = purchaseOrder.IsCollected,
                                IsReturn = purchaseOrder.IsReturn,
                                IsRepared = purchaseOrder.IsRepared,
                                Dates = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                AdvanceAmount = purchaseOrder.AdvanceAmount,
                                Payment = purchaseOrder.AdvanceAmount - purchaseOrder.CashAmount,
                                CashAmount = purchaseOrder.CashAmount,
                                IsCashed = purchaseOrder.IsCashed,
                                PaymentType = purchaseOrder.PaymentType,
                                MultiUpsLineItems = purchaseOrder.MultiUPSLineItems.Where(x=>x.Status == "Complete" && x.IsComplete && x.CompleteDate == request.dateTime).Select(x =>
                                               new MultiUpsLineItemLookUp()
                                               {
                                                   Id = x.Id,
                                                   WarrantyCategoryId = x.WarrantyCategoryId,
                                                   WarrantyCategory = x.WarrantyCategory?.Name,
                                                   CompleteDate = x.CompleteDate,
                                                   Status = x.Status,
                                                   UpsDescriptionId = x.UpsDescriptionId,
                                                   UpsDescription = x.UpsDescription?.Name,
                                                   AcessoryIncludedId = x.AcessoryIncludedId,
                                                   AcessoryIncluded = x.AcessoryInclude?.Name,
                                                   ProblemId = x.ProblemId,
                                                   Problem = x.Problem?.Name,
                                                   JobAssignId = x.JobAssignId,
                                                   SerialNo = x.SerialNo,
                                                   IsComplete = x.IsComplete,
                                                   EstimateAmount = x.EstimateAmount,

                                               }).ToList(),



                            };
                        }
                        else
                        {
                            return new MultiUpLookUp
                            {
                                Id = purchaseOrder.Id,
                                Date = purchaseOrder.Date,
                                RegistrationNo = purchaseOrder.RegistrationNo,
                                CustomerId = purchaseOrder.CustomerId,
                                EmployeeId = purchaseOrder.EmployeeId,
                                CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                                Mobile = purchaseOrder.Customer?.ContactNo1,
                                Address = purchaseOrder.Customer?.Address,
                                EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                                Status = purchaseOrder.Status,
                                Remarks = purchaseOrder.Remarks,
                                EstimateAmount = purchaseOrder.EstimateAmount,
                                ReceivedDate = purchaseOrder.ReceivedDate,
                                ExpectedDate = purchaseOrder.ExpectedDate,
                                CompleteDate = purchaseOrder.CompleteDate,
                                JobAssignId = purchaseOrder.JobAssignId,
                                RegisteredById = purchaseOrder.RegisteredById,
                                IsComplete = purchaseOrder.IsComplete,
                                CradNumber = purchaseOrder.CradNumber,
                                PurchaseDate = purchaseOrder.PurchaseDate,
                                DealerRef = purchaseOrder.DealerRef,
                                FaultInfo = purchaseOrder.FaultInfo,
                                IsCollected = purchaseOrder.IsCollected,
                                IsReturn = purchaseOrder.IsReturn,
                                IsRepared = purchaseOrder.IsRepared,
                                Dates = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                AdvanceAmount = purchaseOrder.AdvanceAmount,
                                Payment = purchaseOrder.AdvanceAmount - purchaseOrder.CashAmount,
                                CashAmount = purchaseOrder.CashAmount,
                                IsCashed = purchaseOrder.IsCashed,
                                PaymentType = purchaseOrder.PaymentType,
                                MultiUpsLineItems = purchaseOrder.MultiUPSLineItems.Select(x =>
                                               new MultiUpsLineItemLookUp()
                                               {
                                                   Id = x.Id,
                                                   WarrantyCategoryId = x.WarrantyCategoryId,
                                                   WarrantyCategory = x.WarrantyCategory?.Name,
                                                   CompleteDate = x.CompleteDate,
                                                   Status = x.Status,
                                                   UpsDescriptionId = x.UpsDescriptionId,
                                                   UpsDescription = x.UpsDescription?.Name,
                                                   AcessoryIncludedId = x.AcessoryIncludedId,
                                                   AcessoryIncluded = x.AcessoryInclude?.Name,
                                                   ProblemId = x.ProblemId,
                                                   IsComplete = x.IsComplete,
                                                   Problem = x.Problem?.Name,
                                                   JobAssignId = x.JobAssignId,
                                                   SerialNo = x.SerialNo,
                                                   EstimateAmount = x.EstimateAmount,

                                               }).ToList(),



                            };
                        }


                       
                    }
                    throw new NotFoundException(nameof(purchaseOrder), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
