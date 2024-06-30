using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksTransfer.Commands
{
    public class AddStockTransferVoucherCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AddStockTransferVoucherCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddStockTransferVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<bool> Handle(AddStockTransferVoucherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    // AutoCode
                    var journalVoucher = await _context.JournalVouchers.AsNoTracking()
                        .OrderBy(x => x.VoucherNumber)
                        .Where(x => x.IsStockTransfer && x.BranchId==request.BranchId)
                        .LastOrDefaultAsync(cancellationToken);

                    var autoCode = journalVoucher != null ? GenerateNewCode(journalVoucher.VoucherNumber) : GenerateCodeFirstTime();

                    //Stock Transfer
                    var stockReceived = await _context.StockReceived.AsNoTracking()
                                              .Include(x => x.StockReceivedItems)
                                              .FirstOrDefaultAsync(x => x.Id==request.Id, cancellationToken: cancellationToken);


                    var branch = await _context.Branches.AsNoTracking().FirstOrDefaultAsync(x => x.Id==stockReceived.FromBranchId, cancellationToken);
                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= stockReceived.Date.Date && x.PeriodEnd >= stockReceived.Date.Date, cancellationToken: cancellationToken);


                    var newJournalVoucher = new JournalVoucher
                    {
                        BranchId = stockReceived.BranchId,
                        Date = DateTime.Now,
                        ApprovalStatus = ApprovalStatus.Approved,
                        VoucherNumber = autoCode,
                        Comments = "Stock Transfer",
                        Narration = "Stock Transfer",
                        IsStockTransfer = true
                    };
                    await _context.JournalVouchers.AddAsync(newJournalVoucher, cancellationToken);


                    var inventoryAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code=="11100001", cancellationToken);
                    var branchAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code==branch.Code && x.CostCenter.Code=="270000", cancellationToken);

                    var newJournalVoucherItems = new List<JournalVoucherItem>
                    {
                        new JournalVoucherItem
                        {
                            AccountId = inventoryAccount.Id,
                            Debit = stockReceived.TotalAmount,
                            Credit = 0,
                            Description = "Stock Transfer",
                            JournalVoucherId = newJournalVoucher.Id,
                        },
                        new JournalVoucherItem
                        {
                            AccountId = branchAccount.Id,
                            Debit = 0,
                            Credit = stockReceived.TotalAmount,
                            Description = "Stock Transfer",
                            JournalVoucherId = newJournalVoucher.Id,
                        }
                    };
                    await _context.JournalVoucherItems.AddRangeAsync(newJournalVoucherItems, cancellationToken);


                    var transactionList = new List<Transaction>
                    {
                        // Inventory
                        new Transaction
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = stockReceived.Date,
                            ApprovalDate = stockReceived.Date,
                            AccountId = inventoryAccount.Id,
                            Credit = 0,
                            Debit = stockReceived.TotalAmount,
                            Description = "Stock Transfer",
                            DocumentId = stockReceived.Id,
                            TransactionType = TransactionType.StockTransferToBranch,
                            DocumentNumber = autoCode,
                            Year = stockReceived.Date.Year.ToString(),
                            ProductId = Guid.Empty,
                            BranchId = stockReceived.BranchId,
                            PeriodId = period?.Id
                        },
                        // Account Payable
                        new Transaction
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = stockReceived.Date,
                            ApprovalDate = stockReceived.Date,
                            AccountId = branchAccount.Id,
                            Credit = stockReceived.TotalAmount,
                            Debit = 0,
                            Description = "Stock Transfer",
                            DocumentId = stockReceived.Id,
                            TransactionType = TransactionType.StockTransferToBranch,
                            DocumentNumber = autoCode,
                            Year = stockReceived.Date.Year.ToString(),
                            ProductId = Guid.Empty,
                            PeriodId = period?.Id,
                            BranchId = stockReceived.BranchId
                        }
                    };
                    await _context.Transactions.AddRangeAsync(transactionList, cancellationToken);
                    
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        BranchId = request.BranchId,
                        Code = _code,
                    }, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);


                    return true;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
            }

            private string GenerateCodeFirstTime()
            {
                return "ST-00001";
            }

            private string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32(fetchNo);
                var format = "00000";
                autoNo++;
                var newCode = "ST-" + autoNo.ToString(format);
                return newCode;
            }
        }

    }
}
