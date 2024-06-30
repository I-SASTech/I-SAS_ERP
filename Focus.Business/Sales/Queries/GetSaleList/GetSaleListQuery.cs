using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Domain.Enum;


namespace Focus.Business.Sales.Queries.GetSaleList
{
    public class GetSaleListQuery : PagedRequest, IRequest<PagedResult<SaleListModel>>
    {

        public string SearchTerm { get; set; }
        public string SaleHoldType { get; set; }
        public string CustomerType { get; set; }
        public DateTime SearchDate { get; set; }
        public InvoiceType Satus { get; set; }
        public bool IsSaleDashboard { get; set; }
        public bool IsPartially { get; set; }
        public bool IsSaleFilter { get; set; }
        public bool IsDropdown { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public bool IsExpense { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ContactAccountId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TerminalId { get; set; }
        public bool IsService { get; set; }
        public bool IsDiscountBeforeVat { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetSaleListQuery, PagedResult<SaleListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetSaleListQuery> logger, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }

            public async Task<PagedResult<SaleListModel>> Handle(GetSaleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _context.DisableTenantFilter = true;
                    var userId = _contextProvider.GetUserId();
                    var loginPermissions = await _context.LoginPermissions.Select(x => new
                    {
                        x.UserId,
                        x.AllowViewAllData,
                    }).FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);

                    var saleList = await _context.Sales
                        .Where(x => x.IsService == request.IsService)
                        .Select(invoice => new
                        {
                            invoice.Id,
                            invoice.CreatedBy,
                            invoice.SaleOrderId,
                            invoice.QuotationId,
                            invoice.DeliveryChallanId,
                            invoice.CustomerId,
                            invoice.CashCustomerId,
                            RegistrationNumber = invoice.RegistrationNo,
                            CustomerName = invoice.Customer.CustomerDisplayName,
                            CustomerCode = invoice.Customer.Code,
                            CustomerNameEn = invoice.Customer.EnglishName,
                            CustomerNameAr = invoice.Customer.ArabicName,
                            //invoice.Customer.IsCashCustomer,
                            IsCashCustomer = true,

                            Phone = invoice.Customer.ContactNo1,
                            PaymentTerms = invoice.Customer.PaymentTerms != null ? invoice.Customer.PaymentTerms : "",
                            ContactAccountId = invoice.CustomerId == null ? Guid.Empty : invoice.Customer.AccountId ?? Guid.Empty,
                            invoice.IsCredit,
                            invoice.Date,
                            invoice.TotalAmount,
                            invoice.Description,
                            invoice.ReferredBy,
                            invoice.PoNumber,
                            invoice.TotalWithOutAmount,
                            invoice.VatAmount,
                            invoice.RefrenceNo,
                            invoice.InvoiceType,
                            invoice.PartiallyInvoices,
                            invoice.BarCode,
                            invoice.IsSaleReturn,
                            invoice.IsSaleReturnPost,
                            invoice.IsProformaInvoice,
                            invoice.MarkAsCompleted,
                            invoice.IsMsgSended,
                            invoice.IsSendMsg,
                            invoice.DueDate,
                            invoice.IsService,
                            invoice.IsDiscountOnTransaction,
                            invoice.SaleInvoiceId,
                            invoice.TaxMethod,
                            invoice.IsFixed,
                            invoice.IsBeforeTax,
                            invoice.TransactionLevelDiscount,
                            invoice.Discount,
                            invoice.TaxRateId,
                            invoice.ProformaId,
                            invoice.UnRegisteredVAtAmount,
                            invoice.InvoiceNote,
                            invoice.IsClose,
                            invoice.IsProcessed,
                            invoice.Reason,
                            IsDeleted = EF.Property<bool>(invoice, "IsDeleted"),
                            CompanyId = EF.Property<Guid>(invoice, "CompanyId"),
                            TerminalId = EF.Property<Guid?>(invoice, "TerminalId"),
                            invoice.SaleHoldTypes,
                            invoice.BranchId,
                            IsDefault = invoice.Customer.IsAllowEmail,
                            SalePayments = invoice.SalePayments.Select(x => new SalePayment()
                            {
                                Name = x.Name,
                                Received = x.Received,
                            }).ToList()
                        }).ToListAsync(cancellationToken: cancellationToken);


                    if (request.BranchId != null)
                    {
                        saleList = saleList.Where(x => x.BranchId == request.BranchId).ToList();
                    }


                    //var terminalList =  _context.Terminals.AsNoTracking();
                    var userList = await (from user in _userManager.Users
                                          select new
                                          {
                                              user.Id,
                                              user.UserName,
                                          }).ToListAsync(cancellationToken: cancellationToken);

                    var salesList = await (from sale in _context.Sales
                                           select new
                                           {
                                               sale.Id,
                                               sale.RegistrationNo,
                                               sale.PurchaseOrderId,
                                               sale.ProformaId,
                                               sale.DeliveryChallanId
                                           }).ToListAsync(cancellationToken: cancellationToken);

                    if (loginPermissions != null && !loginPermissions.AllowViewAllData)
                    {
                        saleList = saleList.Where(x => x.CreatedBy == _contextProvider.GetUserId()).ToList();
                    }
                    if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                    {
                        saleList = saleList.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year).ToList();

                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        decimal parsedSearchTerm;
                        var isDecimal = decimal.TryParse(searchTerm, out parsedSearchTerm);

                        saleList = saleList.Where(x =>
                        (x.RegistrationNumber != null && x.RegistrationNumber.ToLower().Contains(searchTerm)) ||
                        (x.CustomerName != null && x.CustomerName.ToLower().Contains(searchTerm)) ||
                        (x.BarCode != null && x.BarCode.ToLower().Contains(searchTerm)) ||
                        (x.Description != null && x.Description.ToLower().Contains(searchTerm)) ||
                        (x.ReferredBy != null && x.ReferredBy.ToLower().Contains(searchTerm)) ||
                        (x.PoNumber != null && x.PoNumber.ToLower().Contains(searchTerm)) ||
                        (x.CustomerCode != null && x.CustomerCode.ToLower().Contains(searchTerm)) ||
                        (x.CustomerNameEn != null && x.CustomerNameEn.ToLower().Contains(searchTerm)) ||
                        (x.CustomerNameAr != null && x.CustomerNameAr.ToLower().Contains(searchTerm)) ||
                        (!string.IsNullOrEmpty(x.PoNumber) && x.PoNumber.ToLower().Contains(searchTerm)) ||
                        (!string.IsNullOrEmpty(x.ReferredBy) && x.ReferredBy.ToLower().Contains(searchTerm)) ||
                        (!string.IsNullOrEmpty(x.RefrenceNo) && x.RefrenceNo.ToLower().Contains(searchTerm)) ||
                        (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(searchTerm)) ||
                        (x.Phone != null && x.Phone.ToLower().Contains(searchTerm)) ||
                        (isDecimal && x.TotalAmount == parsedSearchTerm)
                        ).ToList();

                    }


                    if (string.IsNullOrEmpty(request.SearchTerm))
                    {
                        if (request.FromDate != null)
                        {
                            saleList = saleList.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            saleList = saleList.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();
                        }

                        if (request.FromTime != null)
                        {
                            saleList = saleList.Where(x => x.Date.TimeOfDay >= request.FromTime.Value.TimeOfDay).ToList();
                        }

                        if (request.ToTime != null)
                        {
                            saleList = saleList.Where(x => x.Date.TimeOfDay <= request.ToTime.Value.TimeOfDay).ToList();
                        }
                    }

                    SaleHoldTypes enumValue;

                    if (request.IsSaleDashboard)
                    {
                        saleList = saleList.Where(x => x.Date.Date == DateTime.UtcNow.Date).ToList();
                    }

                    if (request.CustomerId != null && request.CustomerId != Guid.Empty)
                    {
                        saleList = saleList.Where(x => x.CustomerId == request.CustomerId).ToList();
                    }
                    if (request.CustomerType == "Credit")
                    {
                        saleList = saleList.Where(x => x.PaymentTerms == "Credit").ToList();
                    }
                    if (request.CustomerType == "Cash")
                    {
                        saleList = saleList.Where(x => x.PaymentTerms == "Cash").ToList();
                    }
                    if (request.CustomerType == "Walk-In")
                    {
                        saleList = saleList.Where(x => x.IsCashCustomer).ToList();
                    }
                    if (request.UserId != null)
                    {
                        saleList = saleList.Where(x => x.CreatedBy == request.UserId).ToList();
                    }
                    //if (request.TerminalId != null)
                    //{
                    //    saleList = saleList.Where(x => x.CounterId == request.TerminalId);
                    //}

                    var invoiceList = new List<SaleListLookupModel>();
                    if (request.IsDropdown && request.ContactId != null)
                    {
                        if (request.Satus == InvoiceType.Paid || request.Satus == InvoiceType.Credit)
                        {
                            saleList = saleList.Where(x => x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit).ToList();
                        }
                        else if (request.Satus == InvoiceType.ProformaInvoice)
                        {
                            saleList = saleList.Where(x => x.InvoiceType == InvoiceType.ProformaInvoice).ToList();
                        }

                        var saleList1 = saleList.Where(x => x.PartiallyInvoices != PartiallyInvoices.Fully).ToList();

                        if (request.IsPartially)
                        {
                            saleList = saleList.Where(x => x.PartiallyInvoices != PartiallyInvoices.Fully).ToList();
                        }

                        foreach (var invoice in saleList1)
                        {
                            var saleItems = new List<SaleItemLookupModel>();

                            invoiceList.Add(new SaleListLookupModel()
                            {

                                Id = invoice.Id,
                                CustomerId = invoice.CustomerId,
                                CashCustomerId = invoice.CashCustomerId,
                                SaleOrderId = invoice.SaleOrderId,
                                RegistrationNumber = invoice.RegistrationNumber,
                                CustomerName = invoice.CustomerName,
                                IsCredit = invoice.IsCredit,
                                Date = invoice.Date,
                                RefrenceNo = invoice.RefrenceNo,
                                InvoiceType = invoice.InvoiceType,
                                PartiallyInvoices = invoice.PartiallyInvoices,
                                BarCode = invoice.BarCode,
                                InvoiceNo = invoice.SaleInvoiceId != null ? salesList.FirstOrDefault(x => x.Id == invoice.SaleInvoiceId)?.RegistrationNo : null,
                                IsSaleReturn = invoice.IsSaleReturn,
                                IsSaleReturnPost = invoice.IsSaleReturnPost,
                                ContactAccountId = invoice.ContactAccountId,
                                IsMsgSended = invoice.IsMsgSended,
                                IsSendMsg = invoice.IsSendMsg,
                                NetAmount = invoice.TotalAmount,
                                InvoiceNote = invoice.InvoiceNote,
                                ProformaId = invoice.ProformaId,
                                DueAmount = invoice.SalePayments.Where(x => x.Name == "Credit").AsEnumerable().Sum(x => x.Received),
                                ReceivedAmount = invoice.SalePayments.Where(x => x.Name != "Credit").AsEnumerable().Sum(x => x.Received),
                                DueDate = invoice.DueDate,
                                IsService = invoice.IsService,
                                IsProformaInvoice = invoice.IsProformaInvoice,
                                MarkAsCompleted = invoice.MarkAsCompleted,
                                IsClose = invoice.IsClose,
                                IsProcessed = invoice.IsProcessed,
                                Reason = invoice.Reason,
                                IsDefault = invoice.IsDefault,

                                //CounterName = terminalList.Where(x => x.Id == EF.Property<Guid>(invoice, "CounterId")).Select(x => x.Code).FirstOrDefault(),
                                UserName = userList.FirstOrDefault(x => x.Id == invoice.CreatedBy.ToString())?.UserName,

                            });

                        }


                        //if (request.Month != 0 && request.Month != null && request.Year != 0 && request.Year != null)
                        //{
                        //    invoiceList = invoiceList.Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year).ToList();

                        //}

                        //if (request.FromDate != null)
                        //{
                        //    invoiceList = invoiceList.Where(x => x.Date.Date >= request.FromDate).ToList();
                        //}

                        //if (request.ToDate != null)
                        //{
                        //    invoiceList = invoiceList.Where(x => x.Date.Date <= request.ToDate).ToList();
                        //}


                        if (request.ContactId != null && request.ContactId != Guid.Empty)
                        {
                            invoiceList = invoiceList.Where(x => x.ContactAccountId == request.ContactId).ToList();
                        }

                        return new PagedResult<SaleListModel>
                        {
                            Results = new SaleListModel
                            {
                                Sales = invoiceList
                            }
                        };
                    }

                    if (request.Satus == InvoiceType.Paid || request.Satus == InvoiceType.Credit)
                    {
                        saleList = saleList.Where(x => x.InvoiceType == InvoiceType.Paid || x.InvoiceType == InvoiceType.Credit).ToList();
                    }
                    else if (request.SaleHoldType == "Used")
                    {
                        saleList = saleList.Where(x => x.SaleHoldTypes == SaleHoldTypes.Used && x.CompanyId == _contextProvider.GetCompanyId()).ToList();
                    }
                    else if (request.SaleHoldType == "All")
                    {
                        saleList = saleList.Where(x => x.SaleHoldTypes != SaleHoldTypes.Default && x.CompanyId == _contextProvider.GetCompanyId()).ToList();
                    }
                    else if (Enum.TryParse(request.SaleHoldType, out enumValue))
                    {
                        if (enumValue == SaleHoldTypes.Deleted)
                        {
                            saleList = saleList.Where(x => x.IsDeleted && x.CompanyId == _contextProvider.GetCompanyId()).ToList();
                        }
                    }
                    else
                    {
                        if (request.Satus == InvoiceType.Hold)
                        {
                            saleList = request.Satus == InvoiceType.SaleReturn ? saleList.Where(x => x.InvoiceType == request.Satus && x.IsSaleReturnPost == request.IsSaleReturnPost).ToList() : saleList.Where(x => x.InvoiceType == request.Satus && x.SaleHoldTypes == SaleHoldTypes.Hold && x.CompanyId == _contextProvider.GetCompanyId()).ToList();

                        }
                        else
                        {
                            saleList = request.Satus == InvoiceType.SaleReturn ? saleList.Where(x => x.InvoiceType == request.Satus && x.IsSaleReturnPost == request.IsSaleReturnPost).ToList() : saleList.Where(x => x.InvoiceType == request.Satus && x.CompanyId == _contextProvider.GetCompanyId()).ToList();

                        }
                    }


                    if (request.IsDropdown)
                    {
                        if (request.Satus == InvoiceType.ProformaInvoice)
                        {
                            saleList = saleList.Where(x => x.InvoiceType == request.Satus && !x.IsProformaInvoice && !x.IsClose).ToList();

                        }
                        else if (request.Satus == InvoiceType.PurchaseOrder)
                        {
                            saleList = saleList.Where(x => x.InvoiceType == request.Satus && !x.IsClose).ToList();

                        }

                        foreach (var invoice in saleList)
                        {
                            var saleItems = new List<SaleItemLookupModel>();

                            invoiceList.Add(new SaleListLookupModel()
                            {

                                Id = invoice.Id,
                                CustomerId = invoice.CustomerId,
                                SaleOrderId = invoice.SaleOrderId,
                                RegistrationNumber = invoice.RegistrationNumber,
                                CustomerName = invoice.CustomerName,
                                IsCredit = invoice.IsCredit,
                                Date = invoice.Date,
                                RefrenceNo = invoice.RefrenceNo,
                                InvoiceType = invoice.InvoiceType,
                                PartiallyInvoices = invoice.PartiallyInvoices,
                                BarCode = invoice.BarCode,
                                InvoiceNo = invoice.SaleInvoiceId != null ? salesList.FirstOrDefault(x => x.Id == invoice.SaleInvoiceId).RegistrationNo : null,
                                IsSaleReturn = invoice.IsSaleReturn,
                                IsSaleReturnPost = invoice.IsSaleReturnPost,
                                ContactAccountId = invoice.ContactAccountId,
                                IsDeleted = invoice.IsDeleted,
                                NetAmount = invoice.TotalAmount,
                                InvoiceNote = invoice.InvoiceNote,
                                ProformaId = invoice.ProformaId,
                                IsClose = invoice.IsClose,
                                IsProcessed = invoice.IsProcessed,
                                Reason = invoice.Reason,
                                IsDefault = invoice.IsDefault,
                                //CounterName = terminalList.OrderBy(x => x.Id).FirstOrDefault(x => x.Id == EF.Property<Guid>(invoice, "CounterId"))?.Code,

                                UserName = userList.FirstOrDefault(x => x.Id == invoice.CreatedBy.ToString())?.UserName,

                                CreatedUser = userList.FirstOrDefault(x => x.Id == invoice.CreatedBy.ToString())?.UserName,

                                //CounterId = terminalList.OrderBy(x => x.Id).FirstOrDefault(x => x.Id == EF.Property<Guid>(invoice, "CounterId")).Id,

                                UserId = invoice.CreatedBy,

                            });


                        }

                        invoiceList = !request.IsExpense ? invoiceList.Where(x => !x.IsSaleReturn && x.InvoiceType != InvoiceType.SaleReturn && x.InvoiceType != InvoiceType.Hold).ToList() : invoiceList.Where(x => x.IsCredit && x.IsSaleReturn == false && (x.PartiallyInvoices == PartiallyInvoices.Partially || x.PartiallyInvoices == PartiallyInvoices.UnPaid)).ToList();


                        if (request.ContactId != null)
                        {
                            invoiceList = invoiceList.Where(x => x.ContactAccountId == request.ContactId).ToList();
                        }

                        return new PagedResult<SaleListModel>
                        {
                            Results = new SaleListModel
                            {
                                Sales = invoiceList
                            }
                        };
                    }









                    saleList = saleList.OrderByDescending(x => x.RegistrationNumber).ToList();
                    var count = saleList.Count();
                    saleList = saleList.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();
                    //var taxRateId = _context.CompanyAccountSetups.FirstOrDefault()?.TaxRateId;
                    //var taxRate = _context.TaxRates.FirstOrDefault(x => x.Id == taxRateId);

                    var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                    foreach (var invoice in saleList)
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
                            RegistrationNumber = invoice.RegistrationNumber,
                            CustomerName = invoice.CustomerName,
                            IsCredit = invoice.IsCredit,
                            Date = invoice.Date,
                            RefrenceNo = invoice.RefrenceNo,
                            InvoiceType = invoice.InvoiceType,
                            IsDeleted = invoice.IsDeleted,
                            PartiallyInvoices = invoice.PartiallyInvoices,
                            BarCode = invoice.BarCode,
                            InvoiceNo = invoice.SaleInvoiceId != null ? salesList.FirstOrDefault(x => x.Id == invoice.SaleInvoiceId)?.RegistrationNo : null,
                            IsSaleReturn = invoice.IsSaleReturn,
                            IsSaleReturnPost = invoice.IsSaleReturnPost,
                            ContactAccountId = invoice.ContactAccountId,
                            IsMsgSended = invoice.IsMsgSended,
                            IsSendMsg = invoice.IsSendMsg,
                            NetAmount = invoice.TotalAmount,
                            DueAmount = invoice.SalePayments.Where(x => x.Name == "Credit").AsEnumerable().Sum(x => x.Received),
                            ReceivedAmount = invoice.SalePayments.Where(x => x.Name != "Credit").AsEnumerable().Sum(x => x.Received),
                            DueDate = invoice.DueDate,
                            IsService = invoice.IsService,
                            IsProformaInvoice = invoice.IsProformaInvoice,
                            MarkAsCompleted = invoice.MarkAsCompleted,
                            InvoiceNote = invoice.InvoiceNote,
                            SaleHoldTypes = invoice.SaleHoldTypes,
                            IsClose = invoice.IsClose,
                            IsProcessed = invoice.IsProcessed,
                            Reason = invoice.Reason,
                            IsDefault = invoice.IsDefault,
                            BranchCode = branches.FirstOrDefault(x => x.Id == invoice.BranchId)?.Code,
                            //CounterName = terminalList.Where(x => x.Id == EF.Property<Guid>(invoice, "CounterId")).Select(x => x.Code).FirstOrDefault(),
                            UserName = userList.FirstOrDefault(x => x.Id == invoice.CreatedBy.ToString())?.UserName,
                            EditPurchaseOrderId = salesList.FirstOrDefault(x => x.PurchaseOrderId == invoice.Id)?.PurchaseOrderId,
                            EditProformaId = salesList.FirstOrDefault(x => x.ProformaId == invoice.Id)?.ProformaId,
                            EditDeliveryChallanId = salesList.FirstOrDefault(x => x.DeliveryChallanId == invoice.Id)?.DeliveryChallanId,

                        });

                    }

                    return new PagedResult<SaleListModel>
                    {
                        Results = new SaleListModel
                        {
                            Sales = invoiceList,
                        },
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = invoiceList.Count() / request.PageSize
                    };



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