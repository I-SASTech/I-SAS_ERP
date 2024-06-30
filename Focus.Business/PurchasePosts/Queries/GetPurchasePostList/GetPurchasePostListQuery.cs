using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Users;
using Focus.Business.PurchaseOrders;
using Focus.Business.GoodReceives.Commands.AddGoodReceive;
using Focus.Business.GoodReceives.Queries;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostList
{
    public class GetPurchasePostListQuery : PagedRequest, IRequest<PagedResult<List<PurchasePostLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsPost { get; set; }
        public Guid SupplierId { get; set; }
        public Guid SupplierAccountId { get; set; }
        public bool IsExpense { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Status { get; set; }
        public string SupplierType { get; set; }
        public string PaymentMode { get; set; }
        public Guid? SupplierForFilterId { get; set; }
        public Guid? UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetPurchasePostListQuery, PagedResult<List<PurchasePostLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetPurchasePostListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;

            }

            public async Task<PagedResult<List<PurchasePostLookUpModel>>> Handle(GetPurchasePostListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var purchaseOrders = _context.PurchasePosts.AsNoTracking()
                                   .Include(x => x.PurchasePostItems)
                                   .ThenInclude(x => x.TaxRate)
                                   .Include(x => x.PurchasePostItems)
                                   .ThenInclude(x => x.Product)
                                   .Include(x => x.Supplier)
                                   .Where(x => x.IsPurchasePost)
                                   .AsQueryable();


                        if (request.BranchId != null)
                        {
                            purchaseOrders = purchaseOrders.Where(x => x.BranchId == request.BranchId);
                        }

                        if (request.IsExpense == false)
                        {
                            purchaseOrders = purchaseOrders.Where(x => !x.IsPurchaseReturn && !x.IsClose && x.IsPurchasePost);
                        }
                        else
                        {
                            purchaseOrders = purchaseOrders.Where(x => x.PurchaseInvoiceId == null && x.PartiallyInvoices != Domain.Enum.PartiallyInvoices.Fully);
                        }

                        if (purchaseOrders == null)
                            throw new NotFoundException("Purchase Invoice", request.SupplierId);

                        if (request.SupplierId != Guid.Empty)
                        {
                            purchaseOrders = purchaseOrders.Where(x => x.SupplierId == request.SupplierId);
                        }

                        if (request.SupplierAccountId != Guid.Empty)
                        {
                            purchaseOrders = purchaseOrders.Where(x => x.Supplier.AccountId == request.SupplierAccountId);
                        }
                       

                        var purchaseOrderList = new List<PurchasePostLookUpModel>();
                        foreach (var purchaseOrder in purchaseOrders)
                        {
                            var netAmount = new TotalAmount();
                            var lookUpModel = new PurchasePostLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                InvoiceNo = purchaseOrder.InvoiceNo,
                                PartiallyInvoices = purchaseOrder.PartiallyInvoices,
                                ReceivedAmount = purchaseOrder.ReceivedAmount,
                                SupplierName = purchaseOrder.Supplier.CustomerDisplayName,
                                SupplierNameArabic = purchaseOrder.Supplier.ArabicName,
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                NetAmount = purchaseOrder.TotalAmount,
                            };
                            purchaseOrderList.Add(lookUpModel);
                        }
                        return new PagedResult<List<PurchasePostLookUpModel>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        _context.DisableTenantFilter = true;

                        var pi = _context.PurchasePosts
                            .AsNoTracking()
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Supplier)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            pi = pi.Where(x => x.BranchId == request.BranchId);
                        }

                        var query = pi.Where(x => x.IsPurchaseReturn == request.IsPost);
                        query = request.Status == "post" ? query.Where(x => x.IsPurchasePost) : query.Where(x => !x.IsPurchasePost);

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }
                        if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                        {
                            query = query.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year);

                        }

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                                x.RegistrationNo.Contains(searchTerm) || x.Supplier.EnglishName.Contains(searchTerm) || x.Supplier.ArabicName.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                        if (request.SupplierForFilterId != null)
                        {
                            query = query.Where(x => x.SupplierId == request.SupplierForFilterId);
                        }
                        if (request.PaymentMode != null && request.PaymentMode != "null" && request.PaymentMode != "")
                        {
                            var searchTerm = request.PaymentMode;
                            query = query.Where(x => x.PaymentType == searchTerm);
                        }
                        if (request.UserId != null)
                        {
                            query = query.Where(x => EF.Property<string>(x, "CreatedById") == request.UserId.ToString());
                        }
                        if (request.SupplierType == "Credit")
                        {
                            query = query.Where(x => x.Supplier.PaymentTerms == "Credit");
                        }
                        if (request.SupplierType == "Cash")
                        {
                            query = query.Where(x => x.Supplier.PaymentTerms == "Cash");
                        }

                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList = new List<PurchasePostLookUpModel>();

                        var usersList = _userManager.Users.ToList();

                        var purchaseOrdersList = await _context.PurchaseOrders.AsNoTracking().Select(x => new PurchaseOrderLookupModel
                        {
                            Id = x.Id,
                            RegistrationNo = x.RegistrationNo,
                            Date = x.Date
                        }).ToListAsync();

                        var goodsRecieveList = await _context.GoodReceives.AsNoTracking().Select(x => new GoodReceiveListLookUpModel
                        {
                            Id = x.Id,
                            RegistrationNumber = x.RegistrationNo,
                            Date = x.Date.ToString("dd/MM/yyyy")
                        }).ToListAsync();

                        var temporaryList = query.Select(purchaseOrder => new
                        {
                            Id = purchaseOrder.Id,
                            IsCost = purchaseOrder.IsCost,
                            CreatedBy = EF.Property<string>(purchaseOrder, "CreatedById"),
                            SupplierId = purchaseOrder.SupplierId,
                            RegistrationNumber = purchaseOrder.RegistrationNo,
                            PurchaseInvoiceNo = purchaseOrder.PurchaseInvoiceId != null ? purchaseOrder.PurchaseInvoiceId : null,
                            SupplierName = purchaseOrder.Supplier.CustomerDisplayName,
                            PartiallyInvoices = purchaseOrder.PartiallyInvoices,
                            SupplierNameArabic = purchaseOrder.Supplier.ArabicName,
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            IsPurchaseReturn = purchaseOrder.IsPurchaseReturn,
                            purchaseInvoiceId = purchaseOrder.PurchaseInvoiceId,
                            NetAmount = purchaseOrder.TotalAmount,
                            GoodReceiveNoteId = purchaseOrder.GoodReceiveNoteId,
                            Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                            purchaseOrder.BranchId,
                            IsClose = purchaseOrder.IsClose,
                            PoNumberAndDate = purchaseOrder.poNumberAndDate,
                            GoodsRecieveNumberAndDate = purchaseOrder.GoodsRecieveNumberAndDate,
                            Reference = purchaseOrder.Reference,
                            IsDefault = purchaseOrder.Supplier.IsAllowEmail,
                            CustomerEmail = purchaseOrder.Supplier.Email,
                        }).ToList();

                        var branchList = _context.Branches.AsNoTracking().ToList();

                        foreach (var tempItem in temporaryList)
                        {
                            var lookUpModel = new PurchasePostLookUpModel
                            {
                                Id = tempItem.Id,
                                IsCost = tempItem.IsCost,
                                IsClose = tempItem.IsClose,
                                CreatedBy = usersList.FirstOrDefault(x => x.Id == tempItem.CreatedBy)?.UserName,
                                CreatedFrom = tempItem.PurchaseOrderId == null ? tempItem.GoodReceiveNoteId != null ? goodsRecieveList != null ? goodsRecieveList.FirstOrDefault(x => x.Id == tempItem.GoodReceiveNoteId)?.RegistrationNumber + " - " + goodsRecieveList.FirstOrDefault(x => x.Id == tempItem.GoodReceiveNoteId)?.Date : null : tempItem.purchaseInvoiceId != null ? pi.FirstOrDefault(x => x.Id == tempItem.purchaseInvoiceId)?.RegistrationNo + " - " + pi.FirstOrDefault(x => x.Id == tempItem.purchaseInvoiceId)?.Date.ToString("dd/MM/yyyy") : null : purchaseOrderList != null ? purchaseOrdersList.FirstOrDefault(x => x.Id == tempItem.PurchaseOrderId)?.RegistrationNo + " - " + purchaseOrdersList.FirstOrDefault(x => x.Id == tempItem.PurchaseOrderId)?.Date.ToString("dd/MM/yyyy") : null,
                                SupplierId = tempItem.SupplierId,
                                RegistrationNumber = tempItem.RegistrationNumber,
                                PurchaseInvoiceNo = tempItem.PurchaseInvoiceNo != null ? pi.FirstOrDefault(x => x.Id == tempItem.PurchaseInvoiceNo)?.RegistrationNo : null,
                                SupplierName = tempItem.SupplierName,
                                PartiallyInvoices = tempItem.PartiallyInvoices,
                                SupplierNameArabic = tempItem.SupplierNameArabic,
                                PurchaseOrderId = tempItem.PurchaseOrderId,
                                IsPurchaseReturn = tempItem.IsPurchaseReturn,
                                NetAmount = tempItem.NetAmount,
                                PurchaseInvoiceId = tempItem.purchaseInvoiceId,
                                GoodReceiveNoteId = tempItem.GoodReceiveNoteId,
                                Date = tempItem.Date,
                                BranchCode = branchList.FirstOrDefault(x => x.Id == tempItem.BranchId)?.Code,
                                PoNumberAndDate = tempItem.PoNumberAndDate,
                                GoodsRecieveNumberAndDate = tempItem.GoodsRecieveNumberAndDate,
                                Reference = tempItem.Reference,
                                IsDefault = tempItem.IsDefault,
                                CustomerEmail = tempItem.CustomerEmail,
                            };
                            purchaseOrderList.Add(lookUpModel);
                        }


                        if (request.TotalAmount != null && request.TotalAmount != 0)
                        {
                            purchaseOrderList = purchaseOrderList.Where(x => x.NetAmount.ToString().Contains(request.TotalAmount.ToString())).ToList();
                        }

                        return new PagedResult<List<PurchasePostLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
