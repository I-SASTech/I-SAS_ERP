using Focus.Business.Interface;
using Focus.Business.Reports.TrialBalanceReport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.TrialBalanceTreeReport
{
  public class TrialBalanceTreeReport : IRequest<List<AccountTypeLookupModel>>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<TrialBalanceTreeReport, List<AccountTypeLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public Handler(IApplicationDbContext context, ILogger<TrialBalanceTreeReport> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<AccountTypeLookupModel>> Handle(TrialBalanceTreeReport request, CancellationToken cancellationToken)
            {
                try
                {




                    var listTrialBalance = new List<AccountTypeLookupModel>();
                    var query = _context.Transactions
                            .AsNoTracking()
                              .Include(x => x.Account)
                            .ThenInclude(x => x.CostCenter)
                            .ThenInclude(x => x.AccountTypes)
                            .Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date)
                            .ToList();

                    var groupByAccountType = query.GroupBy(x => x.Account.CostCenter.AccountTypes);
                    var inventoryList = groupByAccountType.Select(x => new AccountTypeLookupModel
                    {
                        Id=x.Key.Id,
                        Name = x.Key.Name,
                        IsActive = false,
                        NameArabic = x.Key.NameArabic,
                        Debit = Math.Abs(x.Sum(y => y.Debit)),
                        Credit = Math.Abs(x.Sum(y => y.Credit)),
                        Total =Convert.ToDecimal(string.Format("{0:0.##}", (Math.Abs(x.Sum(y => y.Debit))- Math.Abs(x.Sum(y => y.Credit))))),
                        CostCenterLookUpModel = x.GroupBy(x=>x.Account.CostCenter).Select(y => new CostCenterLookUpModel
                        {
                            Id = y.Key.Id,
                            AccountName = y.Key.Name,
                            IsActive = false,
                            Code = y.Key.Code,
                            AccountNameArabic = y.Key.NameArabic,
                            Debit= Math.Abs(y.Sum(y=>y.Debit)),
                            Credit= Math.Abs(y.Sum(y=>y.Credit)),
                            Total = Convert.ToDecimal(string.Format("{0:0.##}", (Math.Abs(y.Sum(y => y.Debit)) - Math.Abs(y.Sum(y => y.Credit))))),
                            TrialBalanceModel =y.GroupBy(x=>x.Account).Select(z=> new TrialBalanceModel
                            {
                                Id = z.Key.Id,
                                Code=z.Key.Code,
                                AccountName=z.Key.Name,
                                IsActive = false,
                                AccountNameArabic = z.Key.NameArabic,
                                Debit= Math.Abs(z.Sum(z => z.Debit)),
                                Credit = Math.Abs(z.Sum(z => z.Credit)),
                                Total = Convert.ToDecimal(string.Format("{0:0.##}", (Math.Abs(z.Sum(z => z.Debit)) - Math.Abs(z.Sum(z => z.Credit)))))
                            }).OrderBy(z=>z.AccountName).ToList()



                        }).OrderBy(y => y.AccountName).ToList()

                    }).OrderBy(x => x.Name).ToList();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        inventoryList = inventoryList.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
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
