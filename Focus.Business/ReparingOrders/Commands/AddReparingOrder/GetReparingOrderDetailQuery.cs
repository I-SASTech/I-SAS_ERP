using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrders.Commands.AddReparingOrder
{
    public class GetReparingOrderDetailQuery : IRequest<ReparingOrderLookUp>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }


        public class Handler : IRequestHandler<GetReparingOrderDetailQuery, ReparingOrderLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetReparingOrderDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ReparingOrderLookUp> Handle(GetReparingOrderDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.ReparingOrders.FindAsync(request.Id);


                    if (purchaseOrder != null)
                    {













                        return new ReparingOrderLookUp
                        {
                            Id = purchaseOrder.Id,
                            Date = purchaseOrder.Date,
                            ExpectedDates = purchaseOrder.ExpectedDate?.ToString("MMM dd, yyyy"),
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            CustomerId = purchaseOrder.CustomerId,
                            UpsDescriptionId = purchaseOrder.UpsDescriptionId,
                            EmployeeId = purchaseOrder.EmployeeId,
                            WarrantyCategoryId = purchaseOrder.WarrantyCategoryId,
                            ProblemId = purchaseOrder.ProblemId,
                            AcessoryIncludedId = purchaseOrder.AcessoryIncludedId,
                            Issue = purchaseOrder.Problem?.Name,
                            Accessor = purchaseOrder.AcessoryInclude?.Name,
                            CustomerNameEn = purchaseOrder.Customer?.EnglishName,
                            Mobile = purchaseOrder.Customer?.ContactNo1,
                            Address = purchaseOrder.Customer?.Address,
                            EmployeeNameEn = purchaseOrder.Employee?.EnglishName,
                            SerialNo = purchaseOrder.SerialNo,
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
                            RemaningPrice = purchaseOrder.RemaningPrice,
                            Discount = purchaseOrder.Discount,
                            IsCollected = purchaseOrder.IsCollected,
                            IsReturn = purchaseOrder.IsReturn,
                            IsRepared = purchaseOrder.IsRepared,
                            Dates = purchaseOrder.Date.ToString("MMM dd, yyyy"),
                            AdvanceAmount = purchaseOrder.AdvanceAmount,
                            Payment = purchaseOrder.AdvanceAmount- purchaseOrder.CashAmount ,
                            CashAmount = purchaseOrder.CashAmount,
                            IsCashed = purchaseOrder.IsCashed,
                            PaymentType = purchaseOrder.PaymentType,
                            ReparingItems = purchaseOrder.ReparingItems.Select(x =>
                                           new ReparingOrderItemLookUpModel()
                                           {
                                               Id = x.Id,
                                               ProductId = x.ProductId,
                                               Quantity = x.Quantity,
                                               Product = new ProductDropdownLookUpModel
                                               {
                                                   Id = x.Product.Id,
                                                   BarCode = x.Product.BarCode,
                                                   Code = x.Product.Code,
                                                   EnglishName = x.Product.EnglishName,
                                                   ArabicName = x.Product.ArabicName,

                                               }
                                           }).ToList(),



                        };
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
