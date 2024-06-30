using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Reports.ComparisionReport
{
    public class GetMonthlyComparisionReportQuery : IRequest<List<MonthlyComparisionReportModel>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetMonthlyComparisionReportQuery, List<MonthlyComparisionReportModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetMonthlyComparisionReportQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<MonthlyComparisionReportModel>> Handle(GetMonthlyComparisionReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchases = await _context.PurchasePosts.AsNoTracking()
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date.Month == request.Month && x.Date.Date.Year == request.Year && (x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn)))
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        purchases = purchases.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    var purchasesByDate = purchases.GroupBy(x => x.Date.Date.Date);
                    var monthlySale = new List<MonthlyComparisionReportModel>();

                    var inventoryPurchaseList = purchasesByDate.Select(x => new MonthlyComparisionReportModel
                    {

                        Date = x.Key,
                        GrossPurchaseAmount = x.Sum(p => p.PurchaseInvoiceId == null ? p.TotalWithOutAmount : 0),
                        PurchaseNetSale = x.Sum(p => p.PurchaseInvoiceId == null ? p.TotalAmount : 0),
                        Credit = x.Sum(p => p.IsCredit && p.PurchaseInvoiceId == null ? p.TotalWithOutAmount : 0),
                        Cash = x.Sum(p => p.PaymentType == "Cash" && p.PurchaseInvoiceId == null ? p.TotalWithOutAmount : 0),
                        Bank = x.Sum(p => p.PaymentType == "Bank" && p.PurchaseInvoiceId == null ? p.TotalWithOutAmount : 0),
                        Discount = x.Sum(p => p.PurchaseInvoiceId == null ? p.DiscountAmount : 0),
                        TotalTax = x.Sum(p => p.PurchaseInvoiceId == null ? p.VatAmount : 0),
                        TotalTaxReturn = x.Sum(p => p.PurchaseInvoiceId != null ? p.VatAmount : 0),
                        TotalReturns = x.Sum(p => p.PurchaseInvoiceId != null ? p.TotalWithOutAmount : 0),
                        Purchase = true
                    }).ToList();



                    var sales = await _context.Sales.AsNoTracking()
                        .Include(x => x.SaleInvoiceAdvancePayments)
                        .Include(x => x.SalePayments)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date.Month == request.Month && x.Date.Date.Year == request.Year && x.InvoiceType != 0)
                        .ToListAsync(cancellationToken: cancellationToken);

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        sales = sales.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    var salesByDate = sales.GroupBy(x => x.Date.Date.Date);

                    var inventoryList = salesByDate.Select(x => new MonthlyComparisionReportModel
                    {
                        Date = x.Key,
                        GrossAmount = x.Sum(y => y.SaleInvoiceId == null ? y.TotalWithOutAmount : 0),
                        NetSale = x.Sum(y => y.SaleInvoiceId == null ? y.TotalAmount : 0),
                        Cash = x.Sum(y => y.SalePayments.Where(z => z.PaymentType == SalePaymentType.Cash).Sum(z => z.DueAmount)),
                        Bank = x.Sum(y => y.SalePayments.Where(z => z.PaymentType == SalePaymentType.Bank || z.PaymentType == SalePaymentType.OtherCurrency).Sum(z => z.DueAmount)),
                        Credit = x.Sum(y => y.IsCredit && y.SaleInvoiceId == null ? y.TotalWithOutAmount : 0),
                        Discount = x.Sum(y => y.SaleInvoiceId == null ? y.DiscountAmount : 0),
                        TotalReturns = x.Sum(y => y.SaleInvoiceId != null ? y.TotalWithOutAmount : 0),
                        TotalTax = x.Sum(y => y.SaleInvoiceId == null ? y.VatAmount : 0),
                        Purchase = false
                    }).ToList();

                    var list = (from po in inventoryPurchaseList
                                select new MonthlyComparisionReportModel()
                                {
                                    Date = po.Date,
                                    PurchaseCash = po.Cash,
                                    GrossPurchaseAmount = po.GrossPurchaseAmount,
                                    PurchaseBank = po.Bank,
                                    PurchaseCredit = po.Credit,
                                    PurchaseDiscount = po.Discount,
                                    PurchaseTotalReturns = po.TotalReturns < 0 ? po.TotalReturns * -1 : po.TotalReturns,
                                    PurchaseTotalTax = (po.TotalTax - po.TotalTaxReturn) < 0 ? (po.TotalTax - po.TotalTaxReturn) * -1 : (po.TotalTax - po.TotalTaxReturn),
                                    Purchase = true

                                }
                        ).Union(from sale in inventoryList
                                select new MonthlyComparisionReportModel()
                                {
                                    Date = sale.Date,
                                    Cash = sale.Cash,
                                    Bank = sale.Bank,
                                    GrossAmount = sale.GrossAmount,
                                    Credit = sale.Credit,
                                    Discount = sale.Discount,
                                    TotalReturns = sale.TotalReturns < 0 ? sale.TotalReturns * -1 : sale.TotalReturns,
                                    TotalTax = sale.TotalTax,
                                    Purchase = false
                                }).OrderBy(x => x.Date);



                    var list2 = new MonthlyComparisionReportModel();
                    foreach (var y in list)
                    {
                        if (list2.Date == y.Date)
                        {
                            if (list2.Purchase)
                            {
                                var index = monthlySale.FindIndex(x => x.Date == y.Date);
                                monthlySale.RemoveAt(index);
                                monthlySale.Add(new MonthlyComparisionReportModel()
                                {

                                    Date = y.Date,
                                    Cash = y.Cash,
                                    GrossAmount = y.GrossAmount,
                                    Bank = y.Bank,
                                    Credit = y.Credit,
                                    Discount = y.Discount,
                                    TotalReturns = y.TotalReturns,
                                    TotalTax = y.TotalTax,
                                    PurchaseDate = y.Date,
                                    PurchaseCash = list2.PurchaseCash,
                                    GrossPurchaseAmount = list2.GrossPurchaseAmount,
                                    PurchaseBank = list2.PurchaseBank,
                                    PurchaseCredit = list2.PurchaseCredit,
                                    PurchaseDiscount = list2.PurchaseDiscount,
                                    PurchaseTotalReturns = list2.PurchaseTotalReturns,
                                    PurchaseTotalTax = list2.PurchaseTotalTax,
                                });
                            }
                            else
                            {
                                var index = monthlySale.FindIndex(x => x.Date == y.Date);
                                monthlySale.RemoveAt(index);

                                monthlySale.Add(new MonthlyComparisionReportModel()
                                {

                                    GrossAmount = list2.GrossAmount,
                                    Date = list2.Date,
                                    Cash = list2.Cash,
                                    Bank = list2.Bank,
                                    Credit = list2.Credit,
                                    Discount = list2.Discount,
                                    TotalReturns = list2.TotalReturns,
                                    TotalTax = list2.TotalTax,
                                    PurchaseDate = y.Date,
                                    GrossPurchaseAmount = y.GrossPurchaseAmount,
                                    PurchaseCash = y.PurchaseCash,
                                    PurchaseBank = y.PurchaseBank,
                                    PurchaseCredit = y.PurchaseCredit,
                                    PurchaseDiscount = y.PurchaseDiscount,
                                    PurchaseTotalReturns = y.PurchaseTotalReturns,
                                    PurchaseTotalTax = y.PurchaseTotalTax,
                                });
                            }
                        }
                        else
                        {
                            monthlySale.Add(new MonthlyComparisionReportModel()
                            {
                                GrossAmount = y.GrossAmount,
                                Date = y.Date,
                                Cash = y.Cash,
                                Bank = y.Bank,
                                Credit = y.Credit,
                                Discount = y.Discount,
                                TotalReturns = y.TotalReturns,
                                TotalTax = y.TotalTax,
                                PurchaseDate = y.Date,
                                GrossPurchaseAmount = y.GrossPurchaseAmount,
                                PurchaseCash = y.PurchaseCash,
                                PurchaseBank = y.PurchaseBank,
                                PurchaseCredit = y.PurchaseCredit,
                                PurchaseDiscount = y.PurchaseDiscount,
                                PurchaseTotalReturns = y.PurchaseTotalReturns,
                                PurchaseTotalTax = y.PurchaseTotalTax,
                            });
                        }
                        list2 = y;
                    }
                    return monthlySale;
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
