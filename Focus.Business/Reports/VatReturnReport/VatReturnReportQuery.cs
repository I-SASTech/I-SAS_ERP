using Focus.Business.Interface;
using Focus.Business.Reports.VatSalePurchaseComparisonReport;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Reports.VatReturnReport
{
    public class VatReturnReportQuery : IRequest<VatReturnLookUpModel>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DocumentName { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<VatReturnReportQuery, VatReturnLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<VatReturnReportQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<VatReturnLookUpModel> Handle(VatReturnReportQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    var vatReturnLookUpModel = new VatReturnLookUpModel();
                    //Purchase Post

                    var purchaseSummary = await _context.PurchasePosts
                        .AsNoTracking()
                        .Where(x => x.Date.Date >= request.FromDate.Date &&
                                    x.Date.Date <= request.ToDate.Date &&
                                    x.IsPurchasePost)
                        .GroupBy(x => 1) // Grouping by a constant value to aggregate the entire set
                        .Select(group => new VatSalePurchaseModel
                        {
                            TotalPurchase = group.Sum(x => x.TotalAmount),
                            VatAmountOut = group.Sum(x => x.VatAmount),
                            GrossPurchase = group.Sum(x => x.TotalWithOutAmount)
                        })
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);


                    //Purchase Return


                    var purchaseReturnMonthWiseSum = await _context.PurchasePosts.AsNoTracking().Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.IsPurchaseReturn)
                        .GroupBy(x => 1) // Grouping by a constant value to aggregate the entire set
                        .Select(group => new VatSalePurchaseModel()
                        {
                            TotalPurchase = group.Sum(x => x.TotalAmount),
                            VatAmountOut = group.Sum(x => x.VatAmount),
                            GrossPurchase = group.Sum(x => x.TotalWithOutAmount)
                        })
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    var dailyExpenses = await _context.DailyExpenses
                        .AsNoTracking()
                        .Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.ApprovalStatus == ApprovalStatus.Approved)
                        .GroupBy(x => 1) // Grouping by a constant value to aggregate the entire set
                        .Select(group => new VatSalePurchaseModel()
                        {
                            TotalPurchase = group.Sum(x => x.TotalAmount),
                            VatAmountOut = group.Sum(x => x.TotalVat),
                            GrossPurchase = group.Sum(x => x.GrossAmount)
                        })
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);





                    if (purchaseSummary != null)
                    { //For Purchase
                        vatReturnLookUpModel.TotalPurchase += purchaseSummary.TotalPurchase;
                        vatReturnLookUpModel.TotalPurchaseVat += purchaseSummary.VatAmountOut;
                        vatReturnLookUpModel.TotalGrossPurchase += purchaseSummary.GrossPurchase;
                    }
                    if (dailyExpenses != null)
                    {//For Daily Expense
                        vatReturnLookUpModel.TotalPurchase += dailyExpenses.TotalPurchase;
                        vatReturnLookUpModel.TotalPurchaseVat += dailyExpenses.VatAmountOut;
                        vatReturnLookUpModel.TotalGrossPurchase += dailyExpenses.GrossPurchase;
                    }
                    if (purchaseReturnMonthWiseSum != null)
                    {//For Purchase Return
                        vatReturnLookUpModel.TotalPurchase -= purchaseReturnMonthWiseSum.TotalPurchase;
                        vatReturnLookUpModel.TotalPurchaseVat -= purchaseReturnMonthWiseSum.VatAmountOut;
                        vatReturnLookUpModel.TotalGrossPurchase -= purchaseReturnMonthWiseSum.GrossPurchase;
                    }
                  



                    var salesMonthWiseSum = await _context.Sales
                        .AsNoTracking()
                        .Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            (x.InvoiceType == Domain.Entities.InvoiceType.Paid || x.InvoiceType == Domain.Entities.InvoiceType.Credit))
                        .GroupBy(x => 1) // Grouping by a constant value to aggregate the entire set
                        .Select(group => new VatSalePurchaseModel()
                        {
                            Date = group.FirstOrDefault().Date,
                            TotalSales = group.Sum(x => x.TotalAmount),
                            VatAmountIn = group.Sum(x => x.VatAmount),
                            GrossSales = group.Sum(x => x.TotalWithOutAmount)
                        })
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);


                    var saleReturnMonthWiseSum = await _context.Sales.AsNoTracking().Where(x =>
                            x.Date.Date >= request.FromDate.Date &&
                            x.Date.Date <= request.ToDate.Date &&
                            x.IsSaleReturnPost)
                        .GroupBy(x => 1) // Grouping by a constant value to aggregate the entire set
                        .Select(group => new VatSalePurchaseModel()
                        {
                            TotalSales = group.Sum(x => x.TotalAmount),
                            VatAmountIn = group.Sum(x => x.VatAmount),
                            GrossSales = group.Sum(x => x.TotalWithOutAmount)
                        })
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);


                    if (salesMonthWiseSum != null)
                    {//For Sale
                        vatReturnLookUpModel.TotalSale += salesMonthWiseSum.TotalSales;
                        vatReturnLookUpModel.TotalSaleVat += salesMonthWiseSum.VatAmountIn;
                        vatReturnLookUpModel.TotalGrossSale += salesMonthWiseSum.GrossSales;
                    }
                    if (saleReturnMonthWiseSum != null)
                    {//For Sale
                        vatReturnLookUpModel.TotalSale -= salesMonthWiseSum.TotalSales;
                        vatReturnLookUpModel.TotalSaleVat -= salesMonthWiseSum.VatAmountIn;
                        vatReturnLookUpModel.TotalGrossSale -= salesMonthWiseSum.GrossSales;
                    }








                    return vatReturnLookUpModel;
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