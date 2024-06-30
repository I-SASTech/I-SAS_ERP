using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using  Focus.Domain.Entities;
using Transaction = Focus.Domain.Entities.Transaction;
using Focus.Domain.Enum;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Transactions.JVTransaction
{
    public class PostTransactionCommand : IRequest<bool>
    {
        public List<Transaction> Transactions { get; set; }
        public  string Code { get; set; }
        public  string DocumentNo { get; set; }
        public class Handler : IRequestHandler<PostTransactionCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<PostTransactionCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }

            public async Task<bool> Handle(PostTransactionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var transactions = new List<Transaction>();

                    foreach (var transaction in request.Transactions)
                    {
                        var account = await _context.Accounts.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == transaction.AccountId, cancellationToken);

                        if (account == null) throw new NotFoundException("Account", transaction.AccountId);

                        if (!Enum.IsDefined(typeof(TransactionType), transaction.TransactionType))
                            throw new NotFoundException("Posting TransactionType", transaction.TransactionType);

                        if (transaction.DocumentId == Guid.Empty)
                            throw new NotFoundException("Posting Document", transaction.DocumentId);

                        if (transaction.Date == default(DateTime))
                            throw new NotFoundException("Posting Date", transaction.Date);


                     

                        
                        if (transaction.Credit != 0 || transaction.Debit != 0)
                        {
                           

                           

                            transactions.Add(new Transaction()
                            {
                                TransactionType = transaction.TransactionType,
                                DocumentId = transaction.DocumentId,
                                DocumentNumber = transaction.DocumentNumber,
                                Date = DateTime.UtcNow,
                                DocumentDate = transaction.DocumentDate,
                                ApprovalDate = transaction.ApprovalDate,
                                Year = transaction.Date.Year.ToString(),
                                Debit = transaction.Debit,
                                Credit = transaction.Credit,
                                Description = transaction.Description,
                                PeriodId = transaction.PeriodId,
                             
                                AccountId = transaction.AccountId,

                            });
                        }
                    }

                    await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = request.Code,
                        DocumentNo = request.DocumentNo,
                    }, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (NotFoundException e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }
               
            }

        }
    }
}
