using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.GoodReceives.Queries
{
    public class GetGoodReceiveListQuery : PagedRequest, IRequest<PagedResult<List<GoodReceiveListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetGoodReceiveListQuery, PagedResult<List<GoodReceiveListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;


            public Handler(IApplicationDbContext context, ILogger<GetGoodReceiveListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;

            }
            public async Task<PagedResult<List<GoodReceiveListLookUpModel>>> Handle(GetGoodReceiveListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.GoodReceives
                            .AsNoTracking()
                            .Include(x => x.GoodReceiveNoteItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.GoodReceiveNoteItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Supplier)
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose)
                            .AsQueryable();

                        if (request.BranchId!=null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }


                        if(request.SupplierId != null)
                        {
                            po = po.Where(x => x.SupplierId == request.SupplierId);
                        }
                        var purchaseOrderList = new List<GoodReceiveListLookUpModel>();
                        var netAmount = new TotalAmount();
                        foreach (var purchaseOrder in po)
                        {
                            purchaseOrderList.Add(new GoodReceiveListLookUpModel()
                            {
                                Id = purchaseOrder.Id,
                                PartiallyReceived = purchaseOrder.PartiallyReceived,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                SupplierName = purchaseOrder.Supplier?.EnglishName,
                                SupplierNameArabic = purchaseOrder.Supplier?.ArabicName,
                                NetAmount = netAmount.CalculateGrnTotalAmount(purchaseOrder.GoodReceiveNoteItems.ToList(), purchaseOrder.TaxMethod),
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                            });
                        }
                        return new PagedResult<List<GoodReceiveListLookUpModel>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        _context.DisableTenantFilter = true;
                        var query = _context.GoodReceives
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }
                        


                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }
                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        

                       


                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                           x.RegistrationNo.Contains(searchTerm) || x.Supplier.EnglishName.Contains(searchTerm) || x.Supplier.ArabicName.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                      

                        

                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList = new List<GoodReceiveListLookUpModel>();
                        var usersList = _userManager.Users.ToList();

                        var tempList = query.Select(purchaseOrder => new
                        {
                            Id = purchaseOrder.Id,
                            SupplierId = purchaseOrder.SupplierId,
                            PartiallyReceived = purchaseOrder.PartiallyReceived,
                            CreatedById = EF.Property<string>(purchaseOrder, "CreatedById"),
                            RegistrationNumber = purchaseOrder.RegistrationNo,
                            IsClose = purchaseOrder.IsClose,
                            SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            SupplierName = purchaseOrder.Supplier.CustomerDisplayName,
                            SupplierNameArabic = purchaseOrder.Supplier.CustomerDisplayName,
                            SupplierAdvanceAccountId = purchaseOrder.Supplier.AdvanceAccountId != null ? purchaseOrder.Supplier.AdvanceAccountId : null,
                            NetAmount = purchaseOrder.TotalAmount,
                            Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                            Reason = purchaseOrder.Reason,
                            IsProcessed = purchaseOrder.IsProcessed,
                            InvoiceNo = purchaseOrder.InvoiceNo,
                            Reference = purchaseOrder.Reference,
                            IsDefault = purchaseOrder.Supplier.IsAllowEmail,
                            CustomerEmail = purchaseOrder.Supplier.Email,
                        });

                        var POrders = await _context.PurchaseOrders.AsNoTracking().ToListAsync();

                      

                        foreach (var purchaseOrders in tempList)
                        {
                            purchaseOrderList.Add(new GoodReceiveListLookUpModel
                            {
                                Id = purchaseOrders.Id,
                                SupplierId = purchaseOrders.SupplierId,
                                PartiallyReceived = purchaseOrders.PartiallyReceived,
                                CreatedBy = usersList.FirstOrDefault(x => x.Id == purchaseOrders.CreatedById)?.UserName,
                                RegistrationNumber = purchaseOrders.RegistrationNumber,
                                IsClose = purchaseOrders.IsClose,
                                SupplierName = purchaseOrders.SupplierName,
                                SupplierNameArabic = purchaseOrders.SupplierNameArabic,
                                NetAmount = purchaseOrders.NetAmount,
                                Date = purchaseOrders.Date,
                                Reason = purchaseOrders.Reason,
                                IsProcessed = purchaseOrders.IsProcessed,
                                SupplierQuotationId = purchaseOrders.SupplierQuotationId,
                                PurchaseOrderId = purchaseOrders.PurchaseOrderId,
                                CreatedFrom = purchaseOrders.SupplierQuotationId != null ? POrders.FirstOrDefault(x => x.Id == purchaseOrders.SupplierQuotationId)?.RegistrationNo + " - " + POrders.FirstOrDefault(x => x.Id == purchaseOrders.SupplierQuotationId)?.Date.ToString("dd/MM/yyyy") :
                                purchaseOrders.PurchaseOrderId != null ? POrders.FirstOrDefault(x => x.Id == purchaseOrders.PurchaseOrderId)?.RegistrationNo + " - " + POrders.FirstOrDefault(x => x.Id == purchaseOrders.PurchaseOrderId)?.Date.ToString("dd/MM/yyyy"):null,
                                InvoiceNo = purchaseOrders.InvoiceNo,
                                Reference = purchaseOrders.Reference,
                                IsDefault = purchaseOrders.IsDefault,
                                CustomerEmail = purchaseOrders.CustomerEmail,

                            }); ;
                        }

                        return new PagedResult<List<GoodReceiveListLookUpModel>>
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
