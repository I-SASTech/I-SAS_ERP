using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceAccountLedger.Models;
using Focus.Business.Reports.AdvanceTrialBalanceReport.Models;
using Focus.Business.Transactions.Commands;
using Focus.Business.Transactions.JVTransaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceAccountLedger.Queries
{
    public class GetAdvanceAccountLedgerQuery : IRequest<AdvanceLedgerAccountLookupModel>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid AccountID { get; set; }
        public bool isLedger { get; set; }
        public string DateType { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceAccountLedgerQuery, AdvanceLedgerAccountLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAdvanceAccountLedgerQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<AdvanceLedgerAccountLookupModel> Handle(GetAdvanceAccountLedgerQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Transactions
                           .Include(x => x.Account)
                           .AsNoTracking()
                           .Where(x => x.AccountId == request.AccountID)
                           .AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(request.BranchId));
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);


                            var opening = query.Where(x => x.Date.Date < firstDayOfYear.Date)
                           .Sum(x => x.Debit - x.Credit);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);
                            query1 = query1.OrderBy(x => x.Date);

                            var result = new List<TransactionDto>();
                            decimal runningBalance = opening;
                            foreach (var tr in query1)
                            {
                                if (tr.Debit > 0 || tr.Credit > 0)
                                {
                                    result.Add(new TransactionDto()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                        DocumentId = tr.DocumentId,
                                        Date = tr.Date.ToString("d"),
                                        DocumentDate = tr.DocumentDate?.ToString("d"),
                                        ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                        DocumentNumber = tr.DocumentNumber,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        DebitAmount = tr.Debit,
                                        CreditAmount = tr.Credit,
                                        TransactionType = tr.TransactionType.ToString(),
                                        OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                    });

                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }

                            }

                            advanceLedger.Add(new AdvanceLedgerAccountLookupModel()
                            {
                                TransactionList = result,
                                TotalDebit = result.Sum(x => x.DebitAmount),
                                TotalCredit = result.Sum(x => x.CreditAmount),
                                OpeningBalance = opening,
                                RunningBalance = runningBalance,
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                            });
                        }

                        return new AdvanceLedgerAccountLookupModel
                        {
                            CompareWithList = advanceLedger,
                        };
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);
                            var opening = query.Where(x => x.Date.Date < firstDayOfYear.Date)
                                                       .Sum(x => x.Debit - x.Credit);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);
                            query1 = query1.OrderBy(x => x.Date);

                            var result = new List<TransactionDto>();
                            decimal runningBalance = opening;
                            foreach (var tr in query1)
                            {
                                if (tr.Debit > 0 || tr.Credit > 0)
                                {
                                    result.Add(new TransactionDto()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                        DocumentId = tr.DocumentId,
                                        Date = tr.Date.ToString("d"),
                                        DocumentDate = tr.DocumentDate?.ToString("d"),
                                        ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                        DocumentNumber = tr.DocumentNumber,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        DebitAmount = tr.Debit,
                                        CreditAmount = tr.Credit,
                                        TransactionType = tr.TransactionType.ToString(),
                                        OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                    });

                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }

                            }

                            advanceLedger.Add(new AdvanceLedgerAccountLookupModel()
                            {
                                TransactionList = result,
                                TotalDebit = result.Sum(x => x.DebitAmount),
                                TotalCredit = result.Sum(x => x.CreditAmount),
                                OpeningBalance = opening,
                                RunningBalance = runningBalance,
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                            });
                        }
                        return new AdvanceLedgerAccountLookupModel
                        {
                            CompareWithList = advanceLedger,
                        };
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();


                        DateTime currentDate = DateTime.Now;

                        for (int q = 1; q <= comparePeriod; q++)
                        {
                            int year = currentDate.Year;
                            int quarter = (currentDate.Month - 1) / 3 + 1;

                            if (quarter - q < 0)
                            {
                                year--;
                                quarter = quarter + 4 - q;
                            }
                            else
                            {
                                quarter = quarter - q;
                            }

                            DateTime startDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime endDate = startDate.AddMonths(3).AddDays(-1);
                            var opening = query.Where(x => x.Date.Date < startDate.Date)
                                                            .Sum(x => x.Debit - x.Credit);

                            var query1 = query.Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
                            query1 = query1.OrderBy(x => x.Date);

                            var result = new List<TransactionDto>();
                            decimal runningBalance = opening;
                            foreach (var tr in query1)
                            {
                                if (tr.Debit > 0 || tr.Credit > 0)
                                {
                                    result.Add(new TransactionDto()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                        DocumentId = tr.DocumentId,
                                        Date = tr.Date.ToString("d"),
                                        DocumentDate = tr.DocumentDate?.ToString("d"),
                                        ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                        DocumentNumber = tr.DocumentNumber,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        DebitAmount = tr.Debit,
                                        CreditAmount = tr.Credit,
                                        TransactionType = tr.TransactionType.ToString(),
                                        OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                    });

                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }

                            }

                            advanceLedger.Add(new AdvanceLedgerAccountLookupModel()
                            {
                                TransactionList = result,
                                TotalDebit = result.Sum(x => x.DebitAmount),
                                TotalCredit = result.Sum(x => x.CreditAmount),
                                OpeningBalance = opening,
                                RunningBalance = runningBalance,
                                CompareWith = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy"),
                            });
                        }
                        return new AdvanceLedgerAccountLookupModel
                        {
                            CompareWithList = advanceLedger,
                        };
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDates = DateTime.Now.AddMonths(-i);
                            DateTime toDates = DateTime.Now.AddMonths(-(i - 1));
                            var opening = query.Where(x => x.Date.Date < fromDates.Date)
                                                               .Sum(x => x.Debit - x.Credit);

                            var query1 = query.Where(x => x.Date.Date >= fromDates.Date && x.Date.Date <= toDates.Date);
                            query1 = query1.OrderBy(x => x.Date);

                            var result = new List<TransactionDto>();
                            decimal runningBalance = opening;
                            foreach (var tr in query1)
                            {
                                if (tr.Debit > 0 || tr.Credit > 0)
                                {
                                    result.Add(new TransactionDto()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                        DocumentId = tr.DocumentId,
                                        Date = tr.Date.ToString("d"),
                                        DocumentDate = tr.DocumentDate?.ToString("d"),
                                        ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                        DocumentNumber = tr.DocumentNumber,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        DebitAmount = tr.Debit,
                                        CreditAmount = tr.Credit,
                                        TransactionType = tr.TransactionType.ToString(),
                                        OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                    });

                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }

                            }

                            advanceLedger.Add(new AdvanceLedgerAccountLookupModel()
                            {
                                TransactionList = result,
                                TotalDebit = result.Sum(x => x.DebitAmount),
                                TotalCredit = result.Sum(x => x.CreditAmount),
                                OpeningBalance = opening,
                                RunningBalance = runningBalance,
                                CompareWith = fromDates.ToString("dd/MM/yyyy") + " - " + toDates.ToString("dd/MM/yyyy"),
                            });
                        }
                        return new AdvanceLedgerAccountLookupModel
                        {
                            CompareWithList = advanceLedger,
                        };
                    }
                    else
                    {
                        var opening = query.Where(x => x.Date.Date < request.FromDate.Date)
                           .Sum(x => x.Debit - x.Credit);
                        query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        query = query.OrderBy(x => x.Date);

                        var result = new List<TransactionDto>();
                        decimal runningBalance = opening;
                        foreach (var tr in query)
                        {
                            if (tr.Debit > 0 || tr.Credit > 0)
                            {
                                result.Add(new TransactionDto()
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    DocumentId = tr.DocumentId,
                                    Date = tr.Date.ToString("d"),
                                    DocumentDate = tr.DocumentDate?.ToString("d"),
                                    ApprovalDate = tr.ApprovalDate?.ToString("d"),
                                    DocumentNumber = tr.DocumentNumber,
                                    Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                    DebitAmount = tr.Debit,
                                    CreditAmount = tr.Credit,
                                    TransactionType = tr.TransactionType.ToString(),
                                    OpeningBalance = runningBalance + (tr.Debit - tr.Credit),
                                });

                                runningBalance = runningBalance + (tr.Debit - tr.Credit);
                            }

                        }
                        return new AdvanceLedgerAccountLookupModel
                        {
                            TransactionList = result,
                            TotalDebit = result.Sum(x => x.DebitAmount),
                            TotalCredit = result.Sum(x => x.CreditAmount),
                            OpeningBalance = opening,
                            RunningBalance = runningBalance
                        };
                    }
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
