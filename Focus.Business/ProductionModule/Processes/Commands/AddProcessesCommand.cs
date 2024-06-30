using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Focus.Business.ProductionModule.Processes.Commands
{
    public class AddProcessesCommand : IRequest<Guid>
    {
        public ProcessesLookUpModel process { get; set; }

        public class Handler : IRequestHandler<AddProcessesCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddProcessesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddProcessesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.process.Id == Guid.Empty)
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var isColorExist = await _context.Processes
                           .AnyAsync(x => x.Color == request.process.Color, cancellationToken);

                        if (isColorExist)
                        {
                            throw new ObjectAlreadyExistsException("Process Color" + request.process.Color + " Already Exist");
                        }
                        else
                        {
                            var process = new Domain.Entities.Process()
                            {
                                Code = request.process.Code,
                                Color = request.process.Color,
                                EnglishName = request.process.EnglishName,
                                ArabicName = request.process.ArabicName,
                                Description = request.process.Description,
                                IsActive = request.process.IsActive,
                                Date = request.process.Date,
                                ProcessItems = request.process.ProcessItems.Select(x =>
                                    new ProcessItem()
                                    {
                                        Id = x.Id,
                                        ProcessId = x.ProcessId,
                                        ProductId = x.ProductId
                                    }).ToList(),
                                ProcessContractors = request.process.ProcessContractors.Select(x =>
                                new ProcessContractor()
                                {
                                    Id = x.Id,
                                    ProcessId = x.ProcessId,
                                    ContractorId = x.ContractorId
                                }).ToList()
                            };


                            await _context.Processes.AddAsync(process, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo=process.Code
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            // Return Product id after successfully updating data
                            return process.Id;
                        }

                    }
                    else
                    {
                        var purchase = await _context.Processes
                          .FindAsync(request.process.Id);


                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        purchase.Date = request.process.Date;
                        purchase.Code = request.process.Code;
                        purchase.Color = request.process.Color;
                        purchase.EnglishName = request.process.EnglishName;
                        purchase.ArabicName = request.process.ArabicName;
                        purchase.Description = request.process.Description;
                        purchase.IsActive = request.process.IsActive;
                        _context.ProcessItems.RemoveRange(purchase.ProcessItems);
                        purchase.ProcessItems = request.process.ProcessItems.Select(x =>
                                                       new ProcessItem()
                                                       {
                                                           Id = x.Id,
                                                           ProcessId = x.ProcessId,
                                                           ProductId = x.ProductId
                                                       }).ToList();
                        _context.ProcessContractors.RemoveRange(purchase.ProcessContractors);
                        purchase.ProcessContractors = request.process.ProcessContractors.Select(x =>
                                                        new ProcessContractor()
                                                        {
                                                            Id = x.Id,
                                                            ProcessId = x.ProcessId,
                                                            ContractorId = x.ContractorId
                                                        }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.Code,
                            
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



