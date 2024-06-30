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
using Focus.Domain.Entities;

namespace Focus.Business.Reports.MonthlySalesReport
{
    public class GetMonthlySaleQuery : IRequest<List<MonthlySaleModel>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public class Handler : IRequestHandler<GetMonthlySaleQuery, List<MonthlySaleModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetMonthlySaleQuery> logger
                 )
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<MonthlySaleModel>> Handle(GetMonthlySaleQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var sales = _context.Sales.AsNoTracking()
                        .Include(x => x.SaleInvoiceAdvancePayments)
                        .Include(x => x.SalePayments)
                          .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date.Month == request.Month && x.Date.Date.Year == request.Year && x.InvoiceType != 0)
                        .ToList();
                   


                  
                    var salesByDate = sales.GroupBy(x => x.Date.Date.Date);

                    var monthlySale = new List<MonthlySaleModel>();
                    if (request.Year != 0)
                    {

                        int days = DateTime.DaysInMonth(request.Year, request.Month);



                        var inventoryList = salesByDate.Select(x => new MonthlySaleModel
                        {

                            Date = x.Key.ToString("dd/MM/yyyy"),
                            TaxMethod = x.FirstOrDefault().TaxMethod,
                            GrossAmount = x.Where(x =>  x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice)) - x.Where(x => x.SaleInvoiceId == null ).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                   ? ((item.Quantity * item.UnitPrice) -
                                      ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                       (item.OfferQuantity == 0
                                           ?
                                           (item.Discount == 0
                                               ? item.Quantity * item.FixDiscount
                                               : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                           : item.Discount == 0
                                               ? item.OfferQuantity * item.FixDiscount
                                               : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                   item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                   : 0)),
                            Cash = x.Sum(y => y.SalePayments.Where(x => x.PaymentType == SalePaymentType.Cash).Sum(z => z.DueAmount)),
                            Bank = x.Sum(y => y.SalePayments.Where(x => x.PaymentType == SalePaymentType.Bank || x.PaymentType == SalePaymentType.OtherCurrency).Sum(z => z.DueAmount)),
                            Credit = (x.Where(x => x.IsCredit && x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice))- x.Where(x => x.SaleInvoiceId == null && x.IsCredit).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                     (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                     : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                         : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) - x.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                       (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                           : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                       : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                           : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))))+
                                           x.Where(x => x.SaleInvoiceId == null && x.IsCredit).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                  ? 0
                                  : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                      ?
                                      (item.Discount == 0
                                          ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      : item.Discount == 0
                                          ? item.OfferQuantity * item.FixDiscount
                                          : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                  item.TaxRate.Rate / 100)),

                            Discount = x.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                    (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                        : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                        : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) - x.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                      (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                          : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))),
                            TotalReturns = x.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice)) - x.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                 ? ((item.Quantity * item.UnitPrice) -
                                    ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                     (item.OfferQuantity == 0
                                         ?
                                         (item.Discount == 0
                                             ? item.Quantity * item.FixDiscount
                                             : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                         : item.Discount == 0
                                             ? item.OfferQuantity * item.FixDiscount
                                             : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                 item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                 : 0)),
                            TotalTax = x.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                  ? ((item.Quantity * item.UnitPrice) -
                                     ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                      (item.OfferQuantity == 0
                                          ?
                                          (item.Discount == 0
                                              ? item.Quantity * item.FixDiscount
                                              : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                          : item.Discount == 0
                                              ? item.OfferQuantity * item.FixDiscount
                                              : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                  item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                  : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                      ?
                                      (item.Discount == 0
                                          ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      : item.Discount == 0
                                          ? item.OfferQuantity * item.FixDiscount
                                          : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                  item.TaxRate.Rate / 100)) - x.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                    ? ((item.Quantity * item.UnitPrice) -
                                       ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                        (item.OfferQuantity == 0
                                            ?
                                            (item.Discount == 0
                                                ? item.Quantity * item.FixDiscount
                                                : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                            : item.Discount == 0
                                                ? item.OfferQuantity * item.FixDiscount
                                                : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                    item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                    : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                        ?
                                        (item.Discount == 0
                                            ? item.Quantity * item.FixDiscount
                                            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                        : item.Discount == 0
                                            ? item.OfferQuantity * item.FixDiscount
                                            : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                    item.TaxRate.Rate / 100)),





                        }).ToList();


                        foreach (var x in inventoryList)
                        {
                            monthlySale.Add(new MonthlySaleModel()
                            {

                                Date = x.Date,
                                GrossAmount = x.GrossAmount,
                                Cash = x.Cash ,
                                Bank = x.Bank ,
                                Credit = x.Credit,
                                Discount = x.Discount,
                                TotalReturns = x.TotalReturns < 0 ? x.TotalReturns * -1 : x.TotalReturns,
                                TotalTax = x.TotalTax,
                                TaxMethod = x.TaxMethod


                            });
                        }

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
