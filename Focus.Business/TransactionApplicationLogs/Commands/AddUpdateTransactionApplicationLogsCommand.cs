using Focus.Business.Interface;
using Focus.Business.TransactionApplicationLogs.Model;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.TransactionApplicationLogs.Commands
{
    public class AddUpdateTransactionApplicationLogsCommand : IRequest<Guid>
    {
        public TransactionApplicationLogsLookupModel Pre { get; set; }

        public class Handler : IRequestHandler<AddUpdateTransactionApplicationLogsCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateTransactionApplicationLogsCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateTransactionApplicationLogsCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var prefix = _context.TransactionApplicationLogs
                        .FirstOrDefault();
                    if (prefix != null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        prefix.FreshLogMovingDays = request.Pre.FreshLogMovingDays;
                        prefix.DeleteFromHistory = request.Pre.DeleteFromHistory;
                        prefix.Date = request.Pre.Date;
                        prefix.IsActive = request.Pre.IsActive;
                        prefix.LocationId = request.Pre.LocationId;
                  

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return prefix.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var transactionApplicationLogs = new TransactionApplicationLog
                        {
                            FreshLogMovingDays = request.Pre.FreshLogMovingDays,
                            DeleteFromHistory = request.Pre.DeleteFromHistory,
                            Date = request.Pre.Date,
                            IsActive = request.Pre.IsActive,
                            LocationId = request.Pre.LocationId,
                            
                        };
                        await _context.TransactionApplicationLogs.AddAsync(transactionApplicationLogs, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return transactionApplicationLogs.Id;
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
