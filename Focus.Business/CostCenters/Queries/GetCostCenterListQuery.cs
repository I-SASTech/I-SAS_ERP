using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.CostCenters.Models;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.CostCenters.Queries
{
    public class GetCostCenterListQuery : IRequest<CostCenterLookupModel>
    {
        public bool IsList { get; set; }
        public string BranchId { get; set; }
        public class Handler : IRequestHandler<GetCostCenterListQuery, CostCenterLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCostCenterListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<CostCenterLookupModel> Handle(GetCostCenterListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var costCenters = await _context.CostCenters.AsNoTracking().Include(x => x.AccountTypes).ToListAsync();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        costCenters = costCenters.Where(x => branchIdList.Contains(request.BranchId)).ToList();
                    }

                    if (request.IsList)
                    {
                        var allCostCenters = costCenters.OrderBy(x => x.Code).Select(x => new AllCostCenterLookupModel()
                        {
                            CostCenterName = x.Name,
                            Code = x.Code,
                        }).ToList();

                        return new CostCenterLookupModel
                        {
                            CostCenters = allCostCenters
                        };
                    }
                    else
                    {
                        var assets = new List<AssetLookupModel>();
                        var liabilities = new List<LiabilitiesLookupModel>();
                        var equities = new List<EquityLookupModel>();
                        var incomes = new List<IncomeLookupModel>();
                        var expenses = new List<ExpensesLookupModel>();

                        foreach (var item in costCenters)
                        {
                            if (item.AccountTypes.Name == "Assets")
                            {
                                assets.Add(new AssetLookupModel()
                                {
                                    Code = item.Code,
                                    CostCenterName = item.Name,
                                });
                            }
                            else if (item.AccountTypes.Name == "Liabilities")
                            {
                                liabilities.Add(new LiabilitiesLookupModel()
                                {
                                    Code = item.Code,
                                    CostCenterName = item.Name,
                                });
                            }
                            else if (item.AccountTypes.Name == "Equity")
                            {
                                equities.Add(new EquityLookupModel()
                                {
                                    Code = item.Code,
                                    CostCenterName = item.Name,
                                });
                            }
                            else if (item.AccountTypes.Name == "Income")
                            {
                                incomes.Add(new IncomeLookupModel()
                                {
                                    Code = item.Code,
                                    CostCenterName = item.Name,
                                });
                            }
                            else if (item.AccountTypes.Name == "Expenses")
                            {
                                expenses.Add(new ExpensesLookupModel()
                                {
                                    Code = item.Code,
                                    CostCenterName = item.Name,
                                });
                            }
                        }
                        

                        return new CostCenterLookupModel
                        {
                            Assets = assets,
                            Liabilities = liabilities,
                            Equities = equities,
                            Incomes = incomes,
                            Expenses = expenses
                        };
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
