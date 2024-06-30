using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Dashboard.Queries
{
    public class GetCashTransactionQuery : IRequest<TransactionLookupModel>
    {
        public DateTime? SelectedDate { get; set; }
        public DateTime? CurrentDate { get; set; }
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<GetCashTransactionQuery, TransactionLookupModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly IMediator Mediator;
            public readonly ILogger Logger;

            public Handler(IMediator mediator, IApplicationDbContext context, ILogger<GetCashTransactionQuery> logger)
            {
                Context = context;
                Mediator = mediator;
                Logger = logger;
            }

            public async Task<TransactionLookupModel> Handle(GetCashTransactionQuery request, CancellationToken cancellationToken)

            {
                try
                {
                    var sales = new List<Sale>();
                    var purchase = new List<PurchasePost>();
                    var earning = new List<EarningLookUpModel>();
                    var salePurchase = new List<SalePurchaseLookUpModel>();
                    var list2 = new SalePurchaseLookUpModel();
                    decimal expeneAmount = 0;


                    #region Filter
                    if (request.OverViewFilter == "Today")
                    {
                        if (Context.Sales != null)
                        {
                            sales = Context.Sales
                           .Where(x => x.InvoiceType != 0 && x.Date.Date == DateTime.UtcNow.Date)
                           .ToList();
                        }

                   
                        purchase = Context.PurchasePosts
                        .Where(x => x.Date.Date == DateTime.UtcNow.Date && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .Select(y => new PurchasePost()
                        {
                            Id = y.Id,
                            TotalAmount = y.TotalAmount,
                            TotalWithOutAmount = y.TotalWithOutAmount,
                            VatAmount = y.VatAmount,
                            DiscountAmount = y.DiscountAmount,
                            Date = y.Date,
                            IsPurchasePost = y.IsPurchasePost,
                            IsPurchaseReturn = y.IsPurchaseReturn,
                            PartiallyInvoices = y.PartiallyInvoices,
                            IsCredit = y.IsCredit,
                            PurchaseInvoiceId = y.PurchaseInvoiceId,
                        })
                        .ToList();



                        expeneAmount = Context.DailyExpenses
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date == DateTime.UtcNow.Date)
                           .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0 : x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));


                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddDays(-6);
                        sales = Context.Sales.AsNoTracking()
                           .Where(x => x.InvoiceType != 0 && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                           .ToList();


                        purchase = Context.PurchasePosts.AsNoTracking()
                        .Where(x => x.Date.Date >= firstDay && x.Date.Date <= lastDay && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .ToList();

                        expeneAmount = Context.DailyExpenses
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date == DateTime.UtcNow.Date)
                           .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0 : x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));


                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        try { 
                            sales = Context.Sales.AsNoTracking()
                            .Where(x => x.InvoiceType != 0 && x.Date.Month == DateTime.UtcNow.Month)
                            .ToList();
                        }
                        catch 
                        {

                        }
                        try
                        {
                            purchase = Context.PurchasePosts.AsNoTracking()
                            .Where(x => x.Date.Month == DateTime.UtcNow.Month)
                            .ToList();
                        }
                        catch
                        {

                        }
                        try
                        {
                            expeneAmount = Context.DailyExpenses
                            .Include(x => x.DailyExpenseDetails)
                             .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date == DateTime.UtcNow.Date)
                               .AsEnumerable()
                             .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0 : x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));
                        }
                        catch { }
                        }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-6);
                        sales = Context.Sales.AsNoTracking()
                           .Where(x => x.InvoiceType != 0 && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                           .ToList();


                        purchase = Context.PurchasePosts.AsNoTracking()
                        .Where(x => x.Date.Date >= firstDay && x.Date.Date <= lastDay && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .ToList();

                        expeneAmount = Context.DailyExpenses
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date == DateTime.UtcNow.Date)
                           .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0 : x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));
                    }
                    #endregion

                    #region Calculation
                    // For Total Income

                    var income = sales.Where(x => x.SaleInvoiceId == null).Sum(x => x.TotalWithOutAmount);

                    // For Credit Amount
                    var Credit = sales.Where(x => x.IsCredit && x.SaleInvoiceId == null).Sum(x => x.TotalWithOutAmount);

                    var totalInvoices = sales.Count();
                    var unPaidInvoices = sales.Count(x => x.PartiallyInvoices == Domain.Enum.PartiallyInvoices.UnPaid);
                    var partiallyInvoices = sales.Count(x => x.PartiallyInvoices == Domain.Enum.PartiallyInvoices.Partially);
                    var fullyInvoices = totalInvoices - unPaidInvoices - partiallyInvoices;
                    var totalCreditInvoices = sales.Count(x => x.IsCredit && x.SaleInvoiceId == null);
                    var totalCashInvoices = sales.Count(x => !x.IsCredit && x.SaleInvoiceId == null);
                    var totalReturns = sales.Count(x => x.SaleInvoiceId != null);


                    // For Purchase


                    var GrossAmount = purchase.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.TotalWithOutAmount);
                    decimal Discount = purchase.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.DiscountAmount);
                    decimal TotalTax = purchase.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.VatAmount); ;
                    var totalPurchases = (GrossAmount - Discount) + TotalTax;
                    #endregion
                    #region Earning Graph
                    foreach (var sale in sales)
                    {
                        if (sale.SaleInvoiceId == null)
                        {
                            earning.Add(new EarningLookUpModel
                            {
                                Date = sale.Date,
                                Amount = sale.TotalWithOutAmount,
                               
                            });

                        }


                    }
                    if (request.OverViewFilter == "Today")
                    {
                        var records = earning.GroupBy(x => x.Date.Hour);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var records = earning.GroupBy(x => x.Date.DayOfWeek);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        var records = earning.GroupBy(x => x.Date.Date);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.Key,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var records = earning.GroupBy(x => x.Date.Month);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    #endregion

                    #region SalePurcahse Comparision Graph
                    var saleList = new List<SalePurchaseLookUpModel>();
                    var list4 = new List<SalePurchaseLookUpModel>();

                    var list1 = (from po in purchase
                                 select new SalePurchaseLookUpModel()
                                 {
                                     Date = po.Date,
                                     IsPurchase = true,
                                     PurchaseAmount = purchase.Where(x => x.PurchaseInvoiceId == null).Sum(x => x.TotalWithOutAmount)


                                 }
                                         );

                    foreach (var sale in sales)
                    {
                        if (sale.SaleInvoiceId == null)
                        {
                            list4.Add(new SalePurchaseLookUpModel
                            {
                                Date = sale.Date,
                                IsPurchase = false,
                                SaleAmount = sale.TotalWithOutAmount,
                            });

                        }


                    }

                    var mergeList = list1.Union(list4).OrderBy(x => x.Date);


                    foreach (var y in mergeList)
                    {
                        if (list2.Date == y.Date)
                        {
                            if (list2.IsPurchase)
                            {
                                var index = salePurchase.FindIndex(x => x.Date == y.Date);
                                salePurchase.RemoveAt(index);
                                salePurchase.Add(new SalePurchaseLookUpModel()
                                {
                                    Date = list2.Date,
                                    SaleAmount = (y.SaleAmount - y.SaleDiscount) + y.SaleVAT,
                                    PurchaseAmount = list2.PurchaseAmount,

                                });
                            }


                            else
                            {
                                var index = salePurchase.FindIndex(x => x.Date == y.Date);
                                salePurchase.RemoveAt(index);

                                salePurchase.Add(new SalePurchaseLookUpModel()
                                {
                                    SaleAmount = (y.SaleAmount - y.SaleDiscount) + y.SaleVAT,
                                    Date = list2.Date,
                                    PurchaseAmount = y.PurchaseAmount,
                                });
                            }

                        }
                        else
                        {
                            salePurchase.Add(new SalePurchaseLookUpModel()
                            {
                                SaleAmount = (y.SaleAmount - y.SaleDiscount) + y.SaleVAT,
                                Date = y.Date,
                                PurchaseAmount = y.PurchaseAmount,

                            });
                        }
                        list2 = y;


                    }
                    if (request.OverViewFilter == "Today")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Hour);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.DayOfWeek);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            Week = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Date);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            Date = x.Key,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Month);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }


                    #endregion




                    return new TransactionLookupModel
                    {

                        Income = income,
                        UnPaidInvoices = unPaidInvoices,
                        PartiallyInvoices = partiallyInvoices,
                        FullyInvoices = fullyInvoices,
                        TotalInvoices = totalInvoices,
                        TotalCreditInvoices = totalCreditInvoices,
                        TotalCashInvoices = totalCashInvoices,
                        TotalReturn = totalReturns,
                        CreditAmount = Credit,
                        Expense = expeneAmount,
                        Purchase = totalPurchases,
                        EarningList = earning,
                        SalePurchaseLookUpModel = salePurchase,
                    };
                }


                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }

            }
        }
    }

}

