using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Focus.Business.Reports.VatSalePurchaseComparisonReport
{
    public class VatSalePurchaseComparisionReport : IRequest<List<VatSalePurchaseModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string PaymentType { get; set; }
        public string DocumentName { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<VatSalePurchaseComparisionReport, List<VatSalePurchaseModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<VatSalePurchaseComparisionReport> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<VatSalePurchaseModel>> Handle(VatSalePurchaseComparisionReport request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseMonthWiseSum = await _context.PurchasePosts.AsNoTracking().Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.IsPurchasePost).GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            TotalPurchase = group.Sum(x => x.TotalAmount),
                            VatAmountOut = group.Sum(x => x.VatAmount),
                            GrossPurchase = group.Sum(x => x.TotalWithOutAmount)
                        }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync(cancellationToken: cancellationToken);

                    var purchaseReturnMonthWiseSum = await _context.PurchasePosts.AsNoTracking().Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.IsPurchaseReturn)
                        .GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            TotalPurchaseReturn = group.Sum(x => x.TotalAmount),
                            PurchaseReturnVat = group.Sum(x => x.VatAmount),
                            GrossPurchase = group.Sum(x => x.TotalWithOutAmount)
                        }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync(cancellationToken: cancellationToken);

                    var saleReturnMonthWiseSum = await _context.Sales.AsNoTracking().Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.IsSaleReturnPost)
                        .GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            TotalSaleReturn = group.Sum(x => x.TotalAmount),
                            SaleReturnVat = group.Sum(x => x.VatAmount),
                            GrossPurchase = group.Sum(x => x.TotalWithOutAmount)
                        }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync(cancellationToken: cancellationToken);

                    var dailyExpenses = await _context.DailyExpenses
                        .AsNoTracking()
                        .Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.ApprovalStatus == ApprovalStatus.Approved)
                        .GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            TotalExpense = group.Sum(x => x.TotalAmount),
                            TotalVat = group.Sum(x => x.TotalVat),
                            GrossExpense = group.Sum(x => x.GrossAmount)
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var salesMonthWiseSum = await _context.Sales
                        .AsNoTracking()
                        .Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                        .GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            TotalSales = group.Sum(x => x.TotalAmount),
                            VatAmountIn = group.Sum(x => x.VatAmount),
                            GrossSales = group.Sum(x => x.TotalWithOutAmount)
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var otherVouchers = await _context.Transactions.AsNoTracking().Where(x =>
                            (x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date) &&
                            ((x.AccountId == Guid.Parse("0AB10549-8BC1-4C3B-F050-08DB1EF69414") ||
                              x.AccountId == Guid.Parse("A8F13357-94FA-4ABF-DBCB-08DBA223AF6E") ||
                              x.AccountId == Guid.Parse("A49E6A58-6560-4A13-D429-08DB6684714D")) &&
                             x.TransactionType == TransactionType.JournalVoucher)).GroupBy(x => new { x.Date.Year, x.Date.Month })
                        .Select(group => new VatSalePurchaseModel
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Date = group.FirstOrDefault().Date,
                            OtherVocherVat = group.Sum(x => x.Debit - x.Credit),
                            VatAmountIn = 0,
                            GrossSales = 0
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var allData = new List<VatSalePurchaseModel>();
                    allData.AddRange(purchaseMonthWiseSum);
                    allData.AddRange(dailyExpenses);
                    allData.AddRange(salesMonthWiseSum);
                    allData.AddRange(purchaseReturnMonthWiseSum);
                    allData.AddRange(saleReturnMonthWiseSum);
                    allData.AddRange(otherVouchers);

                    var groupedData = allData.GroupBy(item => new { item.Year, item.Month })
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            TotalPurchase = group.Sum(x => x.TotalPurchase),
                            VatAmountOut = group.Sum(x => x.VatAmountOut),
                            TotalExpense = group.Sum(x => x.TotalExpense),
                            TotalVat = group.Sum(x => x.TotalVat),
                            TotalSales = group.Sum(x => x.TotalSales),
                            VatAmountIn = group.Sum(x => x.VatAmountIn),
                            TotalSaleReturn = group.Sum(x => x.TotalSaleReturn),
                            SaleReturnVat = group.Sum(x => x.SaleReturnVat),
                            TotalPurchaseReturn = group.Sum(x => x.TotalPurchaseReturn),
                            PurchaseReturnVat = group.Sum(x => x.PurchaseReturnVat),
                            OtherVocherVat = group.Sum(x => x.OtherVocherVat)
                        }).OrderBy(x => x.Year)
                        .ThenBy(x => x.Month)
                        .ToList();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        groupedData = groupedData.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    return groupedData;
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