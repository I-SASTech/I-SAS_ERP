using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Transactions.Commands
{
   public class AddTransactionCommand : IRequest<Guid>
    {
        public TransactionLookupModel Transaction { get; set; }
        public string Code { get; set; }
        public Guid? BranchId { get; set; }
        public class Handler : IRequestHandler<AddTransactionCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddTransactionCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                   
                    if (request.Transaction == null)
                    {
                        throw new ApplicationException("Inventory is not found");
                    }
                    var transaction = new Transaction()
                    {
                        DocumentNumber = request.Transaction.DocumentNumber,
                        AccountId = request.Transaction.AccountId,
                        ContactId = request.Transaction.ContactId,
                        Credit = request.Transaction.Credit,
                        Debit = request.Transaction.Debit,
                        Description = request.Transaction.Description,
                        DocumentId = request.Transaction.DocumentId,
                        Date = DateTime.UtcNow,
                        DocumentDate = request.Transaction.DocumentDate,
                        ApprovalDate = request.Transaction.ApprovalDate,
                        TransactionType = request.Transaction.TransactionType,
                        Year = request.Transaction.Date.Year.ToString(),
                        BranchId = request.Transaction.BranchId,
                    };

                    await _context.Transactions.AddAsync(transaction, cancellationToken);
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = request.Code,
                        BranchId = request.BranchId,
                        DocumentNo=transaction.DocumentNumber
                    }, cancellationToken);
                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                    // Return Product id after successfully updating data
                    return transaction.Id;
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
