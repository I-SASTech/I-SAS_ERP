using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Business.PaymentVouchers.Commands;
using Focus.Business.PaymentRefunds.Models;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherDetails
{
    public class PaymentVoucherDetailQuery : IRequest<PaymentVoucherLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsEmail { get; set; }

        public class Handler : IRequestHandler<PaymentVoucherDetailQuery, PaymentVoucherLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentVoucherDetailQuery> logger)
            {
                _context = context;
                Logger = logger;
            }

            public async Task<PaymentVoucherLookUpModel> Handle(PaymentVoucherDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;
                    var po = await _context.PaymentVouchers
                        .AsNoTracking()
                        .Include(x => x.TaxRate)
                        .Include(x => x.VatAccount)
                        .Include(x => x.Account)
                        .Include(x => x.Bills)
                        .ThenInclude(x => x.BillerAccount)
                        .Include(x => x.ContactAccount)
                        .Include(x => x.PaymentVoucherAttachments)
                        .Include(x => x.PaymentVoucherItems)

                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                     
                    if (po == null)
                        throw new NotFoundException("Purchase Order", "Was not found");

                    var paymentRefunds = await _context.PaymentRefunds
                        .Where(x => x.PaymentVoucherId == po.Id)
                        .Select(x => new PaymentRefundModel
                        {
                            Id = x.Id,
                            Date = x.Date,
                            Amount = x.Amount,
                            BankName = x.Account.Name,
                            ContactName = x.ContactAccount.Name,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == po.Id)
                        .AsQueryable();

                    var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);


                    var paymentVoucherAttachments = new List<PaymentVoucherAttachment>();
                    if (po.PaymentVoucherAttachments != null && po.PaymentVoucherAttachments.Count > 0)
                    {
                        foreach (var item in po.PaymentVoucherAttachments)
                        {
                            paymentVoucherAttachments.Add(new PaymentVoucherAttachment()
                            {
                                Id = item.Id,
                                Path = item.Path

                            });
                        }
                    }
                    var paymentVoucherItems = new List<PaymentVoucherItemLookUp>();
                    if (po.PaymentVoucherItems != null && po.PaymentVoucherItems.Count > 0)
                    {
                        foreach (var item in po.PaymentVoucherItems)
                        {
                            paymentVoucherItems.Add(new PaymentVoucherItemLookUp()
                            {
                                Id = item.Id,
                                PaymentVoucherId = item.Id,
                                Description = item.Description,
                                Amount = item.Amount,
                                PayAmount = item.PayAmount,
                                Total = item.Total,
                                ExtraAmount = item.ExtraAmount,
                                PurchaseInvoiceId = item.PurchaseInvoiceId,
                                SaleInvoiceId = item.SaleInvoiceId,
                                ChequeNumber = item.ChequeNumber,
                                Date = System.DateTime.Now,
                                IsActive = item.IsActive,
                                IsAging = item.IsAging,
                                DueAmount = item.DueAmount,
                                PartiallyInvoices=item.PartiallyInvoices
                                
                            });
                        }
                    }

                    var purcahse = new PurchasePost();
                    var sale = new Sale();
                    if (po.PurchaseInvoice != null && po.PurchaseInvoice != Guid.Empty)
                    {
                        purcahse = await _context.PurchasePosts.AsNoTracking().FirstOrDefaultAsync(
                            x => x.Id == po.PurchaseInvoice,
                            cancellationToken: cancellationToken);
                    }

                    if (po.SaleInvoice != null && po.SaleInvoice != Guid.Empty)
                    {
                        sale = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(x => x.Id == po.SaleInvoice,
                            cancellationToken: cancellationToken);
                    }

                    string documentName = "";
                    if (po.DocumentId != null)
                    {
                        if ( po.DocumentType == "Proforma Invoice")
                        {
                            var purcChase = _context.Sales.AsNoTracking()
                                .Select(x=>new 
                                {
                                    Id= x.Id,
                                    TotalAmount = x.TotalAmount,
                                    RegistrationNo = x.RegistrationNo,
                                    Date = x.Date,

                                }).FirstOrDefault(x => x.Id == po.DocumentId);


                            if (purcChase != null)
                            {
                                documentName= purcChase.RegistrationNo+ " "+ purcChase.Date.ToString("D")+ " "+ purcChase.TotalAmount;
                            }


                        }
                        if (po.DocumentType == "Sale Order" || po.DocumentType == "Quotation")
                        {
                            var purcChase = _context.SaleOrders.AsNoTracking()
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    TotalAmount = x.TotalAmount,
                                    RegistrationNo = x.RegistrationNo,
                                    Date = x.Date,

                                }).FirstOrDefault(x => x.Id == po.DocumentId);


                            if (purcChase != null)
                            {
                                documentName = purcChase.RegistrationNo + " " + purcChase.Date.ToString("D") + " " + purcChase.TotalAmount;
                            }


                        }

                        if (po.DocumentType == "Purchase Invoice")
                        {
                            var purcChase = _context.PurchasePosts.AsNoTracking()
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    TotalAmount = x.TotalAmount,
                                    RegistrationNo = x.RegistrationNo,
                                    Date = x.Date,

                                }).FirstOrDefault(x => x.Id == po.DocumentId);


                            if (purcChase != null)
                            {
                                documentName = purcChase.RegistrationNo + " " + purcChase.Date.ToString("D") + " " + purcChase.TotalAmount;
                            }


                        }

                        if (po.DocumentType == "Supplier Quotation" || po.DocumentType == "Purchase Order")
                        {
                            var purcChase = _context.PurchaseOrders.AsNoTracking()
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    TotalAmount = x.TotalAmount,
                                    RegistrationNo = x.RegistrationNo,
                                    Date = x.Date,

                                }).FirstOrDefault(x => x.Id == po.DocumentId);


                            if (purcChase != null)
                            {
                                documentName = purcChase.RegistrationNo + " " + purcChase.Date.ToString("D") + " " + purcChase.TotalAmount;
                            }


                        }


                    }

         

                    return new PaymentVoucherLookUpModel
                    {
                        DocumentType = po.DocumentType,
                        DocumentId = po.DocumentId,
                        DocumentName = documentName,
                        Id = po.Id,
                        Date = po.Date,
                        Dates = po.Date.ToString("MM/dd/yyyy hh:mm tt"),
                        VoucherNumber = po.VoucherNumber,
                        Narration = po.Narration,
                        ChequeNumber = po.ChequeNumber,
                        ApprovalStatus = po.ApprovalStatus,
                        Amount = po.Amount,
                        NatureOfPayment = po.NatureOfPayment,
                        TransactionId = po.TransactionId,
                        PurchaseInvoice = po.PurchaseInvoice,
                        PurchaseInvoiceCode = purcahse?.InvoiceNo,
                        SaleInvoice = po.SaleInvoice,
                        PettyCash = po.PettyCash,
                        SaleInvoiceCode = sale?.RegistrationNo,
                        PaymentVoucherType = po.PaymentVoucherType,
                        BankCashAccountId = po.BankCashAccountId,
                        BankCashAccountName = po.Account.Name,
                        ContactAccountName = po.ContactAccount.Name,
                        ContactAccountNameAr = po.ContactAccount.NameArabic,
                        ContactAccountId = po.ContactAccountId,
                        DraftBy = po.DraftBy,
                        ApprovedBy = po.ApprovedBy,
                        RejectBy = po.RejectBy,
                        VoidBy = po.VoidBy,
                        Reason = po.Reason,
                        DraftDate = po.DraftDate,
                        ApprovedDate = po.ApprovedDate,
                        RejectDate = po.RejectDate,
                        VoidDate = po.VoidDate,
                        PaymentMethod = po.PaymentMethod,
                        PaymentMode = po.PaymentMode,
                        PaymentVoucherAttachments = paymentVoucherAttachments,
                        PaymentVoucherItems = paymentVoucherItems,
                        Rate = po.TaxRate?.Rate,
                        VatName = po.VatAccount?.Name,
                        BillsName = po.Bills?.RegistrationNo + " / " + po.Bills?.BillerAccount.Name + " / " +
                                    po.Bills?.Date.ToString("MM/dd/yyyy hh:mm tt"),
                        TaxMethod = po.TaxMethod,
                        Vatamount = sale?.VatAmount ?? 0,
                        AttachmentList = attachmentList,
                        PaymentRefunds = paymentRefunds,
                        PaymentDate = po.PaymentDate,
                        InvoiceAmount= po.InvoiceAmount,
                        RemainingBalance = po.RemainingBalance,
                        IsSaleInvoice = false,
                        IsPurchaseInvoice = false,
                        FormName = ""
                    };
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}