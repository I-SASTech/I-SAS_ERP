using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceAccountLedger.Models;
using Focus.Business.Reports.AdvanceAccountLedgerDetailsReport.Models;
using Focus.Business.Reports.TrialBalanceReport;
using Focus.Business.Transactions.JVTransaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceAccountLedgerDetailsReport.Queries
{
    public class GetAdvanceAccountLedgerDetailsQuery : IRequest<List<AdvanceAccountLedgerDetailsLookupModel>>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid AccountID { get; set; }
        public bool isLedger { get; set; }
        public string DateType { get; set; }
        public List<Guid> AccountsId { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }


        public class Handler : IRequestHandler<GetAdvanceAccountLedgerDetailsQuery, List<AdvanceAccountLedgerDetailsLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAdvanceAccountLedgerDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<AdvanceAccountLedgerDetailsLookupModel>> Handle(GetAdvanceAccountLedgerDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = _context.Transactions
                        .Include(x => x.Account)
                        .ThenInclude(x => x.CostCenter)
                        .AsNoTracking()
                         .Where(x => request.AccountsId.Contains(x.AccountId.Value))
                        .AsEnumerable();

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var inventoryList1 = new List<AdvanceAccountLedgerDetailsLookupModel>();
                        var groupBYRecord = query.GroupBy(x => x.Account.Name);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            var inventoryList = new List<AdvanceAccountLedgerDetailsLookupModel>();

                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);
                            foreach (var x in groupBYRecord)
                            {
                                var trialBalances = new List<TrialBalanceModel>();
                                var runningBalance = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit);
                                var TrialBalanceModel = x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    Description = tr.Description == "" ? tr.TransactionType.ToString() : tr.Description,
                                    Date = tr.Date,
                                    DocumentDate = tr.DocumentDate.ToString(),
                                    TransactionDate = tr.Date.ToString(),
                                    TransactionType = tr.TransactionType.ToString(),
                                    Code = tr.Account.Code,
                                    IsActive = false,
                                    Debit = tr.Debit,
                                    Credit = tr.Credit,
                                }).OrderBy(y => y.Date).ToList();
                                foreach (var tr in TrialBalanceModel)
                                {
                                    trialBalances.Add(new TrialBalanceModel()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.AccountName,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        Date = tr.Date,
                                        DocumentDate = tr.DocumentDate.ToString(),
                                        TransactionDate = tr.Date.ToString(),
                                        Code = tr.Code,
                                        IsActive = false,
                                        Debit = tr.Debit,
                                        Credit = tr.Credit,
                                        Total = runningBalance + (tr.Debit - tr.Credit)
                                    });
                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }
                                inventoryList.Add(new AdvanceAccountLedgerDetailsLookupModel()
                                {
                                    Name = x.Key.ToString(),
                                    Opening = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit),
                                    Closing = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit)
                                              + x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Sum(x => x.Debit - x.Credit),
                                    TrialBalanceModel = trialBalances
                                });
                            }

                            inventoryList1.Add(new AdvanceAccountLedgerDetailsLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = inventoryList,
                            });
                        }
                        return inventoryList1;
                     }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;
                        var inventoryList1 = new List<AdvanceAccountLedgerDetailsLookupModel>();
                        var groupBYRecord = query.GroupBy(x => x.Account.Name);
                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            var inventoryList = new List<AdvanceAccountLedgerDetailsLookupModel>();

                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);
                            foreach (var x in groupBYRecord)
                            {
                                var trialBalances = new List<TrialBalanceModel>();
                                var runningBalance = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit);
                                var TrialBalanceModel = x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    Description = tr.Description == "" ? tr.TransactionType.ToString() : tr.Description,
                                    Date = tr.Date,
                                    DocumentDate = tr.DocumentDate.ToString(),
                                    TransactionDate = tr.Date.ToString(),
                                    TransactionType = tr.TransactionType.ToString(),
                                    Code = tr.Account.Code,
                                    IsActive = false,
                                    Debit = tr.Debit,
                                    Credit = tr.Credit,
                                }).OrderBy(y => y.Date).ToList();
                                foreach (var tr in TrialBalanceModel)
                                {
                                    trialBalances.Add(new TrialBalanceModel()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.AccountName,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        Date = tr.Date,
                                        DocumentDate = tr.DocumentDate.ToString(),
                                        TransactionDate = tr.Date.ToString(),
                                        Code = tr.Code,
                                        IsActive = false,
                                        Debit = tr.Debit,
                                        Credit = tr.Credit,
                                        Total = runningBalance + (tr.Debit - tr.Credit)
                                    });
                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }
                                inventoryList.Add(new AdvanceAccountLedgerDetailsLookupModel()
                                {
                                    Name = x.Key.ToString(),
                                    Opening = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit),
                                    Closing = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit)
                                              + x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Sum(x => x.Debit - x.Credit),
                                    TrialBalanceModel = trialBalances
                                });
                            }
                            inventoryList1.Add(new AdvanceAccountLedgerDetailsLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = inventoryList,
                            });
                        }
                        return inventoryList1;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var inventoryList1 = new List<AdvanceAccountLedgerDetailsLookupModel>();
                        var groupBYRecord = query.GroupBy(x => x.Account.Name);


                        DateTime currentDate = DateTime.Now;

                        for (int q = 1; q <= comparePeriod; q++)
                        {
                            var inventoryList = new List<AdvanceAccountLedgerDetailsLookupModel>();

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

                            DateTime firstDayOfYear = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime lastDayOfYear = firstDayOfYear.AddMonths(3).AddDays(-1);
                            foreach (var x in groupBYRecord)
                            {
                                var trialBalances = new List<TrialBalanceModel>();
                                var runningBalance = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit);
                                var TrialBalanceModel = x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    Description = tr.Description == "" ? tr.TransactionType.ToString() : tr.Description,
                                    Date = tr.Date,
                                    DocumentDate = tr.DocumentDate.ToString(),
                                    TransactionDate = tr.Date.ToString(),
                                    TransactionType = tr.TransactionType.ToString(),
                                    Code = tr.Account.Code,
                                    IsActive = false,
                                    Debit = tr.Debit,
                                    Credit = tr.Credit,
                                }).OrderBy(y => y.Date).ToList();
                                foreach (var tr in TrialBalanceModel)
                                {
                                    trialBalances.Add(new TrialBalanceModel()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.AccountName,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        Date = tr.Date,
                                        DocumentDate = tr.DocumentDate.ToString(),
                                        TransactionDate = tr.Date.ToString(),
                                        Code = tr.Code,
                                        IsActive = false,
                                        Debit = tr.Debit,
                                        Credit = tr.Credit,
                                        Total = runningBalance + (tr.Debit - tr.Credit)
                                    });
                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }
                                inventoryList.Add(new AdvanceAccountLedgerDetailsLookupModel()
                                {
                                    Name = x.Key.ToString(),
                                    Opening = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit),
                                    Closing = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit)
                                              + x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Sum(x => x.Debit - x.Credit),
                                    TrialBalanceModel = trialBalances
                                });
                            }
                            inventoryList1.Add(new AdvanceAccountLedgerDetailsLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = inventoryList,
                            });
                        }
                        return inventoryList1;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var inventoryList1 = new List<AdvanceAccountLedgerDetailsLookupModel>();
                        var groupBYRecord = query.GroupBy(x => x.Account.Name);


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            var inventoryList = new List<AdvanceAccountLedgerDetailsLookupModel>();

                            DateTime firstDayOfYear = DateTime.Now.AddMonths(-i);
                            DateTime lastDayOfYear = DateTime.Now.AddMonths(-(i - 1));
                            foreach (var x in groupBYRecord)
                            {
                                var trialBalances = new List<TrialBalanceModel>();
                                var runningBalance = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit);
                                var TrialBalanceModel = x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                                {
                                    Id = tr.Id,
                                    AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                    Description = tr.Description == "" ? tr.TransactionType.ToString() : tr.Description,
                                    Date = tr.Date,
                                    DocumentDate = tr.DocumentDate.ToString(),
                                    TransactionDate = tr.Date.ToString(),
                                    TransactionType = tr.TransactionType.ToString(),
                                    Code = tr.Account.Code,
                                    IsActive = false,
                                    Debit = tr.Debit,
                                    Credit = tr.Credit,
                                }).OrderBy(y => y.Date).ToList();
                                foreach (var tr in TrialBalanceModel)
                                {
                                    trialBalances.Add(new TrialBalanceModel()
                                    {
                                        Id = tr.Id,
                                        AccountName = tr.AccountName,
                                        Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                        Date = tr.Date,
                                        DocumentDate = tr.DocumentDate.ToString(),
                                        TransactionDate = tr.Date.ToString(),
                                        Code = tr.Code,
                                        IsActive = false,
                                        Debit = tr.Debit,
                                        Credit = tr.Credit,
                                        Total = runningBalance + (tr.Debit - tr.Credit)
                                    });
                                    runningBalance = runningBalance + (tr.Debit - tr.Credit);
                                }
                                inventoryList.Add(new AdvanceAccountLedgerDetailsLookupModel()
                                {
                                    Name = x.Key.ToString(),
                                    CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                    Opening = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit),
                                    Closing = x.Where(x => x.Date.Date < firstDayOfYear.Date).Sum(x => x.Debit - x.Credit)
                                              + x.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date && (x.Debit != 0 || x.Credit != 0)).Sum(x => x.Debit - x.Credit),
                                    TrialBalanceModel = trialBalances
                                });
                            }
                            inventoryList1.Add(new AdvanceAccountLedgerDetailsLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = inventoryList,
                            });
                        }
                        return inventoryList1;
                    }
                    else
                    {
                        var groupBYRecord = query.GroupBy(x => x.Account.Name);
                        var inventoryList = new List<AdvanceAccountLedgerDetailsLookupModel>();
                        foreach (var x in groupBYRecord)
                        {
                            var trialBalances = new List<TrialBalanceModel>();
                            var runningBalance = x.Where(x => x.Date.Date < request.FromDate.Date).Sum(x => x.Debit - x.Credit);
                            var TrialBalanceModel = x.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                            {
                                Id = tr.Id,
                                AccountName = tr.Account.Code + "--" + tr.Account.Name,
                                Description = tr.Description == "" ? tr.TransactionType.ToString() : tr.Description,
                                Date = tr.Date,
                                DocumentDate = tr.DocumentDate.ToString(),
                                TransactionDate = tr.Date.ToString(),
                                TransactionType = tr.TransactionType.ToString(),
                                Code = tr.Account.Code,
                                IsActive = false,
                                Debit = tr.Debit,
                                Credit = tr.Credit,
                            }).OrderBy(y => y.Date).ToList();
                            foreach (var tr in TrialBalanceModel)
                            {
                                trialBalances.Add(new TrialBalanceModel()
                                {
                                    Id = tr.Id,
                                    AccountName = tr.AccountName,
                                    Description = tr.Description == "" || tr.Description == null ? tr.TransactionType.ToString() : tr.Description,
                                    Date = tr.Date,
                                    DocumentDate = tr.DocumentDate.ToString(),
                                    TransactionDate = tr.Date.ToString(),
                                    Code = tr.Code,
                                    IsActive = false,
                                    Debit = tr.Debit,
                                    Credit = tr.Credit,
                                    Total = runningBalance + (tr.Debit - tr.Credit)
                                });
                                runningBalance = runningBalance + (tr.Debit - tr.Credit);
                            }
                            inventoryList.Add(new AdvanceAccountLedgerDetailsLookupModel()
                            {
                                Name = x.Key.ToString(),
                                Opening = x.Where(x => x.Date.Date < request.FromDate.Date).Sum(x => x.Debit - x.Credit),
                                Closing = x.Where(x => x.Date.Date < request.FromDate.Date).Sum(x => x.Debit - x.Credit)
                                                  + x.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date && (x.Debit != 0 || x.Credit != 0)).Sum(x => x.Debit - x.Credit),
                                TrialBalanceModel = trialBalances
                            });
                        }
                        return inventoryList;
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
