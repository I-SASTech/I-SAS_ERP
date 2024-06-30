using Focus.Business.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;

namespace Focus.Business.Sales.Commands.SaveVerifiedInvoice
{
    public class SaveVerifiedInvoicesCommand : IRequest<Message>
    {
        public List<Guid> VerifiedInvoicesList { get; set; }
        public bool Status { get; set; }

        public class Handler : IRequestHandler<SaveVerifiedInvoicesCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;


            public Handler(IApplicationDbContext context, ILogger<SaveVerifiedInvoicesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }
            public async Task<Message> Handle(SaveVerifiedInvoicesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.VerifiedInvoicesList!=null && request.VerifiedInvoicesList.Any())
                    {
                        foreach (var invoice in request.VerifiedInvoicesList)
                        {
                            var sale = await _context.Sales.FindAsync(invoice);
                            if (sale!=null)
                            {
                                sale.MarkAsCompleted = request.Status;
                            }
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }


                    return new Message()
                    {

                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new Exception(exception.Message);
                }

            }
            
        }

    }
}
