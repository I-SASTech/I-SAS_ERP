using Focus.Business.Common;
using Focus.Business.Holidays.Commands.AddHoliday;
using Focus.Business.Holidays;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.HoldTypeSetup.Models;
using System;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.HoldTypeSetup.Commands
{
    public class PermanentDeleteHoldSetupAddUpdateCommand : IRequest<Message>
    {
        public HoldSetupLookupModel HoldSetups { get; set; }
        public class Handler : IRequestHandler<PermanentDeleteHoldSetupAddUpdateCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<PermanentDeleteHoldSetupAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(PermanentDeleteHoldSetupAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.HoldSetups.Id != Guid.Empty)
                    {
                        var hold = await _context.PermanentDeleteHoldSetups.FindAsync(request.HoldSetups.Id);

                        if(hold == null)
                            throw new NotFoundException("Permanent Delete Hold Setup Not Found", "");

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        hold.HoldRecordType = request.HoldSetups.HoldRecordType;
                        hold.IsActive = request.HoldSetups.IsActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = hold.Id,
                            IsAddUpdate = "Data Has Been Added Success"
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var hold = new HoldSetup
                        {
                            HoldRecordType = request.HoldSetups.HoldRecordType,
                            IsActive = request.HoldSetups.IsActive
                        };

                        await _context.HoldSetups.AddAsync(hold);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = hold.Id,
                            IsAddUpdate = "Data Has Been Added Success"
                        };
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
