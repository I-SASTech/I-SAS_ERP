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
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Users;

namespace Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderList
{
    public class GetPurchaseOrderListQuery : PagedRequest, IRequest<PagedResult<List<PurchaseOrderLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public string DocumentType { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsPartially { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SupplierId { get; set; }
        public Guid? SupplierForFilterId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PoSupplierId { get; set; }
        public Guid? SupperAccountId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string SupplierType { get; set; }
        public Guid? BranchId { get; set; }


        public class Handler : IRequestHandler<GetPurchaseOrderListQuery, PagedResult<List<PurchaseOrderLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetPurchaseOrderListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;
            }

            public async Task<PagedResult<List<PurchaseOrderLookUpModel>>> Handle(GetPurchaseOrderListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.PurchaseOrders
                            .AsNoTracking()
                            .Include(x => x.PurchaseOrderVersions)
                            .Include(x => x.PurchaseOrderItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.PurchaseOrderItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Supplier)
                            .Where(x=>x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose)
                            .AsQueryable();

                        if(request.BranchId != null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }
                        if(request.SupplierId != null)
                        {
                            po = po.Where(x => x.SupplierId == Guid.Parse(request.SupplierId));
                        }
                        if(request.SupperAccountId != null)
                        {
                            po = po.Where(x => x.Supplier.AccountId == request.SupperAccountId);
                        }
                        if(request.IsPartially)
                        {
                            po = po.Where(x => x.PartiallyReceived != PartiallyInvoices.Fully);
                        }

                        if (!string.IsNullOrEmpty(request.DocumentType))
                        {

                            po = po.Where(x => x.DocumentType == request.DocumentType);


                        }
                        else
                        {
                            po = po.Where(x => x.DocumentType == null || x.DocumentType == "");

                        }
                        
                        var purchaseOrderList = new List<PurchaseOrderLookUpModel>();
                        foreach (var purchaseOrder in po)
                        {
                            purchaseOrderList.Add(new PurchaseOrderLookUpModel
                            {
                                Id = purchaseOrder.Id,
                                PartiallyReceived = purchaseOrder.PartiallyReceived,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                IsClose = purchaseOrder.IsClose,
                                DocumentType = purchaseOrder.DocumentType,
                                SupplierName = purchaseOrder.Supplier?.CustomerDisplayName,
                                SupplierNameArabic = purchaseOrder.Supplier?.CustomerDisplayName,
                                SupplierAdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
                                NetAmount = purchaseOrder.TotalAmount,
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                Version = purchaseOrder.PurchaseOrderVersions?.LastOrDefault()?.Version

                            });
                        }
                        return new PagedResult<List<PurchaseOrderLookUpModel>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        _context.DisableTenantFilter = true;

                        var query = _context.PurchaseOrders
                            .AsNoTracking()
                            .Include(x=>x.PurchaseOrderVersions)
                            .Include(x=>x.Supplier)
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
                        if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                        {
                            query = query.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year);

                        }
                        if (request.SupplierForFilterId != null)
                        {
                            query = query.Where(x => x.SupplierId == request.SupplierForFilterId);
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
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                           x.RegistrationNo.Contains(searchTerm) || x.Supplier.EnglishName.Contains(searchTerm) || x.Supplier.ArabicName.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                        if (!string.IsNullOrEmpty(request.DocumentType))
                        {

                            query = query.Where(x => x.DocumentType == request.DocumentType);


                        }
                        else
                        {
                            query = query.Where(x => x.DocumentType ==null || x.DocumentType == "");

                        }
                        if (request.TotalAmount != null && request.TotalAmount != 0)
                        {
                            query = query.Where(x => x.TotalAmount.ToString().Contains(request.TotalAmount.ToString()));
                        }
                        
                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var purchaseOrderList = new List<PurchaseOrderLookUpModel>();
                        var usersList = _userManager.Users.ToList();

                        var tempList = query.Select(purchaseOrder => new
                        {
                            Id = purchaseOrder.Id,
                            PartiallyReceived = purchaseOrder.PartiallyReceived,
                            CreatedById = EF.Property<string>(purchaseOrder, "CreatedById"),
                            RegistrationNumber = purchaseOrder.RegistrationNo,
                            IsClose = purchaseOrder.IsClose,
                            DocumentType = purchaseOrder.DocumentType,
                            SupplierQuotationId = purchaseOrder.SupplierQuotationId,
                            SupplierName = purchaseOrder.Supplier.CustomerDisplayName,
                            SupplierNameArabic = purchaseOrder.Supplier.CustomerDisplayName,
                            SupplierAdvanceAccountId = purchaseOrder.Supplier.AdvanceAccountId != null ? purchaseOrder.Supplier.AdvanceAccountId : null,
                            NetAmount = purchaseOrder.TotalAmount,
                            SupplierId = purchaseOrder.SupplierId,
                            Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                            Reason = purchaseOrder.Reason,
                            IsProcessed = purchaseOrder.IsProcessed,
                            InvoiceNo = purchaseOrder.InvoiceNo,
                            Reference = purchaseOrder.Reference,
                            Version = purchaseOrder.PurchaseOrderVersions.Count > 0 ? purchaseOrder.PurchaseOrderVersions.OrderBy(x => x.Version).LastOrDefault().Version : null,
                            IsDefault = purchaseOrder.Supplier.IsAllowEmail,
                            CustomerEmail = purchaseOrder.Supplier.Email,
                        });

                        var POrders = await _context.PurchaseOrders.AsNoTracking().Where(x => x.DocumentType == "SupplierQuotation").ToListAsync();

                       
                        foreach (var purchaseOrders in tempList)
                        {
                            purchaseOrderList.Add(new PurchaseOrderLookUpModel
                            {
                                Id = purchaseOrders.Id,
                                PartiallyReceived = purchaseOrders.PartiallyReceived,
                                CreatedBy = usersList.FirstOrDefault(x => x.Id == purchaseOrders.CreatedById)?.UserName,
                                RegistrationNumber = purchaseOrders.RegistrationNumber,
                                IsClose = purchaseOrders.IsClose,
                                DocumentType = purchaseOrders.DocumentType,
                                SupplierName = purchaseOrders.SupplierName,
                                SupplierNameArabic = purchaseOrders.SupplierNameArabic,
                                SupplierAdvanceAccountId = purchaseOrders.SupplierAdvanceAccountId,
                                NetAmount = purchaseOrders.NetAmount,
                                Date = purchaseOrders.Date,
                                SupplierId = purchaseOrders.SupplierId,
                                SupplierQuotationId = purchaseOrders.SupplierQuotationId,
                                Reason=  purchaseOrders.Reason ,
                                IsProcessed = purchaseOrders.IsProcessed,
                                CreatedFrom = purchaseOrders.SupplierQuotationId != null ? POrders.FirstOrDefault(x => x.Id == purchaseOrders.SupplierQuotationId)?.RegistrationNo + " - " + POrders.FirstOrDefault(x => x.Id == purchaseOrders.SupplierQuotationId)?.Date.ToString("dd/MM/yyyy") : null,
                                InvoiceNo = purchaseOrders.InvoiceNo,
                                Reference = purchaseOrders.Reference,
                                Version = purchaseOrders.Version,
                                IsDefault = purchaseOrders.IsDefault,
                                CustomerEmail = purchaseOrders.CustomerEmail,
                            });;
                        }

                        return new PagedResult<List<PurchaseOrderLookUpModel>>
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
