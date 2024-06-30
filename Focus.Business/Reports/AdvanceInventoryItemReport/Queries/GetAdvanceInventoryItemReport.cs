using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceInventoryItemReport.Models;
using Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Models;
using Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Queries;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Reports.AdvanceInventoryItemReport.Queries
{
    public class GetAdvanceInventoryItemReport : IRequest<List<AdvanceInventoryItemLookupMdoel>>
    {
        public Guid ProductId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceInventoryItemReport, List<AdvanceInventoryItemLookupMdoel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetAdvanceInventoryItemReport> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<List<AdvanceInventoryItemLookupMdoel>> Handle(GetAdvanceInventoryItemReport request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select pr.Code as ProductCode, pr.EnglishName as ProductName,lt.TransactionType, " +
                                "pr.ArabicName as ProductArabicName,a.Code as AccountCode, a.Name as AccountName,lt.Date,pr.DisplayName as DisplayName, " +
                                "sum(lt.Debit - lt.Credit) as Amount from Products pr " +
                                "left outer join LedgerTransactions lt  on pr.Id = lt.ProductId " +
                                "left outer join LedgerAccounts ld on lt.AccountId = ld.Id " +
                                "left outer join Accounts a on a.Id = ld.AccountId " +
                                "where pr.Id = '" + request.ProductId + "'  and (lt.TransactionType = 9 or lt.TransactionType = 0) " +
                                "or (a.Code = '42000001' and a.Code = '11100001' and a.Code = '10000101') and lt.CompanyId = '" + _contextProvider.GetCompanyId() + "' " +
                                "group by pr.Code,pr.EnglishName,pr.ArabicName,a.Code, a.Name,lt.TransactionType,lt.Date,pr.DisplayName";

                    var query = conn.Query<AdvanceInventoryItemLookupMdoel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }

                    var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ProductId);
                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        var list = new List<AdvanceInventoryItemLookupMdoel>();

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                            if(inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceInventoryItemLookupMdoel
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = x.Key,
                                        ProductName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ?  x.FirstOrDefault()?.ProductName : x.FirstOrDefault()?.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductArabicName : x.FirstOrDefault()?.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    });
                                }
                            }
                            
                            else
                            {
                                list.Add(new AdvanceInventoryItemLookupMdoel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                    ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                    AccounCode = "",
                                    AccountName = "",
                                    OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                });
                            }

                        }
                        return list;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        var list = new List<AdvanceInventoryItemLookupMdoel>();

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceInventoryItemLookupMdoel
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = x.Key,
                                        ProductName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductName : x.FirstOrDefault()?.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductArabicName : x.FirstOrDefault()?.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    });
                                }
                            }

                            else
                            {
                                list.Add(new AdvanceInventoryItemLookupMdoel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                    ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                    AccounCode = "",
                                    AccountName = "",
                                    OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        var list = new List<AdvanceInventoryItemLookupMdoel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

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

                            DateTime fromDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime toDate = fromDate.AddMonths(3).AddDays(-1);

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceInventoryItemLookupMdoel
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = x.Key,
                                        ProductName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductName : x.FirstOrDefault()?.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductArabicName : x.FirstOrDefault()?.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    });
                                }
                            }

                            else
                            {
                                list.Add(new AdvanceInventoryItemLookupMdoel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                    ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                    AccounCode = "",
                                    AccountName = "",
                                    OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                });
                            }



                        }

                        return list;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        var list = new List<AdvanceInventoryItemLookupMdoel>();
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));
                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceInventoryItemLookupMdoel
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = x.Key,
                                        ProductName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductName : x.FirstOrDefault()?.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductArabicName : x.FirstOrDefault()?.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                        CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                        ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    });
                                }
                            }

                            else
                            {
                                list.Add(new AdvanceInventoryItemLookupMdoel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                    ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                    AccounCode = "",
                                    AccountName = "",
                                    OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < fromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < fromDate.Date).Sum(x => x.Amount),
                                    CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                    ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date).Sum(x => x.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else
                    {
                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                        var list = new List<AdvanceInventoryItemLookupMdoel>();

                        if (inventoryItemList.Count > 0)
                        {
                            var inventoryList = inventoryItemList.Select(x => new AdvanceInventoryItemLookupMdoel
                            {
                                ProductCode = x.Key,
                                ProductName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductName : x.FirstOrDefault()?.DisplayName,
                                ProductArabicName = string.IsNullOrEmpty(x.FirstOrDefault()?.DisplayName) ? x.FirstOrDefault()?.ProductArabicName : x.FirstOrDefault()?.DisplayName,
                                AccounCode = x.FirstOrDefault()?.AccounCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                OpeningPurchase = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentPurchase = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingPurchase = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningSale = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentSale = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingSale = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningCOGS = x.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentCOGS = x.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingCOGS = x.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningInventory = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount) + x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentInventory = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount) + x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingInventory = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount) + x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                            }).ToList();

                            return inventoryList;
                        }
                        else
                        {
                            list.Add(new AdvanceInventoryItemLookupMdoel()
                            {
                                ProductCode = product.Code,
                                ProductName = string.IsNullOrEmpty(product.DisplayName) ?  product.EnglishName : product.DisplayName,
                                ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                AccounCode = "",
                                AccountName = "",
                                OpeningPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingPurchase = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingSale = query.Where(y => y.AccountName == "Sale" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingCOGS = query.Where(y => y.AccountName == "Cost of Goods Sold" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                                OpeningInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date < request.FromDate.Date).Sum(x => x.Amount),
                                CurrentInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date >= request.FromDate.Date && y.Date.Date <= request.ToDate.Date).Sum(x => x.Amount),
                                ClosingInventory = query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount) + query.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9 && y.Date.Date > request.ToDate.Date).Sum(x => x.Amount),
                            });
                        }
                        return list;
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
