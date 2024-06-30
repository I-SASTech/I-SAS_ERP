using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;

namespace Focus.Business.JournalVouchers.Commands.DeleteJV

{
    public class DeleteJournalVoucherCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteJournalVoucherCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<DeleteJournalVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(DeleteJournalVoucherCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    var jv = await _context.JournalVouchers.FindAsync(request.Id);
                    if (jv == null)
                        throw new NotFoundException("Journal Vouchers", "");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    _context.JournalVouchers.Remove(jv);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                        BranchId = jv.BranchId,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = request.Id,
                        IsAddUpdate = "Data has been deleted successfully"
                    };
                    return message;
                }
               
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    return new Message
                    {
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                }
            }
        }
    }
}
