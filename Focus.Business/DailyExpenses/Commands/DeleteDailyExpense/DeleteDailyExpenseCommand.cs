using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DailyExpenses.Commands.DeleteDailyExpense
{
    public class DeleteDailyExpensesCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteDailyExpensesCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<DeleteDailyExpensesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(DeleteDailyExpensesCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    var purchaseOrder = await _context.DailyExpenses.FindAsync(request.Id);
                    if (purchaseOrder==null)
                    {
                        throw new NotFoundException("Daily Expense not found", "");
                    }

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    _context.DailyExpenses.Remove(purchaseOrder);


                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                        BranchId = purchaseOrder.BranchId,
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
                    var message = new Message
                    {
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                    _logger.LogError(exception.Message);

                    return message;
                }
            }
        }
    }
}
