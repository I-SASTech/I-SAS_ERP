using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Reports.ProductLedgerReport
{
    public class ProductLedgerReport : IRequest<List<ProductLedgerModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid ProductId { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<ProductLedgerReport, List<ProductLedgerModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ProductLedgerReport> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ProductLedgerModel>> Handle(ProductLedgerReport request, CancellationToken cancellationToken)
            {
                try
                {
                    request.ToDate = request.ToDate.AddDays(1);

                    var ledgerAccount = await _context.LedgerAccounts
                        .AsNoTracking()
                        .Include(x => x.Account)
                        .ToListAsync(cancellationToken: cancellationToken);

                    var ledgerTransaction = await _context.LedgerTransactions
                        .Where(x => x.ProductId == request.ProductId)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        ledgerTransaction = ledgerTransaction.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }


                    var inventoryWiseLedger = ledgerTransaction.OrderBy(x => x.AccountId).GroupBy(x => x.AccountId);


                    var inventoryList = inventoryWiseLedger.Select(x => new ProductLedgerModel
                    {
                        Id = x.Key,
                        Name = ledgerAccount.FirstOrDefault(y => y.Id == x.Key) == null ? "" : ledgerAccount.FirstOrDefault(y => y.Id == x.Key).Account.Name,
                        OpeningBalance = x.Where(x => x.DocumentDate < request.FromDate).Sum(y => y.Debit - y.Credit),
                        RunningBalance = x.Where(x => x.DocumentDate < request.FromDate).Sum(y => y.Debit - y.Credit) + x.Where(x => x.DocumentDate >= request.FromDate && x.DocumentDate <= request.ToDate).Sum(y => y.Debit - y.Credit),
                        LedgerTransactionLookUpModels = x.Where(x => x.DocumentDate >= request.FromDate && x.DocumentDate <= request.ToDate).Select(tr => new LedgerTransactionLookUpModel
                        {
                            Id = tr.Id,
                            DocumentId = tr.DocumentId,
                            //DocumentDate = tr.DocumentDate == null ? string.Empty : tr.DocumentDate.Value.ToString("d"),
                            Date = tr.DocumentDate == null ? string.Empty : tr.DocumentDate.Value.ToString("d"),
                            DocumentNumber = tr.DocumentNumber,
                            Description = tr.Description,
                            Debit = tr.Debit,
                            Credit = tr.Credit,
                            TransactionType = tr.TransactionType.ToString()
                        }).ToList()
                    }).ToList();

                    return inventoryList;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
