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
using Focus.Domain.Interface;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Entities;
using DocumentFormat.OpenXml.Bibliography;

namespace Focus.Business.SaleOrders.Queries.GetSaleOrderList
{
    public class GetSaleOrderListQuery : PagedRequest, IRequest<PagedResult<List<SaleOrderLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsForBom { get; set; }
        public bool IsQuotation { get; set; }
        public bool IsService { get; set; }
        public bool IsPartially { get; set; }
        public bool IsSaleOrderTracking { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? ContactAccountId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? UserId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetSaleOrderListQuery, PagedResult<List<SaleOrderLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetSaleOrderListQuery> logger, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
                _userManager = userManager;

            }

            public async Task<PagedResult<List<SaleOrderLookUpModel>>> Handle(GetSaleOrderListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var allowViewAllData = await _context.LoginPermissions.AsNoTracking()
                        .Where(x => x.UserId == _contextProvider.GetUserId())
                        .Select(x => x.AllowViewAllData)
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (request.IsDropDown)
                    {
                        if(request.IsForBom)
                        {
                            var po = await _context.SaleOrders
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose && x.IsQuotation == request.IsQuotation && !x.IsUsedForBom)
                            .AsNoTracking()
                            .Select(x => new
                            {
                                x.Id,
                                x.BranchId,
                                x.CustomerId,
                                x.Customer.AccountId,
                                x.Customer.AdvanceAccountId,
                                x.Customer.CustomerDisplayName,
                                x.PartiallyInvoices,
                                x.CreatedBy,
                                x.IsSaleOrderTracking,
                                x.IsClose,
                                x.IsProcessed,
                                x.RegistrationNo,
                                x.TotalAmount,
                                x.IsQuotation,
                                x.Date,
                                x.InvoiceNote,
                                IsDefault = x.Customer.IsAllowEmail
                                //version = x.SaleOrderVersions.MaxBy(y => y.Version).Version
                            }).ToListAsync(cancellationToken: cancellationToken);


                            if (request.BranchId != null)
                            {
                                po = po.Where(x => x.BranchId == request.BranchId).ToList();
                            }

                            if (request.CustomerId != null)
                            {
                                po = po.Where(x => x.CustomerId == request.CustomerId).ToList();
                            }
                            if (request.ContactAccountId != null)
                            {
                                po = po.Where(x => x.AccountId == request.ContactAccountId).ToList();
                            }
                            if (request.IsPartially)
                            {
                                po = po.Where(x => x.PartiallyInvoices != PartiallyInvoices.Fully).ToList();
                            }

                            if (!allowViewAllData)
                            {
                                po = po.Where(x => x.CreatedBy == _contextProvider.GetUserId()).ToList();
                            }
                            if (request.IsSaleOrderTracking)
                            {
                                po = po.Where(x => x.IsSaleOrderTracking == request.IsSaleOrderTracking).ToList();
                            }
                            var userList = _userManager.Users.ToList();

                            var saleOrderList = new List<SaleOrderLookUpModel>();
                            foreach (var saleOrder in po)
                            {
                                string fullName = userList.FirstOrDefault(x => x.Id == saleOrder.CreatedBy.ToString())?.FirstName;

                                //var sOrder = SaleOrderLookUpModel.Create(saleOrder,);
                                saleOrderList.Add(new SaleOrderLookUpModel
                                {
                                    Id = saleOrder.Id,
                                    IsClose = saleOrder.IsClose,
                                    IsProcessed = saleOrder.IsProcessed,
                                    PartiallyInvoices = saleOrder.PartiallyInvoices,
                                    CustomerId = saleOrder.CustomerId,
                                    CustomerAccountId = saleOrder.AccountId,
                                    CustomerAdvanceAccountId = saleOrder.AdvanceAccountId,
                                    RegistrationNumber = saleOrder.RegistrationNo,
                                    CustomerName = saleOrder.CustomerDisplayName,
                                    NetAmount = saleOrder.TotalAmount,
                                    IsQuotation = saleOrder.IsQuotation,
                                    Date = saleOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                    InvoiceNote = saleOrder.InvoiceNote,
                                    CreatedBy = fullName,
                                    IsDefault = saleOrder.IsDefault,
                                    //Version = saleOrder.version,
                                });
                            }
                            return new PagedResult<List<SaleOrderLookUpModel>>
                            {
                                Results = saleOrderList
                            };
                        }
                        else
                        {
                            var po = await _context.SaleOrders
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose && x.IsQuotation == request.IsQuotation && x.IsService == request.IsService)
                            .AsNoTracking()
                            .Select(x => new
                            {
                                x.Id,
                                x.BranchId,
                                x.CustomerId,
                                x.Customer.AccountId,
                                x.Customer.AdvanceAccountId,
                                x.Customer.CustomerDisplayName,
                                x.PartiallyInvoices,
                                x.CreatedBy,
                                x.IsSaleOrderTracking,
                                x.IsClose,
                                x.IsProcessed,
                                x.RegistrationNo,
                                x.TotalAmount,
                                x.IsQuotation,
                                x.Date,
                                x.InvoiceNote,
                                IsDefault = x.Customer.IsAllowEmail
                                //version = x.SaleOrderVersions.MaxBy(y => y.Version).Version
                            }).ToListAsync(cancellationToken: cancellationToken);


                            if (request.BranchId != null)
                            {
                                po = po.Where(x => x.BranchId == request.BranchId).ToList();
                            }

                            if (request.CustomerId != null)
                            {
                                po = po.Where(x => x.CustomerId == request.CustomerId).ToList();
                            }
                            if (request.ContactAccountId != null)
                            {
                                po = po.Where(x => x.AccountId == request.ContactAccountId).ToList();
                            }
                            if (request.IsPartially)
                            {
                                po = po.Where(x => x.PartiallyInvoices != PartiallyInvoices.Fully).ToList();
                            }

                            if (!allowViewAllData)
                            {
                                po = po.Where(x => x.CreatedBy == _contextProvider.GetUserId()).ToList();
                            }
                            if (request.IsSaleOrderTracking)
                            {
                                po = po.Where(x => x.IsSaleOrderTracking == request.IsSaleOrderTracking).ToList();
                            }
                            var userList = _userManager.Users.ToList();

                            var saleOrderList = new List<SaleOrderLookUpModel>();
                            foreach (var saleOrder in po)
                            {
                                string fullName = userList.FirstOrDefault(x => x.Id == saleOrder.CreatedBy.ToString())?.FirstName;

                                //var sOrder = SaleOrderLookUpModel.Create(saleOrder,);
                                saleOrderList.Add(new SaleOrderLookUpModel
                                {
                                    Id = saleOrder.Id,
                                    IsClose = saleOrder.IsClose,
                                    IsProcessed = saleOrder.IsProcessed,
                                    PartiallyInvoices = saleOrder.PartiallyInvoices,
                                    CustomerId = saleOrder.CustomerId,
                                    CustomerAccountId = saleOrder.AccountId,
                                    CustomerAdvanceAccountId = saleOrder.AdvanceAccountId,
                                    RegistrationNumber = saleOrder.RegistrationNo,
                                    CustomerName = saleOrder.CustomerDisplayName,
                                    NetAmount = saleOrder.TotalAmount,
                                    IsQuotation = saleOrder.IsQuotation,
                                    Date = saleOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                    InvoiceNote = saleOrder.InvoiceNote,
                                    CreatedBy = fullName,
                                    IsDefault = saleOrder.IsDefault
                                    //Version = saleOrder.version,
                                });
                            }
                            return new PagedResult<List<SaleOrderLookUpModel>>
                            {
                                Results = saleOrderList
                            };
                        }
                    }
                    else
                    {
                        var query = await _context.SaleOrders
                            .AsNoTracking()
                            .Include(x => x.SaleOrderVersions)
                            .Include(x => x.SaleOrderItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.SaleOrderItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Customer)
                            .Where(x => x.IsService == request.IsService && x.IsSaleOrderTracking == request.IsSaleOrderTracking)
                            .ToListAsync();

                        var salesRecord = await _context.Sales.AsNoTracking().Select(x => new
                        {
                            SalerOrderId = x.SaleOrderId,
                            QuotationId = x.QuotationId,
                            DeliveryChallanId = x.DeliveryChallanId,
                            PurchaseOrderId = x.PurchaseOrderId,
                            ProformaId = x.ProformaId,
                        }).ToListAsync(cancellationToken: cancellationToken);

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId).ToList();
                        }

                        if (!allowViewAllData)
                        {
                            query = query.Where(x => x.CreatedBy == _contextProvider.GetUserId()).ToList(); ;
                        }

                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status && x.IsQuotation == request.IsQuotation).ToList(); ;
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            var isDecimal = decimal.TryParse(searchTerm, out var parsedSearchTerm);


                            query = query.Where(x =>
                                x.RegistrationNo.ToLower().Contains(searchTerm) ||
                                (x.Customer.CustomerDisplayName != null && x.Customer.CustomerDisplayName.ToLower().Contains(searchTerm)) ||
                                (x.Customer.EnglishName != null && x.Customer.EnglishName.ToLower().Contains(searchTerm)) ||
                                (x.Customer.ArabicName != null && x.Customer.ArabicName.ToLower().Contains(searchTerm)) ||
                                (x.Customer.Code != null && x.Customer.Code.ToLower().Contains(searchTerm)) ||
                                x.BarCode.ToLower().Contains(searchTerm) ||
                                x.Description.ToLower().Contains(searchTerm) ||
                                x.ReferredBy.ToLower().Contains(searchTerm) ||
                                x.PoNumber.ToLower().Contains(searchTerm) ||
                                x.Customer.ContactNo1.ToLower().Contains(searchTerm) ||
                                (!string.IsNullOrEmpty(x.Refrence) && x.Refrence.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.ClientPurchaseNo) && x.ClientPurchaseNo.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.OneTimeDescription) && x.OneTimeDescription.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.CustomerInquiry) && x.CustomerInquiry.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.PoNumber) && x.PoNumber.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.ReferredBy) && x.ReferredBy.ToLower().Contains(searchTerm.ToLower())) ||
                                (!string.IsNullOrEmpty(x.RefrenceNo) && x.RefrenceNo.ToLower().Contains(searchTerm.ToLower())) ||
                                (isDecimal && x.TotalAmount == parsedSearchTerm)).ToList();
                        }
                        if (request.CustomerId != null)
                        {
                            query = query.Where(x => x.CustomerId == request.CustomerId).ToList();
                        }
                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();
                        }
                        if (request.UserId != null)
                        {
                            query = query.Where(x => x.CreatedBy == request.UserId).ToList();
                        }
                        if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                        {
                            query = query.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year).ToList();

                        }

                        query = query.OrderByDescending(x => x.RegistrationNo).ToList();
                        var count = query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();
                        var userList = _userManager.Users.ToList();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                        var saleOrderList = new List<SaleOrderLookUpModel>();
                        foreach (var saleOrder in query)
                        {
                            string fullName = userList.FirstOrDefault(x => x.Id == saleOrder.CreatedBy.ToString())?.FirstName;

                            //var sOrder = SaleOrderLookUpModel.Create(saleOrder,);
                            saleOrderList.Add(new SaleOrderLookUpModel
                            {
                                Id = saleOrder.Id,
                                IsProcessed = saleOrder.IsProcessed,
                                IsClose = saleOrder.IsClose,
                                CustomerId = saleOrder.CustomerId,
                                PartiallyInvoices = saleOrder.PartiallyInvoices,

                                CustomerAccountId = saleOrder.Customer.AccountId,
                                CustomerAdvanceAccountId = saleOrder.Customer.AdvanceAccountId,
                                RegistrationNumber = saleOrder.RegistrationNo,
                                CustomerName = saleOrder.Customer.CustomerDisplayName,
                                NetAmount = saleOrder.TotalAmount,
                                IsQuotation = saleOrder.IsQuotation,
                                Date = saleOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                InvoiceNote = saleOrder.InvoiceNote,
                                CreatedBy = fullName,
                                Version = saleOrder.SaleOrderVersions.MaxBy(x => x.Version)?.Version,
                                QuotationId = saleOrder.QuotationId,
                                SaleOrderId = saleOrder.SaleOrderId,
                                ProformaId = saleOrder.ProformaId,
                                Reason = saleOrder.Reason,
                                DeliveryChallanId = saleOrder.DeliveryChallanId,
                                BranchCode = branches.FirstOrDefault(x => x.Id == saleOrder.BranchId)?.Code,
                                EditQuotationId = salesRecord.FirstOrDefault(x => x.QuotationId == saleOrder.Id)?.QuotationId,
                                EditSaleOrderId = salesRecord.FirstOrDefault(x => x.SalerOrderId == saleOrder.Id)?.SalerOrderId,
                                IsDefault = saleOrder.Customer.IsAllowEmail,
                                CustomerEmail = saleOrder.Customer.Email,
                            });
                        }

                        return new PagedResult<List<SaleOrderLookUpModel>>
                        {
                            Results = saleOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = saleOrderList.Count / request.PageSize
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
