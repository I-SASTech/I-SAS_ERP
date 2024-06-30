using DocumentFormat.OpenXml.Office2010.Excel;
using Focus.Business.Interface;
using Focus.Business.Reports.ProductLedgerReport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductComparisionReport
{
    public class ProductComparisionReport : IRequest<List<ComparisionModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<ProductComparisionReport, List<ComparisionModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<ProductComparisionReport> logger,
                IConfiguration configuration)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ComparisionModel>> Handle(ProductComparisionReport request, CancellationToken cancellationToken)
            {
                try
                {
                    request.ToDate = request.ToDate.AddDays(1);
                    var accounts = _context.LedgerAccounts.AsNoTracking().Include(x=>x.Account).ToList();
                    var iAccounts = accounts.Where(x=>x.Account.Code== "11100001").Select(x => x.Id) ;
                    var cogsAccounts = accounts.Where(x => x.Account.Code == "60000101").Select(x => x.Id);
                    var sAccounts = accounts.Where(x => x.Account.Code == "42000001").Select(x => x.Id);

                    var inventories = _context.Inventories.AsNoTracking().Include(x=>x.Stock).AsEnumerable();
                    var productList = _context.Products.AsNoTracking().AsEnumerable();

                    var ledgerTranasction = _context.LedgerTransactions.Where(x => x.DocumentDate >= request.FromDate && x.DocumentDate <= request.ToDate)
                        .AsEnumerable();


                    //if (!string.IsNullOrEmpty(request.BranchId))
                    //{
                    //    var branchIdList = new List<string>(request.BranchId.Split(','));
                    //    ledgerTranasction = ledgerTranasction.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    //}




                    var inventoryWiseLedger = ledgerTranasction.OrderBy(x => x.ProductId).GroupBy(x => x.ProductId);


                    var result = new List<ComparisionModel>();
                    decimal runningBalance = 0;



                    var inventoryList = inventoryWiseLedger.Select(x => new ComparisionModel
                    {
                        Name = productList.FirstOrDefault(y => y.Id == x.Key) == null ? "" : productList.FirstOrDefault(y => y.Id == x.Key).EnglishName,
                        Quantity = inventories.LastOrDefault(y => y.ProductId == x.Key) == null ? 0 : inventories.FirstOrDefault(y => y.ProductId == x.Key).Stock.CurrentQuantity,
                        InventoryBalance = x.Where(n => iAccounts.Contains(n.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        CogsBalance = x.Where(n => cogsAccounts.Contains(n.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        SaleBalance = x.Where(n => sAccounts.Contains(n.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                       

                    }).ToList();

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
