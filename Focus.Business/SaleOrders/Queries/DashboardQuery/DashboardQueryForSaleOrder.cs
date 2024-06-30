using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Domain.Enum;


namespace Focus.Business.SaleOrders.Queries.DashboardQuery
{
    
   public class DashboardQueryForSaleOrder : IRequest<SaleOrderDashboardLookUpModel>
    {

        public bool IsService { get; set; }
        public bool IsQuotation { get; set; }
        public bool IsPurchaseOrderInvoice { get; set; }
        public bool IsSupplierQuotation { get; set; }
        public Guid? BranchId { get; set; }


        public class Handler : IRequestHandler<DashboardQueryForSaleOrder, SaleOrderDashboardLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<DashboardQueryForSaleOrder> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<SaleOrderDashboardLookUpModel> Handle(DashboardQueryForSaleOrder request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.IsPurchaseOrderInvoice)
                    {

                        var po = _context.PurchaseOrders.AsNoTracking()
                                    .Include(x => x.Supplier)
                                    .Select(x => new
                                    {
                                        Id = x.Id,
                                        SupplierId = x.SupplierId,
                                        SupplierName = x.Supplier.CustomerDisplayName,
                                        ApprovalStatus = x.ApprovalStatus,
                                        TotalAmount = x.TotalAmount,
                                        DocumentType = x.DocumentType,
                                    }).ToList();

                        if (request.IsSupplierQuotation)
                        {
                            po = po.Where(x => x.DocumentType == "SupplierQuotation").ToList();
                        }
                        else
                        {
                            po = po.Where(x => x.DocumentType != "SupplierQuotation").ToList();
                        }

                        //Draft by Amount
                        var creditListByAmount = new List<DashboardLookUp>();

                        var groupByCustomerCredit = po
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Draft).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();

                        creditListByAmount = groupByCustomerCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Approved List by Amount
                        var paidListByAmount = new List<DashboardLookUp>();

                        var groupByCustomerPaid = po.Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                            .GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        paidListByAmount = groupByCustomerPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Credit Customer
                        var groupByCustomerByCredit = po
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Draft).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
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

                        //Paid Customer
                        var groupByCustomerByPaid = po
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved).GroupBy(x => x.SupplierId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.SupplierName,
                                Amount = x.Count()
                            }).ToList();


                        var paidList = new List<DashboardLookUp>();
                        paidList = groupByCustomerByPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();


                        var saleModel = new SaleOrderDashboardLookUpModel();
                        saleModel.TotalDraft = po.Where(x => x.ApprovalStatus == ApprovalStatus.Draft).Sum(x => x.TotalAmount);

                        saleModel.TotalPartially = po.Where(x => x.ApprovalStatus == ApprovalStatus.InProcess).Sum(x => x.TotalAmount);

                        saleModel.TotalApproved = po.Where(x => x.ApprovalStatus == ApprovalStatus.Approved).Sum(x => x.TotalAmount);


                        saleModel.Draft = po.Count(x => x.ApprovalStatus == ApprovalStatus.Draft);
                        saleModel.Approved = po.Count(x => x.ApprovalStatus == ApprovalStatus.Approved);
                        saleModel.Processing = po.Count(x => x.ApprovalStatus == ApprovalStatus.InProcess);

                        return new SaleOrderDashboardLookUpModel
                        {
                            Draft = saleModel.Draft,
                            Approved = saleModel.Approved,
                            TotalDraft = saleModel.TotalDraft,
                            TotalApproved = saleModel.TotalApproved,
                            CreditListByAmount = creditListByAmount,
                            PaidListByAmount = paidListByAmount,
                            CreditList = creditList,
                            PaidList = paidList,
                            Processing = saleModel.Processing,
                            TotalPartially = saleModel.TotalPartially,
                        };
                    }
                    else
                    {
                        var saleList = _context.SaleOrders.AsNoTracking()
                            .Include(x => x.Customer)
                            .Where(x => x.IsService == request.IsService && x.IsQuotation==request.IsQuotation)
                            .Select(x => new
                            {
                                Id = x.Id,
                                CustomerId = x.CustomerId,
                                ApprovalStatus = x.ApprovalStatus,
                                CustomerName = x.Customer.CustomerDisplayName,
                                TotalAmount = x.TotalAmount,
                                x.BranchId,
                            })
                            .ToList();



                        if (request.BranchId != null)
                        {
                            saleList = saleList.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        //Draft by Amount
                        var creditListByAmount = new List<DashboardLookUp>();

                        var groupByCustomerCredit = saleList
                            .Where(x => x.ApprovalStatus==ApprovalStatus.Draft).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.CustomerName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        creditListByAmount = groupByCustomerCredit.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();
                        //Approved List by Amount
                        var paidListByAmount = new List<DashboardLookUp>();

                        var groupByCustomerPaid = saleList.Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                            .GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.CustomerName,
                                Amount = x.Sum(y => y.TotalAmount)
                            }).ToList();
                        paidListByAmount = groupByCustomerPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();

                        //Credit Customer

                        var groupByCustomerByCredit = saleList
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Draft).GroupBy(x => x.CustomerId)
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
                        //Paid Customer
                        var groupByCustomerByPaid = saleList
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved).GroupBy(x => x.CustomerId)
                            .Select(x => new
                            {
                                Id = x.Key.ToString(),
                                CustomerName = x.FirstOrDefault()!.CustomerName,
                                Amount = x.Count()
                            }).ToList();


                        var paidList = new List<DashboardLookUp>();
                        paidList = groupByCustomerByPaid.Select(x =>
                            new DashboardLookUp()
                            {
                                Id = x.Id,
                                Name = x.CustomerName,
                                Amount = x.Amount,

                            }).OrderByDescending(y => y.Amount).Take(5).ToList();



                        var saleModel = new SaleOrderDashboardLookUpModel();
                        saleModel.TotalDraft = saleList.Where(x => x.ApprovalStatus == ApprovalStatus.Draft)
                            .Sum(x => x.TotalAmount);
                       
                    
                        saleModel.TotalApproved = saleList.Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                            .Sum(x => x.TotalAmount);
                     

                        saleModel.Draft = saleList.Count(x => x.ApprovalStatus == ApprovalStatus.Draft);
                        saleModel.Approved = saleList.Count(x =>
                            x.ApprovalStatus == ApprovalStatus.Approved);

                        return new SaleOrderDashboardLookUpModel
                        {
                            Draft = saleModel.Draft,
                            Approved = saleModel.Approved,
                            TotalDraft = saleModel.TotalDraft,
                            TotalApproved = saleModel.TotalApproved,
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
