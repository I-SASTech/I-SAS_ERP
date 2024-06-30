using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.ProductionBatchs.Commands.AddProductionBatch
{
    public class AddProductionBatchCommand : IRequest<Guid>
    {

        public ProductionBatchLookupModel ProductionBatch { get; set; }
        public class Handler : IRequestHandler<AddProductionBatchCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddProductionBatchCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddProductionBatchCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.ProductionBatch.Id == Guid.Empty)
                    {
                        var isProductionExist = await _context.ProductionBatchs.Where(x => x.RegistrationNo == request.ProductionBatch.RegistrationNo).AnyAsync(cancellationToken);
                        if (isProductionExist)
                        {
                            return Guid.Empty;
                        }

                        var productionBatch = new ProductionBatch()
                        {
                            Date = request.ProductionBatch.Date,
                            EmployeeRegistrationId = request.ProductionBatch.EmployeeRegistrationId,
                            StartTime = request.ProductionBatch.StartTime,
                            EndTime = request.ProductionBatch.EndTime,
                            RegistrationNo = request.ProductionBatch.RegistrationNo,
                            Note = request.ProductionBatch.Note,
                            NoOfBatches = request.ProductionBatch.NoOfBatches,
                            RecipeQuantity = request.ProductionBatch.RecipeQuantity,
                            SaleOrderId = request.ProductionBatch.SaleOrderId,
                            RecipeNoId = request.ProductionBatch.RecipeNoId,
                            ApprovalStatus = request.ProductionBatch.ApprovalStatus,
                            IsClose = request.ProductionBatch.IsClose,
                            BranchId = request.ProductionBatch.BranchId,
                            ProductionBatchItems = request.ProductionBatch.ProductionBatchItems.Select(x =>
                                new ProductionBatchItem()
                                {
                                    ProductId = x.ProductId,
                                    WarehouseId = x.WareHouseId,
                                    Quantity = x.Quantity,
                                    CurrentQuantity = x.Quantity * Convert.ToInt32(request.ProductionBatch.NoOfBatches),
                                    RemainingQuantity = x.Quantity * Convert.ToInt32(request.ProductionBatch.NoOfBatches),
                                    UnitPerPack = x.UnitPerPack,
                                    BasicUnit = x.BasicUnit,
                                    LevelOneUnit = x.LevelOneUnit,
                                    HighQuantity = x.HighQuantity,
                                    Waste = x.Waste,

                                }).ToList(),
                            BatchProcesses = request.ProductionBatch.ProcessList.Select(x =>
                                new BatchProcess()
                                {
                                    Code = x.Code,
                                    ProcessId = x.Id,
                                    Color = x.Color,
                                    EnglishName = x.EnglishName,
                                    ArabicName = x.ArabicName,
                                    Description = x.Description,
                                    Date = x.Date,
                                    BatchProcessContractors = x.ProcessContractors.Select(y =>
                                        new BatchProcessContractor()
                                        {
                                            ContractorId = y.ContractorId,
                                            IsActive = y.IsActive,
                                        }).ToList()

                                }).ToList()
                        };
                        await _context.ProductionBatchs.AddAsync(productionBatch, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo = productionBatch.RegistrationNo,
                            BranchId = productionBatch.BranchId
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return productionBatch.Id;
                    }
                    else
                    {
                        var purchase = await _context.ProductionBatchs.FindAsync(request.ProductionBatch.Id);
                        if (purchase == null)
                        {
                            throw new NotFoundException("Batch not found", "");
                        }
                        purchase.Date = request.ProductionBatch.Date;
                        purchase.StartTime = request.ProductionBatch.StartTime;
                        purchase.EndTime = request.ProductionBatch.EndTime;
                        purchase.RegistrationNo = request.ProductionBatch.RegistrationNo;
                        purchase.Note = request.ProductionBatch.Note;
                        purchase.NoOfBatches = request.ProductionBatch.NoOfBatches;
                        purchase.SaleOrderId = request.ProductionBatch.SaleOrderId;
                        purchase.RecipeNoId = request.ProductionBatch.RecipeNoId;
                        purchase.ApprovalStatus = request.ProductionBatch.ApprovalStatus;
                        purchase.IsClose = request.ProductionBatch.IsClose;

                        _context.ProductionBatchItems.RemoveRange(purchase.ProductionBatchItems);
                        purchase.ProductionBatchItems = request.ProductionBatch.ProductionBatchItems.Select(x =>
                                                       new ProductionBatchItem()
                                                       {
                                                           ProductId = x.ProductId,
                                                           WarehouseId = x.WareHouseId,
                                                           Quantity = x.Quantity,
                                                           HighQuantity = x.HighQuantity,
                                                           Waste = x.Waste,
                                                           UnitPerPack = x.UnitPerPack,
                                                           BasicUnit = x.BasicUnit,
                                                           LevelOneUnit = x.LevelOneUnit,

                                                       }).ToList();


                        await _mediator.Send(new AddUpdateSyncRecordCommand
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo = purchase.RegistrationNo,
                            BranchId = purchase.BranchId
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
