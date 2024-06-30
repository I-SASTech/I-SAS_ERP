using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Reports.TrialBalanceReport;
using Focus.Business.Reports.TrialBalanceTreeReport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Focus.Business.Transactions.JVTransaction
{ 
    public class TransactionQueryofLedgerAccountWise : PagedRequest, IRequest<List<CostCenterLookUpModel>>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid AccountID { get; set; }
        public bool isLedger { get; set; }
        public string DateType { get; set; }
        public List<Guid> AccountsId { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<TransactionQueryofLedgerAccountWise, List<CostCenterLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TransactionQueryofLedgerAccountWise> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<CostCenterLookUpModel>> Handle(TransactionQueryofLedgerAccountWise request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Transactions
                        .Include(x => x.Account)
                        .ThenInclude(x => x.CostCenter)
                        .AsNoTracking()
                         .Where(x => request.AccountsId.Contains(x.AccountId.Value))
                        .AsEnumerable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    //query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                    var groupBYRecord = query.GroupBy(x => x.Account.Name);
                    var inventoryList = new List<CostCenterLookUpModel>();
                    foreach (var x in groupBYRecord)
                    {
                        var trialBalances = new List<TrialBalanceModel>();
                        var runningBalance = x.Where(x => x.Date.Date < request.FromDate.Date).Sum(x => x.Debit - x.Credit);
                        var TrialBalanceModel = x.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date && (x.Debit != 0 || x.Credit != 0)).Select(tr => new TrialBalanceModel
                        {
                            Id = tr.Id,
                            AccountName = tr.Account.Code + "--" + tr.Account.Name,
                            Description = tr.Description==""?tr.TransactionType.ToString(): tr.Description,
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
                        inventoryList.Add(new CostCenterLookUpModel()
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

                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
