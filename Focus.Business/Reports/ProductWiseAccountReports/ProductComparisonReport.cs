using Focus.Business.Interface;
using Focus.Business.Reports.ProductLedgerReport;
using Focus.Business.Reports.PurchaseInvoiceReport;
using Focus.Business.Reports.SaleInvoiceReport;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductWiseAccountReports
{
    public class ProductComparisonReport : IRequest<List<ProductComparisonLookUpModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchTerm { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<ProductComparisonReport, List<ProductComparisonLookUpModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public Handler(IApplicationDbContext context,ILogger<GetPurchaseInvoiceQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ProductComparisonLookUpModel>> Handle(ProductComparisonReport request, CancellationToken cancellationToken)
            {
                try
                {
                    request.ToDate = request.ToDate.AddDays(1);

                    var fromDate = request.FromDate;
                    var toDate = request.ToDate;

                    var accounts = await _context.LedgerAccounts.AsNoTracking().Include(x => x.Account).ToListAsync();
                    var iAccountIds = accounts.Where(x=> x.Account.Code == "11100001").Select(x => x.Id);
                    var cogsAccountIds = accounts.Where(x=> x.Account.Code == "60000101").Select(x => x.Id);
                    var sAccountIds = accounts.Where(x=> x.Account.Code == "42000001").Select(x => x.Id);
                    var ledgerTranasction = _context.LedgerTransactions.AsEnumerable();
                    var ledgerTranasction1= ledgerTranasction.OrderBy(x => x.ProductId).GroupBy(x => x.ProductId);

                    //var list = ledgerTranasction.Select(x => new AccountLookUpModel
                    //{
                    //    IOpeningBalance = x.OrderBy(x=> x.Id).Where(x => x.Date < request.FromDate).Sum(y=>y.Debit - y.Credit),
                    //    IClosingingBalance = x.OrderBy(x=> x.Id).Where(x => x.Date >= request.FromDate && x.Date <= request.ToDate).Sum(y => y.Debit - y.Credit),
                    //});
                    var productsList = await _context.Products.ToListAsync();

                    var productC = new List<ProductComparisonLookUpModel>();

                    var closingBlance = 0;

                    var inventoryList = ledgerTranasction1.Select(x => new ProductComparisonLookUpModel
                    {
                        ProductName = productsList.FirstOrDefault(y => y.Id == x.Key) == null ? "" : string.IsNullOrEmpty(productsList.FirstOrDefault(y => y.Id == x.Key).DisplayNameForPrint) ? productsList.FirstOrDefault(y => y.Id == x.Key).EnglishName : productsList.FirstOrDefault(y => y.Id == x.Key).DisplayNameForPrint,
                        Code = productsList.FirstOrDefault(y => y.Id == x.Key) == null ? "" : productsList.FirstOrDefault(y => y.Id == x.Key).Code,
                        IOpeningBalance = x.Where(x => x.DocumentDate < request.FromDate && iAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        IClosingingBalance = x.Where(x => x.DocumentDate < request.FromDate && iAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit) + x.Where(x => x.DocumentDate >= request.FromDate && x.Date <= request.ToDate && iAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        GSOpeningBalance = x.Where(x => x.DocumentDate < request.FromDate && cogsAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        GSClosingingBalance = x.Where(x => x.DocumentDate < request.FromDate && cogsAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit) + x.Where(x => x.DocumentDate >= request.FromDate && x.Date <= request.ToDate && cogsAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        SOpeningBalance = x.Where(x => x.DocumentDate < request.FromDate && sAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                        SClosingingBalance = x.Where(x => x.DocumentDate < request.FromDate && sAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit) + x.Where(x => x.DocumentDate >= request.FromDate && x.Date <= request.ToDate && sAccountIds.Contains(x.AccountId.Value)).Sum(y => y.Debit - y.Credit),
                      

                    }).ToList();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        inventoryList = inventoryList.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    //foreach (var item in ledgerTranasction)
                    //{   
                    //    var product = productsList.FirstOrDefault(x=> x.Id == item.Key);

                    //    productC.Add(new ProductComparisonLookUpModel
                    //    {
                    //        ProductName = product.EnglishName,
                    //        ProductArabicName = product.ArabicName,
                    //        IAccountType = accounts.FirstOrDefault(x => x.Id == product.InventoryAccountId)?.Name,
                    //        COGSAccountType = accounts.FirstOrDefault(x => x.Id == product.CogsAccountId)?.Name,
                    //        SAccountType = accounts.FirstOrDefault(x => x.Id == product.SaleAccountId)?.Name,
                    //    });
                    //}



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
