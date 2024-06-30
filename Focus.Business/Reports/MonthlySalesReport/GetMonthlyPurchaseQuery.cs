using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Focus.Business.Reports.MonthlySalesReport
{
    public class GetMonthlyPurchaseQuery : IRequest<List<MonthlySaleModel>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public class Handler : IRequestHandler<GetMonthlyPurchaseQuery, List<MonthlySaleModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetMonthlyPurchaseQuery> logger
                 )
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<MonthlySaleModel>> Handle(GetMonthlyPurchaseQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var sales = _context.PurchasePosts.AsNoTracking()
                          .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date.Month == request.Month && x.Date.Date.Year == request.Year && (x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn)))
                        .ToList();




                    var salesByDate = sales.GroupBy(x => x.Date.Date.Date);

                    var monthlySale = new List<MonthlySaleModel>();
                    if (request.Year != 0)
                    {

                        int days = DateTime.DaysInMonth(request.Year, request.Month);



                        var inventoryList = salesByDate.Select(x => new MonthlySaleModel
                        {

                            Date = x.Key.ToString("dd/MM/yyyy"),

                            TaxMethod=x.FirstOrDefault().TaxMethod,
                           
                            GrossAmount = x.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice))-x.Where(x => x.PurchaseInvoiceId == null  && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>

                                  ((item.Quantity * item.UnitPrice) -
                                       (item.Discount == 0
                                              ? item.Quantity * item.FixDiscount
                                              : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                         ) *

                                      item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                      ))
                            ,
                            Credit = (x.Where(x => x.IsCredit && x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice)) -
                            (x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Credit").Sum(z => z.PurchasePostItems.Sum(item =>
                                   (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                       : (item.Quantity * item.UnitPrice * item.Discount) / 100))))) + (
                                  x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Credit" && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                     (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *
                                 item.TaxRate.Rate / 100))),
                            Cash = (x.Where(x => x.PaymentType == "Cash" && x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice))-
                            (x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Cash").Sum(z => z.PurchasePostItems.Sum(item =>
                                   (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                       : (item.Quantity * item.UnitPrice * item.Discount) / 100)))))+ (
                                  x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Cash" && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                     (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *
                                 item.TaxRate.Rate / 100))), 
                            Bank = (x.Where(x => x.PaymentType == "Bank" && x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice)) -
                            (x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Bank").Sum(z => z.PurchasePostItems.Sum(item =>
                                   (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                       : (item.Quantity * item.UnitPrice * item.Discount) / 100))))) + (
                                  x.Where(x => x.PurchaseInvoiceId == null && x.PaymentType == "Bank" && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                     (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *
                                 item.TaxRate.Rate / 100))),
                            Discount = (x.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item =>
                                   (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                       : (item.Quantity * item.UnitPrice * item.Discount) / 100)))- x.Where(x => x.PurchaseInvoiceId != null).Sum(z => z.PurchasePostItems.Sum(item =>
                                   (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                       : (item.Quantity * item.UnitPrice * item.Discount) / 100)))),
                            TotalTax = x.Where(x => x.PurchaseInvoiceId == null &&  (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item => 
                                   ((item.Quantity * item.UnitPrice) -
                                   (item.Discount == 0
                                          ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                     ) *

                                  item.TaxRate.Rate / (100 + item.TaxRate.Rate)))+
                                  x.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                     (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *
                                 item.TaxRate.Rate / 100))
                            ,
                                 
                            TotalTaxReturn = x.Where(x => x.PurchaseInvoiceId != null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                  (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *

                                 item.TaxRate.Rate / (100 + item.TaxRate.Rate))) +
                                  x.Where(x => x.PurchaseInvoiceId != null && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                     (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *
                                 item.TaxRate.Rate / 100)),
                            TotalReturns = ((x.Where(x =>  x.PurchaseInvoiceId != null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice)) -x.Where(x => x.PurchaseInvoiceId != null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                  ((item.Quantity * item.UnitPrice) -
                                  (item.Discount == 0
                                         ? item.Quantity * item.FixDiscount
                                         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                    ) *

                                 item.TaxRate.Rate / (100 + item.TaxRate.Rate))))),




                        }).ToList();


                        foreach (var x in inventoryList)
                        {
                            monthlySale.Add(new MonthlySaleModel()
                            {

                                Date = x.Date,
                                Cash = x.Cash,
                                Bank = x.Bank ,
                                Credit = x.Credit ,
                                GrossAmount = x.GrossAmount,
                                Discount = x.Discount < 0 ? x.Discount * -1 : x.Discount,
                                TotalReturns = x.TotalReturns < 0 ? x.TotalReturns * -1 : x.TotalReturns,
                                //TotalTax =  x.TotalTax < 0 ? (x.TotalTax * -1) - (x.TotalTaxReturn < 0 ? x.TotalTaxReturn * -1 : x.TotalTaxReturn) : x.TotalTax-(x.TotalTaxReturn < 0 ? x.TotalTaxReturn * -1 : x.TotalTaxReturn),
                                TotalTax =  (x.TotalTax - x.TotalTaxReturn)<0 ? (x.TotalTax - x.TotalTaxReturn)*-1: (x.TotalTax - x.TotalTaxReturn),
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
