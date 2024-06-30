using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Domain.Enum;


namespace Focus.Business.Sales.Queries.GetSaleList
{
    public class SaleListQueryForOnlySaleRecord : IRequest<SaleDashboardModel>
    {

       public bool IsService { get; set; }
       public bool IsPurchase { get; set; }
       public bool IsProforma { get; set; }
       public bool IsPurchaseOrder { get; set; }
       public bool IsPurchaseReturn { get; set; }
       public InvoiceType Satus { get; set; }

        public Guid? BranchId { get; set; }


        public class Handler : IRequestHandler<SaleListQueryForOnlySaleRecord, SaleDashboardModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<SaleListQueryForOnlySaleRecord> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<SaleDashboardModel> Handle(SaleListQueryForOnlySaleRecord request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsPurchase)
                    {
                        var pi = await _context.PurchasePosts
                            .Select(x => new
                            {
                                x.Id,
                                x.IsCredit,
                                x.PartiallyInvoices,
                                x.IsPurchasePost,
                                x.SupplierId,
                                SupplierName = x.Supplier.EnglishName,
                                TotalAmount = x.TotalAmount,
                                IsPurchaseReturn = x.IsPurchaseReturn,
                                x.BranchId
                            }).ToListAsync(cancellationToken: cancellationToken);

                        if(request.IsPurchase && !request.IsPurchaseReturn)
                        {
                            pi = pi.Where(x => !x.IsPurchaseReturn).ToList();
                        }
                        else if(request.IsPurchase && request.IsPurchaseReturn)
                        {
                            pi = pi.Where(x => x.IsPurchaseReturn).ToList();
                        }

                        if (request.BranchId != null)
                        {
                            pi = pi.Where(x => x.BranchId == request.BranchId).ToList();
                        }
                        //Credit List by Amount

                        var groupByCustomerCredit = pi
                            .Where(x => x.IsCredit && x.IsPurchasePost).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();

                        var creditListByAmount = groupByCustomerCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Paid List by Amount

                        var groupByCustomerPaid = pi.Where(x => !x.IsCredit && x.IsPurchasePost)
                            .GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();

                        var paidListByAmount = groupByCustomerPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Credit Customer
                        var groupByCustomerByCredit = pi
                            .Where(x => x.IsCredit && x.IsPurchasePost).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Count()
                            }).ToList();

                        var creditList = groupByCustomerByCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Paid Customer
                        var groupByCustomerByPaid = pi
                            .Where(x => !x.IsCredit && x.IsPurchasePost).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Count()
                            }).ToList();


                        var paidList = groupByCustomerByPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();








                        var saleModel = new SaleDashboardModel();
                        saleModel.TotalHold = pi.Where(x => !x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalPartially = pi.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.Partially && x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalUnPaid = pi.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.UnPaid && x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalFullyPaid = pi.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.Fully && x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalCash = pi.Where(x => !x.IsCredit && x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalCredit = pi.Where(x => x.IsCredit && x.IsPurchasePost)
                            .Sum(x => x.TotalAmount);

                        saleModel.Hold = pi.Count(x => !x.IsPurchasePost);
                        saleModel.Partially = pi.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.Partially && x.IsPurchasePost);
                        saleModel.UnPaid = pi.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.UnPaid && x.IsPurchasePost);
                        saleModel.FullyPaid = pi.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.Fully && x.IsPurchasePost);
                        saleModel.Cash = pi.Count(x => !x.IsCredit && x.IsPurchasePost);
                        saleModel.Credit = pi.Count(x => x.IsCredit && x.IsPurchasePost);

                        return new SaleDashboardModel
                        {
                            Hold = saleModel.Hold,
                            Credit = saleModel.Credit,
                            Cash = saleModel.Cash,
                            Partially = saleModel.Partially,
                            UnPaid = saleModel.UnPaid,
                            FullyPaid = saleModel.FullyPaid,
                            TotalHold = saleModel.TotalHold,
                            TotalCredit = saleModel.TotalCredit,
                            TotalCash = saleModel.TotalCash,
                            TotalPartially = saleModel.TotalPartially,
                            TotalUnPaid = saleModel.TotalUnPaid,
                            TotalFullyPaid = saleModel.TotalFullyPaid,
                            CreditListByAmount = creditListByAmount,
                            PaidListByAmount = paidListByAmount,
                            CreditList = creditList,
                            PaidList = paidList,

                        };

                    }
                    else if (request.IsProforma)
                    {
                        if (request.IsPurchaseOrder)
                        {
                            request.Satus = InvoiceType.PurchaseOrder;
                        }
                        else
                        {

                            request.Satus = InvoiceType.ProformaInvoice;


                        }

                        var saleList =await _context.Sales.AsNoTracking()
                            .Include(x => x.Customer)
                            .Where(x => x.IsService == request.IsService && !x.IsCredit && x.InvoiceType == request.Satus)
                            .Select(x => new
                            {
                                Id = x.Id,
                                IsCredit = x.IsCredit,
                                InvoiceType = x.InvoiceType,
                                PartiallyInvoices = x.PartiallyInvoices,
                                CustomerId = x.CustomerId,
                                CustomerName = x.Customer.CustomerDisplayName,
                                TotalAmount = x.TotalAmount
                            }).ToListAsync(cancellationToken: cancellationToken);

                        //Credit List by Amount

                        var groupByCustomerCredit = saleList
                            .Where(x => x.InvoiceType == request.Satus).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.CustomerName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        var creditListByAmount = groupByCustomerCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();


                        //Credit Customer
                        var groupByCustomerByCredit = saleList
                            .Where(x => x.InvoiceType == request.Satus).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.CustomerName,
                                Amount = x.Count()
                            }).ToList();
                        var creditList = new List<DashboardLookUp>();
                        creditList = groupByCustomerByCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();
                        var saleModel = new SaleDashboardModel();

                        saleModel.TotalCredit = saleList.Where(x => x.InvoiceType == request.Satus)
                            .Sum(x => x.TotalAmount);
                        saleModel.Credit = saleList.Count(x =>  x.InvoiceType == request.Satus);
                        return new SaleDashboardModel
                        {
                            Hold = saleModel.Hold,
                            Credit = saleModel.Credit,
                            Cash = saleModel.Cash,
                            Partially = saleModel.Partially,
                            UnPaid = saleModel.UnPaid,
                            FullyPaid = saleModel.FullyPaid,
                            TotalHold = saleModel.TotalHold,
                            TotalCredit = saleModel.TotalCredit,
                            TotalCash = saleModel.TotalCash,
                            TotalPartially = saleModel.TotalPartially,
                            TotalUnPaid = saleModel.TotalUnPaid,
                            TotalFullyPaid = saleModel.TotalFullyPaid,
                            CreditListByAmount = creditListByAmount,
                            CreditList = creditList,

                        };
                    }
                    else
                    {
                        var saleList = await _context.Sales.AsNoTracking()
                            .Include(x => x.Customer)
                            .Where(x => x.IsService == request.IsService)
                            .Select(x => new
                            {
                                Id = x.Id,
                                IsCredit = x.IsCredit,
                                InvoiceType = x.InvoiceType,
                                PartiallyInvoices = x.PartiallyInvoices,
                                CustomerId = x.CustomerId,
                                CustomerName = x.Customer.CustomerDisplayName,
                                TotalAmount = x.TotalAmount,
                                x.BranchId
                            }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            saleList = saleList.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        //Credit List by Amount

                        var groupByCustomerCredit = saleList
                            .Where(x => x.IsCredit && x.InvoiceType == InvoiceType.Credit).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                x.FirstOrDefault()!.CustomerName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        var creditListByAmount = groupByCustomerCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Paid List by Amount
                        var groupByCustomerPaid = saleList.Where(x => !x.IsCredit && x.InvoiceType == InvoiceType.Paid)
                            .GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                x.FirstOrDefault()!.CustomerName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        var paidListByAmount = groupByCustomerPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Credit Customer
                        var groupByCustomerByCredit = saleList
                            .Where(x => x.IsCredit && x.InvoiceType == InvoiceType.Credit).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                x.FirstOrDefault()!.CustomerName,
                                Amount = x.Count()
                            }).ToList();
                        var creditList = groupByCustomerByCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();
                        //Paid Customer
                        var groupByCustomerByPaid = saleList
                            .Where(x => !x.IsCredit && x.InvoiceType == InvoiceType.Paid).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                x.FirstOrDefault()!.CustomerName,
                                Amount = x.Count()
                            }).ToList();


                        var paidList = groupByCustomerByPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();





                        var saleModel = new SaleDashboardModel();
                        saleModel.TotalHold = saleList.Where(x => x.InvoiceType == InvoiceType.Hold)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalPartially = saleList.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.Partially && x.InvoiceType != InvoiceType.Hold)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalUnPaid = saleList.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.UnPaid && x.InvoiceType != InvoiceType.Hold)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalFullyPaid = saleList.Where(x =>
                                x.PartiallyInvoices == PartiallyInvoices.Fully && x.InvoiceType != InvoiceType.Hold)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalCash = saleList.Where(x => !x.IsCredit && x.InvoiceType == InvoiceType.Paid)
                            .Sum(x => x.TotalAmount);
                        saleModel.TotalCredit = saleList.Where(x => x.IsCredit && x.InvoiceType == InvoiceType.Credit)
                            .Sum(x => x.TotalAmount);

                        saleModel.Hold = saleList.Count(x => x.InvoiceType == InvoiceType.Hold);
                        saleModel.Partially = saleList.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.Partially && x.InvoiceType != InvoiceType.Hold);
                        saleModel.UnPaid = saleList.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.UnPaid && x.InvoiceType != InvoiceType.Hold);
                        saleModel.FullyPaid = saleList.Count(x =>
                            x.PartiallyInvoices == PartiallyInvoices.Fully && x.InvoiceType != InvoiceType.Hold);
                        saleModel.Cash = saleList.Count(x => !x.IsCredit && x.InvoiceType == InvoiceType.Paid);
                        saleModel.Credit = saleList.Count(x => x.IsCredit && x.InvoiceType == InvoiceType.Credit);






                        return new SaleDashboardModel
                        {
                            Hold = saleModel.Hold,
                            Credit = saleModel.Credit,
                            Cash = saleModel.Cash,
                            Partially = saleModel.Partially,
                            UnPaid = saleModel.UnPaid,
                            FullyPaid = saleModel.FullyPaid,
                            TotalHold = saleModel.TotalHold,
                            TotalCredit = saleModel.TotalCredit,
                            TotalCash = saleModel.TotalCash,
                            TotalPartially = saleModel.TotalPartially,
                            TotalUnPaid = saleModel.TotalUnPaid,
                            TotalFullyPaid = saleModel.TotalFullyPaid,
                            CreditListByAmount = creditListByAmount,
                            PaidListByAmount = paidListByAmount,
                            CreditList = creditList,
                            PaidList = paidList,
                        };
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
