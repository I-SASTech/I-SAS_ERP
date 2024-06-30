using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceQuantityWiseInventoryItemReport.Models;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Enum;

namespace Focus.Business.Reports.AdvanceQuantityWiseInventoryItemReport.Queries
{
    public class GetAdvanceQuantityWiseInventoryItem : IRequest<List<AdvanceQuantityWiseInventoryItemLookupModel>>
    {
        public Guid ProductId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceQuantityWiseInventoryItem, List<AdvanceQuantityWiseInventoryItemLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetAdvanceQuantityWiseInventoryItem> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<List<AdvanceQuantityWiseInventoryItemLookupModel>> Handle(GetAdvanceQuantityWiseInventoryItem request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select pr.Code as ProductCode, pr.EnglishName as ProductName, pr.DisplayName as DisplayName,lt.TransactionType, pr.ArabicName as ProductArabicName, " +
                                "a.Code as AccountCode, a.Name as AccountName, sum(lt.Debit - lt.Credit) as Amount, lt.Date from Products pr " +
                                "left outer join LedgerTransactions lt on pr.Id = lt.ProductId " +
                                "left outer join LedgerAccounts ld on lt.AccountId = ld.Id " +
                                "left outer join Accounts a on a.Id = ld.AccountId " +
                                "where pr.Id = '"+request.ProductId+"' and (lt.TransactionType = 0 or lt.TransactionType = 9) or (a.Code = '42000001' and a.Code = '11100001' and a.Code = '10000101') and lt.CompanyId = '"+ _contextProvider.GetCompanyId() +"' " +
                                "group by pr.Code,pr.EnglishName,pr.ArabicName,a.Code, a.Name,lt.TransactionType,lt.Date, pr.DisplayName";

                    var query = conn.Query<AdvanceQuantityWiseInventoryItemLookupModel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ProductId);

                    var inventory = await _context.Inventories.AsNoTracking().Where(x => x.ProductId == request.ProductId).ToListAsync();

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        var list = new List<AdvanceQuantityWiseInventoryItemLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                            var inven = inventory.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = product.Code,
                                        ProductName = string.IsNullOrEmpty(product.DisplayName) ?  product.EnglishName : product.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        PurchaseBalance = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0).Sum(x => x.Amount),
                                        PurchaseQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.PurchasePost).Sum(z => z.Quantity) : 0,
                                        InventoryTotal = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9).Sum(x => x.Amount),
                                        InventoryQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        SaleTotal = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9).Sum(x => x.Amount),
                                        SaleQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        CostOfGoodsSoldTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Price * z.Quantity) : 0,
                                        CostOfGoodsSoldQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Quantity) : 0,
                                        StockOutTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Price * z.Quantity) : 0,
                                        StockOutQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Quantity) : 0,
                                        OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                        ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                    });
                                }   
                            }
                            else
                            {
                                list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductArabicName = product.ArabicName,
                                    ProductName = product.EnglishName,
                                    OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                    ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        var list = new List<AdvanceQuantityWiseInventoryItemLookupModel>();

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                            var inven = inventory.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = product.Code,
                                        ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        PurchaseBalance = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0).Sum(x => x.Amount),
                                        PurchaseQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.PurchasePost).Sum(z => z.Quantity) : 0,
                                        InventoryTotal = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9).Sum(x => x.Amount),
                                        InventoryQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        SaleTotal = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9).Sum(x => x.Amount),
                                        SaleQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        CostOfGoodsSoldTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Price * z.Quantity) : 0,
                                        CostOfGoodsSoldQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Quantity) : 0,
                                        StockOutTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Price * z.Quantity) : 0,
                                        StockOutQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Quantity) : 0,
                                        OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                        ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                    });
                                }
                            }
                            else
                            {
                                list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductArabicName = product.ArabicName,
                                    ProductName = product.EnglishName,
                                    OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                    ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        var list = new List<AdvanceQuantityWiseInventoryItemLookupModel>();

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

                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                            var inven = inventory.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = product.Code,
                                        ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        PurchaseBalance = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0).Sum(x => x.Amount),
                                        PurchaseQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.PurchasePost).Sum(z => z.Quantity) : 0,
                                        InventoryTotal = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9).Sum(x => x.Amount),
                                        InventoryQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        SaleTotal = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9).Sum(x => x.Amount),
                                        SaleQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        CostOfGoodsSoldTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Price * z.Quantity) : 0,
                                        CostOfGoodsSoldQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Quantity) : 0,
                                        StockOutTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Price * z.Quantity) : 0,
                                        StockOutQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Quantity) : 0,
                                        OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                        ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),

                                    });
                                }
                            }
                            else
                            {
                                list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductArabicName = product.ArabicName,
                                    ProductName = product.EnglishName,
                                    OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                    ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        var list = new List<AdvanceQuantityWiseInventoryItemLookupModel>();
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));
                            var query1 = query.Where(x => x.Date.Date >= fromDate.Date && x.Date <= toDate.Date);
                            var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                            var inven = inventory.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            if (inventoryItemList.Count > 0)
                            {
                                foreach (var x in inventoryItemList)
                                {
                                    list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                    {
                                        CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                        ProductCode = product.Code,
                                        ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                        ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                        AccounCode = x.FirstOrDefault()?.AccounCode,
                                        AccountName = x.FirstOrDefault()?.AccountName,
                                        PurchaseBalance = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0).Sum(x => x.Amount),
                                        PurchaseQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.PurchasePost).Sum(z => z.Quantity) : 0,
                                        InventoryTotal = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9).Sum(x => x.Amount),
                                        InventoryQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        SaleTotal = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9).Sum(x => x.Amount),
                                        SaleQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                        CostOfGoodsSoldTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Price * z.Quantity) : 0,
                                        CostOfGoodsSoldQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Quantity) : 0,
                                        StockOutTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Price * z.Quantity) : 0,
                                        StockOutQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Quantity) : 0,
                                        OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                        ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                    });
                                }
                            }
                            else
                            {
                                list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = product.Code,
                                    ProductArabicName = product.ArabicName,
                                    ProductName = product.EnglishName,
                                    OpeningBalance = query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < fromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                    ClosingBalance = query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 0).Sum(y => y.Amount) + query.Where(y => y.Date.Date >= fromDate.Date && y.Date.Date <= toDate.Date && y.AccountName == "Inventory" && y.TransactionType == 9).Sum( y => y.Amount),
                                });
                            }
                        }
                        return list;
                    }
                    else
                    {
                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        var inventoryItemList = query1.GroupBy(x => x.ProductCode).ToList();
                        var inven = inventory.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).ToList();

                        if(inventoryItemList.Count > 0)
                        {
                            var inventoryList = inventoryItemList.Select(x => new AdvanceQuantityWiseInventoryItemLookupModel
                            {
                                ProductCode = product.Code,
                                ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                AccounCode = x.FirstOrDefault()?.AccounCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                PurchaseBalance = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 0).Sum(x => x.Amount),
                                PurchaseQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.PurchasePost).Sum(z => z.Quantity) : 0,
                                InventoryTotal = x.Where(y => y.AccountName == "Inventory" && y.TransactionType == 9).Sum(x => x.Amount),
                                InventoryQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                SaleTotal = x.Where(y => y.AccountName == "Sale" && y.TransactionType == 9).Sum(x => x.Amount),
                                SaleQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.SaleInvoice).Sum(z => z.Quantity) : 0,
                                CostOfGoodsSoldTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Price * z.Quantity) : 0,
                                CostOfGoodsSoldQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockIn).Sum(z => z.Quantity) : 0, 
                                StockOutTotal = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Price * z.Quantity) : 0,
                                StockOutQuantity = inven.Count > 0 ? inven.Where(z => z.TransactionType == TransactionType.StockOut).Sum(z => z.Quantity) : 0,
                                OpeningBalance = query.Where(y => y.Date.Date < request.FromDate.Date && y.AccountName == "Inventory" ) != null ? query.Where(y => y.Date.Date < request.FromDate.Date &&  y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                ClosingBalance = query.Where(y => y.Date.Date >= request.FromDate.Date &&  y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date >= request.FromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                            }).ToList();

                            return inventoryList;
                        }
                        else
                        {
                            var list = new List<AdvanceQuantityWiseInventoryItemLookupModel>();

                            list.Add(new AdvanceQuantityWiseInventoryItemLookupModel()
                            {
                                ProductCode = product.Code,
                                ProductName = string.IsNullOrEmpty(product.DisplayName) ? product.EnglishName : product.DisplayName,
                                ProductArabicName = string.IsNullOrEmpty(product.DisplayName) ? product.ArabicName : product.DisplayName,
                                OpeningBalance = query.Where(y => y.Date.Date < request.FromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date < request.FromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                                ClosingBalance = query.Where(y => y.Date.Date >= request.FromDate.Date && y.AccountName == "Inventory") != null ? query.Where(y => y.Date.Date >= request.FromDate.Date && y.AccountName == "Inventory").Sum(y => y.Amount) : 0,
                            });
                            return list;
                        }
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
