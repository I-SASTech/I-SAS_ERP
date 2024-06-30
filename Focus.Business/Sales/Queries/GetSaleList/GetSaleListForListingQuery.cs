using Focus.Business.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales.Queries.GetSaleList
{
    public class GetSaleListForListingQuery : PagedRequest, IRequest<PagedResult<SaleListModel>>
    {
        public string SearchTerm { get; set; }
        public string SaleHoldType { get; set; }
        public string CustomerType { get; set; }
        public InvoiceType Satus { get; set; }
        public bool IsPartially { get; set; }
        public bool IsDropdown { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public bool IsExpense { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public Guid? UserId { get; set; }
        //public Guid? TerminalId { get; set; }
        public bool IsService { get; set; }
        public bool IsDiscountBeforeVat { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public Guid? BranchId { get; set; }
        public class Handler : IRequestHandler<GetSaleListForListingQuery, PagedResult<SaleListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetSaleListForListingQuery> logger, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }

            public async Task<PagedResult<SaleListModel>> Handle(GetSaleListForListingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _context.DisableTenantFilter = true;
                    var userId = _contextProvider.GetUserId();
                    var companyId = _contextProvider.GetCompanyId();

                    var loginPermissions = await _context.LoginPermissions.AsNoTracking()
                        .AnyAsync(x => x.UserId == userId && x.AllowViewAllData, cancellationToken: cancellationToken);

                    var saleList = _context.Sales.Where(x => x.IsService == request.IsService && EF.Property<Guid>(x, "CompanyId") == companyId);

                    if (request.BranchId != null)
                    {
                        saleList = saleList.Where(x => x.BranchId == request.BranchId);
                    }


                    var userList = await _userManager.Users.AsNoTracking().Select(user => new
                    {
                        user.Id,
                        user.UserName,
                    }).ToListAsync(cancellationToken: cancellationToken);


                    if (!loginPermissions)
                    {
                        saleList = saleList.Where(x => x.CreatedBy == userId);
                    }

                    if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                    {
                        saleList = saleList.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year);

                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        decimal parsedSearchTerm;
                        var isDecimal = decimal.TryParse(searchTerm, out parsedSearchTerm);

                        saleList = saleList.Where(x => x.RegistrationNo.ToLower().Contains(searchTerm) ||
                                                       x.Customer.CustomerDisplayName.ToLower().Contains(searchTerm) ||
                                                       (x.PoNumber != null && x.PoNumber.ToLower().Contains(searchTerm)) ||
                                                       (!string.IsNullOrEmpty(x.PoNumber) && x.PoNumber.ToLower().Contains(searchTerm)) ||
                                                       (isDecimal && x.TotalAmount == parsedSearchTerm)
                        );

                    }


                    if (string.IsNullOrEmpty(request.SearchTerm))
                    {
                        if (request.FromDate != null)
                        {
                            saleList = saleList.Where(x => x.Date.Date >= request.FromDate.Value.Date);
                        }

                        if (request.ToDate != null)
                        {
                            saleList = saleList.Where(x => x.Date.Date <= request.ToDate.Value.Date);
                        }

                        if (request.FromTime != null)
                        {
                            saleList = saleList.Where(x => x.Date.TimeOfDay >= request.FromTime.Value.TimeOfDay);
                        }

                        if (request.ToTime != null)
                        {
                            saleList = saleList.Where(x => x.Date.TimeOfDay <= request.ToTime.Value.TimeOfDay);
                        }
                    }



                    if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                    {
                        saleList = saleList.Where(x => x.CustomerId == request.CustomerId);
                    }

                    if (request.CustomerType == "Credit" || request.CustomerType == "Cash")
                    {
                        saleList = saleList.Where(x => x.Customer.PaymentTerms == request.CustomerType);
                    }

                    //if (request.CustomerType == "Walk-In")
                    //{
                    //    saleList = saleList.Where(x => x.IsCashCustomer);
                    //}

                    if (request.UserId != null)
                    {
                        saleList = saleList.Where(x => x.CreatedBy == request.UserId);
                    }

                    SaleHoldTypes enumValue;

                    if (request.Satus == InvoiceType.Paid || request.Satus == InvoiceType.Credit)
                    {
                        saleList = saleList.Where(x => x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit);
                    }
                    else if (request.SaleHoldType == "Used")
                    {
                        saleList = saleList.Where(x => x.SaleHoldTypes == SaleHoldTypes.Used);
                    }
                    else if (request.SaleHoldType == "All")
                    {
                        saleList = saleList.Where(x => x.SaleHoldTypes != SaleHoldTypes.Default);
                    }
                    else if (Enum.TryParse(request.SaleHoldType, out enumValue))
                    {
                        if (enumValue == SaleHoldTypes.Deleted)
                        {
                            saleList = saleList.Where(x => EF.Property<bool>(x, "IsDeleted"));
                        }
                    }
                    else
                    {
                        if (request.Satus == InvoiceType.Hold)
                        {
                            saleList = request.Satus == InvoiceType.SaleReturn ? saleList.Where(x => x.InvoiceType == request.Satus && x.IsSaleReturnPost == request.IsSaleReturnPost) : saleList.Where(x => x.InvoiceType == request.Satus && x.SaleHoldTypes == SaleHoldTypes.Hold);

                        }
                        else
                        {
                            saleList = request.Satus == InvoiceType.SaleReturn ? saleList.Where(x => x.InvoiceType == request.Satus && x.IsSaleReturnPost == request.IsSaleReturnPost) : saleList.Where(x => x.InvoiceType == request.Satus);

                        }
                    }

                    if (request.Satus == InvoiceType.ProformaInvoice)
                    {
                        saleList = saleList.Where(x => x.InvoiceType == request.Satus && !x.IsProformaInvoice && !x.IsClose);
                    }
                    else if (request.Satus == InvoiceType.PurchaseOrder)
                    {
                        saleList = saleList.Where(x => x.InvoiceType == request.Satus && !x.IsClose);

                    }

                    if (request.IsDropdown)
                    {
                        if (request.IsPartially)
                        {
                            saleList = saleList.Where(x => x.PartiallyInvoices != PartiallyInvoices.Fully);
                        }

                        if (request.ContactId != null && request.ContactId != Guid.Empty)
                        {
                            saleList = saleList.Where(x => x.Customer.AccountId == request.ContactId);
                        }

                        saleList = !request.IsExpense ? saleList.Where(x => !x.IsSaleReturn && x.InvoiceType != InvoiceType.SaleReturn && x.InvoiceType != InvoiceType.Hold) : saleList.Where(x => x.IsCredit && x.IsSaleReturn == false && (x.PartiallyInvoices == PartiallyInvoices.Partially || x.PartiallyInvoices == PartiallyInvoices.UnPaid));


                        var invoiceList = await saleList.Select(invoice => new SaleListLookupModel
                        {
                            Id = invoice.Id,
                            RegistrationNumber = invoice.RegistrationNo,
                            CustomerName = invoice.Customer.CustomerDisplayName,
                            NetAmount = invoice.TotalAmount,
                            Date = invoice.Date
                        }).ToListAsync(cancellationToken: cancellationToken);

                        return new PagedResult<SaleListModel>
                        {
                            Results = new SaleListModel
                            {
                                Sales = invoiceList
                            }
                        };
                    }
                    else
                    {
                        var salesList = await _context.Sales.AsNoTracking().Select(sale => new
                        {
                            sale.Id,
                            sale.RegistrationNo,
                            sale.ProformaId,
                            sale.DeliveryChallanId
                        }).ToListAsync(cancellationToken: cancellationToken);

                        var branches = await _context.Branches.Select(x => new
                        {
                            x.Id,
                            x.Code
                        }).ToListAsync(cancellationToken: cancellationToken);

                        saleList = saleList.OrderByDescending(x => x.RegistrationNo);
                        var count = await saleList.CountAsync(cancellationToken: cancellationToken);
                        var saleQuery = await saleList.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize)
                            .Select(invoice => new
                            {
                                invoice.Id,
                                invoice.CreatedBy,
                                invoice.CustomerId,
                                invoice.ProformaId,
                                invoice.CashCustomerId,
                                invoice.SaleOrderId,
                                invoice.QuotationId,
                                invoice.DeliveryChallanId,
                                invoice.RegistrationNo,
                                invoice.Customer.CustomerDisplayName,
                                invoice.SaleInvoiceId,
                                invoice.IsCredit,
                                invoice.Date,
                                IsDeleted = EF.Property<bool>(invoice, "IsDeleted"),
                                invoice.PartiallyInvoices,
                                invoice.IsSaleReturn,
                                invoice.TotalAmount,
                                invoice.InvoiceNote,
                                invoice.SaleHoldTypes,
                                invoice.IsClose,
                                invoice.BranchId,
                                IsDefault = invoice.Customer.IsAllowEmail,
                                CustomerEmail = invoice.Customer.Email,
                            }).ToListAsync(cancellationToken: cancellationToken);

                        var invoiceList = new List<SaleListLookupModel>();
                        foreach (var invoice in saleQuery)
                        {
                            invoiceList.Add(new SaleListLookupModel()
                            {
                                Id = invoice.Id,
                                CustomerId = invoice.CustomerId,
                                ProformaId = invoice.ProformaId,
                                CashCustomerId = invoice.CashCustomerId,
                                SaleOrderId = invoice.SaleOrderId,
                                QutationId = invoice.QuotationId,
                                DeliveryChallanId = invoice.DeliveryChallanId,
                                RegistrationNumber = invoice.RegistrationNo,
                                CustomerName = invoice.CustomerDisplayName,
                                IsCredit = invoice.IsCredit,
                                Date = invoice.Date,
                                IsDeleted = invoice.IsDeleted,
                                PartiallyInvoices = invoice.PartiallyInvoices,
                                InvoiceNo = invoice.SaleInvoiceId != null ? salesList.FirstOrDefault(x => x.Id == invoice.SaleInvoiceId)?.RegistrationNo : null,
                                IsSaleReturn = invoice.IsSaleReturn,
                                NetAmount = invoice.TotalAmount,
                                InvoiceNote = invoice.InvoiceNote,
                                SaleHoldTypes = invoice.SaleHoldTypes,
                                IsClose = invoice.IsClose,
                                BranchCode = branches.FirstOrDefault(x => x.Id == invoice.BranchId)?.Code,
                                UserName = userList.FirstOrDefault(x => Guid.Parse(x.Id) == invoice.CreatedBy)?.UserName,
                                IsDefault = invoice.IsDefault,
                                CustomerEmail = invoice.CustomerEmail,
                            });
                        }

                        return new PagedResult<SaleListModel>
                        {
                            Results = new SaleListModel
                            {
                                Sales = invoiceList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = count / request.PageSize
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
