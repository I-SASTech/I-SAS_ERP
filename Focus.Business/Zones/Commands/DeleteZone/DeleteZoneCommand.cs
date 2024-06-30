using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Zones.Commands.DeleteZone
{
   public class DeleteZoneCommand : IRequest<Guid>
   {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<DeleteZoneCommand, Guid>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<DeleteZoneCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                var zone = await _context.Zones.FindAsync(request.Id);
                if (zone == null)
                    throw new NotFoundException("Not Found", request.Id);
                _context.Zones.Remove(zone);

                await _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                   
                    Code = _code,
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return zone.Id;
            }
        }
    }
}
