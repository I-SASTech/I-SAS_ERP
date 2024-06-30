using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.ProductionModule.GatePasses.Queries.GetGatePassesRegistrationNo;
using Focus.Business.ProductionModule.SampleRequests;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionBatchs.ProcessContractor
{
    public class AddContractorCommand : IRequest<Guid>
    {
        public ProcessContractorLookup ContractorLookup { get; set; }
        public class Handler : IRequestHandler<AddContractorCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddContractorCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }

            public async Task<Guid> Handle(AddContractorCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    {
                        if (request.ContractorLookup.ApprovalStatus == ApprovalStatus.Complete)
                        {

                            Random rnd = new Random();
                            for (int i = 0; i < 11; i++)
                            {
                                _code += rnd.Next(0, 9).ToString();
                            }

                            //var contractor = await _context.BatchProcessContractors
                            //    .AsNoTracking()
                            //    .FirstOrDefaultAsync(x=>x.Id==request.ContractorLookup.Id,cancellationToken);
                            //contractor.ApprovalStatus = request.ContractorLookup.ApprovalStatus;
                            //_context.BatchProcessContractors.Update(contractor);
                            //var batchProcess = await _context.BatchProcesses
                            //    .AsNoTracking()
                            //    .Include(x=>x.BatchProcessContractors)
                            //    .FirstOrDefaultAsync(x => x.Id == request.ContractorLookup.BatchProcessId, cancellationToken);
                            var batch = await _context.ProductionBatchs
                               .AsNoTracking()
                               .Include(x => x.BatchProcesses)
                               .ThenInclude(x => x.BatchProcessContractors)
                               .FirstOrDefaultAsync(x => x.Id == request.ContractorLookup.ProductionBatchId, cancellationToken);
                            if (batch != null)
                            {
                                int i = 0;
                                foreach (var process in batch.BatchProcesses)
                                {
                                    if (i == 1)
                                    {
                                         process.ApprovalStatus = ApprovalStatus.InProcess;
                                         process.IsActive = true;
                                        i++;


                                    }

                                    foreach (var contractor in process.BatchProcessContractors)
                                    {
                                        if (contractor.Id == request.ContractorLookup.Id)
                                        {
                                            contractor.ApprovalStatus = request.ContractorLookup.ApprovalStatus;

                                        }

                                    }
                                    if (process.Id == request.ContractorLookup.BatchProcessId)
                                    {
                                        process.IsActive = false;
                                        process.ApprovalStatus = process.BatchProcessContractors.All(x => x.ApprovalStatus == ApprovalStatus.Complete) == true ? ApprovalStatus.Complete : ApprovalStatus.InProcess;
                                        i++;
                                        continue;
                                    }


                                }
                            }


                            //if (batchProcess != null)
                            //{

                            //    batchProcess.IsActive = true;
                            //    batchProcess.ApprovalStatus = batchProcess.BatchProcessContractors.All(x => x.ApprovalStatus == ApprovalStatus.Complete) == true ? ApprovalStatus.Complete :  ApprovalStatus.InProcess;
                            //    _context.BatchProcesses.Update(batchProcess);

                            //}
                            _context.ProductionBatchs.Update(batch);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                

                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);

                            if (request.ContractorLookup.Id == null)
                            {
                                return Guid.Empty;
                            }
                            else
                            {
                                return Guid.Empty;
                            }

                        }
                        else
                        {

                            Random rnd = new Random();
                            for (int i = 0; i < 11; i++)
                            {
                                _code += rnd.Next(0, 9).ToString();
                            }

                            var contractor = await _context.BatchProcessContractors
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == request.ContractorLookup.Id, cancellationToken);



                            var batchProcess = await _context.BatchProcesses
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == request.ContractorLookup.BatchProcessId, cancellationToken);
                            if (batchProcess != null)
                            {
                                batchProcess.IsActive = true;
                                _context.BatchProcesses.Update(batchProcess);
                            }
                            contractor.FromDate = request.ContractorLookup.FromDate;
                            contractor.ToDate = request.ContractorLookup.ToDate;
                            contractor.Comments = request.ContractorLookup.Comments;
                            contractor.ContractorType = request.ContractorLookup.ContractorType;
                            contractor.ApprovalStatus = request.ContractorLookup.ApprovalStatus;

                            contractor.ContractorItems = request.ContractorLookup.ContractorItems.Select(x =>
                                                            new ContractorItem()
                                                            {
                                                                ProductId = x.ProductId,
                                                                HighQuantity = x.HighQuantity,
                                                                UnitPerPack = x.UnitPerPack,
                                                                Quantity = x.Quantity,
                                                                BatchProcessContractorId = request.ContractorLookup.Id.Value,
                                                                WarehouseId = x.WareHouseId,

                                                            }).ToList();
                            _context.BatchProcessContractors.Update(contractor);

                            var productionBatch = _context.ProductionBatchs
                                .AsNoTracking()
                                .Include(x => x.ProductionBatchItems)
                                .FirstOrDefault(x => x.Id == request.ContractorLookup.ProductionBatchId);
                            if (productionBatch != null)
                            {
                                foreach (var productionItems in productionBatch.ProductionBatchItems)
                                {
                                    foreach (var contractorItem in contractor.ContractorItems)
                                    {
                                        if (productionItems.ProductId == contractorItem.ProductId)
                                        {
                                            productionItems.RemainingQuantity = productionItems.RemainingQuantity -
                                                contractorItem.Quantity;

                                            _context.ProductionBatchItems.Update(productionItems);

                                        }

                                    }
                                }
                            }

                            var gatePass = new GatePass();
                            if (request.ContractorLookup.ContractorType == "Out Door")
                            {
                                var gatePassNo = await _mediator.Send(new GetGatePassesRegistrationNoQuery(), cancellationToken);
                                Guid gatePassId = Guid.NewGuid();
                                gatePass = new GatePass()
                                {
                                    Id = gatePassId,
                                    IsActive = true,
                                    IsGatePass = true,
                                    Date = DateTime.UtcNow,
                                    BatchProcessContractorId = contractor.Id,
                                    FromDate = contractor.FromDate,
                                    ToDate = contractor.ToDate,
                                    RegistrationNo = gatePassNo,
                                    ContractorType = contractor.ContractorType,
                                    EmployeeId = contractor.ContractorId,
                                    Note = contractor.Comments,
                                    ApprovalStatus = contractor.ApprovalStatus,
                                    ProductionBatchId = contractor.ProductionBatchId,
                                    GatePassItems = request.ContractorLookup.ContractorItems.Select(x =>
                                        new GatePassItem()
                                        {
                                            GatePassId = gatePassId,
                                            UnitPrice = x.UnitPerPack,
                                            Quantity = x.Quantity,
                                            HighQuantity = x.HighQuantity,
                                            ProductId = x.ProductId,
                                            WareHouseId = x.WareHouseId
                                        }).ToList(),

                                };

                                await _context.GatePasses.AddAsync(gatePass, cancellationToken);
                            }
                            else
                            {
                                var gatePassNo = await _mediator.Send(new GetGatePassesRegistrationNoQuery(), cancellationToken);
                                Guid gatePassId = Guid.NewGuid();
                                gatePass = new GatePass()
                                {
                                    Id = gatePassId,
                                    IsActive = true,
                                    IsGatePass = false,
                                    Date = DateTime.UtcNow,
                                    BatchProcessContractorId = contractor.Id,
                                    FromDate = contractor.FromDate,
                                    ToDate = contractor.ToDate,
                                    RegistrationNo = gatePassNo,
                                    Note = contractor.Comments,
                                    ContractorType = contractor.ContractorType,
                                    EmployeeId = contractor.ContractorId,
                                    ApprovalStatus = contractor.ApprovalStatus,
                                    ProductionBatchId = contractor.ProductionBatchId,
                                    GatePassItems = request.ContractorLookup.ContractorItems.Select(x =>
                                        new GatePassItem()
                                        {
                                            GatePassId = gatePassId,
                                            UnitPrice = x.UnitPerPack,
                                            Quantity = x.Quantity,
                                            HighQuantity = x.HighQuantity,
                                            ProductId = x.ProductId,
                                            WareHouseId = x.WareHouseId
                                        }).ToList(),

                                };

                                await _context.GatePasses.AddAsync(gatePass, cancellationToken);

                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo= gatePass.RegistrationNo
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return contractor.Id;
                        }

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
