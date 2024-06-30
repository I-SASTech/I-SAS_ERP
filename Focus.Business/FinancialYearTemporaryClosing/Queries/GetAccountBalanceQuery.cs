using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.FinancialYearTemporaryClosing.Models;
using Focus.Business.Interface;
using Focus.Business.Transactions.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.FinancialYearTemporaryClosing.Queries
{
    public class GetAccountBalanceQuery : IRequest<List<CostCenterListModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsReport { get; set; }
        public List<ClosingTypePeriodListModel> ClosingTypePeriodList { get; set; }

        public class Handler : IRequestHandler<GetAccountBalanceQuery, List<CostCenterListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAccountBalanceQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<CostCenterListModel>> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsReport)
                    {
                        var filterList = request.ClosingTypePeriodList.OrderBy(x => x.FromDate).Where(x => x.IsSelect).ToList();

                        var costCenters = await _context.CostCenters
                            .AsNoTracking()
                            .Select(x => new
                            {
                                x.Id,
                                x.Name,
                                x.NameArabic,
                                accounts = x.Accounts.Select(z => new
                                {
                                    z.Id,
                                    z.Name,
                                    z.NameArabic,
                                }).ToList()
                            }).ToListAsync(cancellationToken: cancellationToken);
                        

                        var transactions = await _context.Transactions
                            .AsNoTracking()
                            .Select(x => new TransactionLookupModel
                            {
                                Date = x.Date,
                                Debit = x.Debit,
                                Credit = x.Credit,
                                AccountId = x.AccountId,
                            }).ToListAsync(cancellationToken: cancellationToken);

                        var transactionList = new List<TransactionLookupModel>();
                        foreach (var filter in filterList)
                        {
                            var filterTransactions = transactions.Where(x => x.Date.Date >= filter.FromDate && x.Date.Date <= filter.ToDate).ToList();
                            transactionList.AddRange(filterTransactions);
                        }

                        var costCenterList = new List<CostCenterListModel>();
                        foreach (var costCenter in costCenters)
                        {
                            var list = new List<AccountListModel>();
                            foreach (var account in costCenter.accounts)
                            {
                                list.Add(new AccountListModel()
                                {
                                    Name = account.Name,
                                    NameArabic = account.NameArabic,
                                    AccountId = account.Id,
                                    Debit = transactionList.Where(x => x.AccountId == account.Id).Sum(x => x.Debit),
                                    Credit = transactionList.Where(x => x.AccountId == account.Id).Sum(x => x.Credit),
                                    Balance = transactionList.Where(x => x.AccountId == account.Id).Sum(x => x.Debit - x.Credit),
                                });
                            }
                            costCenterList.Add(new CostCenterListModel()
                            {
                                Name = costCenter.Name,
                                NameArabic = costCenter.NameArabic,
                                AccountList = list
                            });
                        }
                        
                        return costCenterList;
                    }
                    else
                    {
                        var costCenters = await _context.CostCenters
                        .AsNoTracking()
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.NameArabic,
                            accounts = x.Accounts.Select(z => new
                            {
                                z.Id,
                                z.Name,
                                z.NameArabic,
                            }).ToList()
                        }).ToListAsync(cancellationToken: cancellationToken);
                        
                        var transactions = await _context.Transactions
                            .AsNoTracking()
                            .Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date)
                            .Select(x => new
                            {
                                x.Id,
                                x.Date,
                                x.Debit,
                                x.Credit,
                                x.AccountId,
                            }).ToListAsync(cancellationToken: cancellationToken);


                        var financialYearClosing = await _context.FinancialYearClosings
                            .AsNoTracking()
                            .Where(x => x.FromDate.Date >= request.FromDate.Date && x.ToDate.Date <= request.ToDate.Date)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        var costCenterList = new List<CostCenterListModel>();
                        if (financialYearClosing != null)
                        {
                            var financialYearClosingBalances = await _context.FinancialYearClosingBalances
                                .AsNoTracking()
                                .Where(x => x.FinancialYearClosingId == financialYearClosing.Id && x.ReOpen == financialYearClosing.ReOpen)
                                .ToListAsync(cancellationToken: cancellationToken);

                            foreach (var costCenter in costCenters)
                            {
                                var list = new List<AccountListModel>();
                                foreach (var account in costCenter.accounts)
                                {
                                    list.Add(new AccountListModel()
                                    {
                                        Name = account.Name,
                                        NameArabic = account.NameArabic,
                                        AccountId = account.Id,
                                        Debit = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Debit),
                                        Credit = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Credit),
                                        Balance = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Debit - x.Credit),
                                        PreviousBalance = financialYearClosingBalances.FirstOrDefault(x => x.AccountId == account.Id)?.Balance
                                    });
                                }
                                costCenterList.Add(new CostCenterListModel()
                                {
                                    Name = costCenter.Name,
                                    NameArabic = costCenter.NameArabic,
                                    AccountList = list
                                });
                            }
                        }
                        else
                        {
                            foreach (var costCenter in costCenters)
                            {
                                var list = new List<AccountListModel>();
                                foreach (var account in costCenter.accounts)
                                {
                                    list.Add(new AccountListModel()
                                    {
                                        Name = account.Name,
                                        NameArabic = account.NameArabic,
                                        AccountId = account.Id,
                                        Debit = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Debit),
                                        Credit = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Credit),
                                        Balance = transactions.Where(x => x.AccountId == account.Id).Sum(x => x.Debit - x.Credit),
                                    });
                                }
                                costCenterList.Add(new CostCenterListModel()
                                {
                                    Name = costCenter.Name,
                                    NameArabic = costCenter.NameArabic,
                                    AccountList = list
                                });
                            }
                        }

                        return costCenterList;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
