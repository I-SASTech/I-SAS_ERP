using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Focus.Business.ProductionModule.GatePasses.Commands
{
    public class AddGatePassesCommand : IRequest<Guid>
    {
        public GatePassesLookUpModel GatePass { get; set; }
        public class Handler : IRequestHandler<AddGatePassesCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddGatePassesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddGatePassesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.GatePass.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var GatePass = new GatePass()
                        {
                           
                            IsActive = request.GatePass.IsActive,
                            Date = request.GatePass.Date,
                            FromDate = request.GatePass.FromDate,
                            ToDate = request.GatePass.ToDate,
                            RegistrationNo = request.GatePass.RegistrationNo,
                            EmployeeId = request.GatePass.EmployeeId,
                            ApprovalStatus = request.GatePass.ApprovalStatus,
                            ProductionBatchId = request.GatePass.ProductionBatchId,
                            GatePassItems = request.GatePass.GatePassItems.Select(x =>
                                new GatePassItem()
                                {
                                    Id = x.Id,
                                    GatePassId = x.GatePassId,
                                    UnitPrice = x.UnitPrice,
                                    Quantity = x.Quantity,
                                    ProductId = x.ProductId
                                }).ToList(),
                               
                        };

                        await _context.GatePasses.AddAsync(GatePass, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=GatePass.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return GatePass.Id;
                    }
                    else
                    {
                        var purchase = await _context.GatePasses
                          .FindAsync(request.GatePass.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        purchase.Date = request.GatePass.Date;
                        purchase.FromDate = request.GatePass.FromDate;
                        purchase.ToDate = request.GatePass.ToDate;
                        purchase.RegistrationNo = request.GatePass.RegistrationNo;
                        purchase.EmployeeId = request.GatePass.EmployeeId;
                        purchase.ApprovalStatus = request.GatePass.ApprovalStatus;
                        purchase.ProductionBatchId = request.GatePass.ProductionBatchId;
                    
                        _context.GatePassItems.RemoveRange(purchase.GatePassItems);
                        purchase.GatePassItems = request.GatePass.GatePassItems.Select(x =>
                                                       new GatePassItem()
                                                       {
                                                           Id = x.Id,
                                                           GatePassId = x.GatePassId,
                                                           UnitPrice = x.UnitPrice,
                                                           Quantity = x.Quantity,
                                                           ProductId = x.ProductId
                                                       }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.RegistrationNo
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
