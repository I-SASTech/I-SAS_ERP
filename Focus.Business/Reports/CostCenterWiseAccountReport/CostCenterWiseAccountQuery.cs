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

namespace Focus.Business.Reports.CostCenterWiseAccountReport
{
   public class CostCenterWiseAccountQuery : IRequest<List<CostCenterLookUpModel>>
    {
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<CostCenterWiseAccountQuery, List<CostCenterLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public Handler(IApplicationDbContext context, ILogger<CostCenterWiseAccountQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<CostCenterLookUpModel>> Handle(CostCenterWiseAccountQuery request, CancellationToken cancellationToken)
            {
                try
                {




                    var listTrialBalance = new List<CostCenterLookUpModel>();
                    var query = _context.CostCenters
                            .AsNoTracking()
                              .Include(x => x.Accounts)
                            .ToList();

                    var inventoryList = query.Select(x => new CostCenterLookUpModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                        IsActive = x.IsActive,
                        NameArabic = x.NameArabic,
                        TrialBalanceModel = x.Accounts.Select(z => new TrialBalanceModel
                        {
                            Id = z.Id,
                            Code = z.Code,
                            Name = z.Name,
                            check = false,
                            Date=System.DateTime.UtcNow,
                            AccountNameArabic = z.NameArabic,
                        }).OrderBy(z => z.AccountName).ToList()

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
