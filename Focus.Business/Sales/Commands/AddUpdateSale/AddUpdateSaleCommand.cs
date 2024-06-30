using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleRegistrationNo;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Sales.Commands.AddSaleReturn;
using Focus.Business.Sales.Commands.SaleOrderPayment;
using Focus.Business.PaymentVouchers.Commands.AddUpdatePaymentVoucher;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherNumber;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Focus.Domain.Interface;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Sales.Commands.AddUpdateSale
{
    public class AddUpdateSaleCommand : IRequest<Message>
    {
        public SaleLookupModel Sale { get; set; }
        public string CounterId { get; set; }
        public Guid SaleId { get; set; }
        public bool IsPayment { get; set; }
        public decimal Payment { get; set; }
        public bool InvoiceWoInventory { get; set; }

        public class Handler : IRequestHandler<AddUpdateSaleCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly IHostingEnvironment _hostingEnvironment;
            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateSaleCommand> logger, IMediator mediator, IHostingEnvironment hostingEnvironment, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _hostingEnvironment = hostingEnvironment;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
            }

            public async Task<Message> Handle(AddUpdateSaleCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.Sale.Date.Year && x.PeriodStart.Month == request.Sale.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    if (request.Sale.Id != Guid.Empty)
                    {
                        var sale = await _context.Sales.FindAsync(request.Sale.Id);
                        if (sale == null)
                            throw new NotFoundException("Requested Sale Invoice not found ", request.Sale.Id);

                        if (request.Sale.IsEditPaidInvoice)
                        {
                            sale.DispatchDate = request.Sale.DispatchDate;
                            sale.DueDate = request.Sale.DueDate;
                            sale.PickUpDate = request.Sale.PickUpDate;
                            sale.InquiryId = request.Sale.InquiryId;
                            sale.ProformaId = request.Sale.ProformaId;
                            sale.PurchaseOrderId = request.Sale.PurchaseOrderId;
                            sale.SaleOrderNo = request.Sale.SaleOrderNo;
                            sale.DeliveryNoteAndDate = request.Sale.DeliveryNoteAndDate;
                            sale.QuotationValidUpto = request.Sale.QuotationValidUpto;
                            sale.PerfomaValidUpto = request.Sale.PerfomaValidUpto;
                            sale.PeroformaInvoiceNo = request.Sale.PeroformaInvoiceNo;
                            sale.PurchaseOrderNo = request.Sale.PurchaseOrderNo;
                            sale.IsCashCustomer = request.Sale.IsCashCustomer;
                            sale.InquiryNo = request.Sale.InquiryNo;
                            sale.QuotationNo = request.Sale.QuotationNo;
                            sale.DeliveryChallanId = request.Sale.DeliveryChallanId;
                            sale.Date = request.Sale.Date;
                            sale.CustomerInquiry = request.Sale.CustomerInquiry;
                            sale.DeliveryAddress = request.Sale.DeliveryAddress;
                            sale.DeliveryDate = request.Sale.DeliveryDate;
                            sale.BillingAddress = request.Sale.BillingAddress;
                            sale.ReferredBy = request.Sale.ReferredBy;
                            sale.DeliveryId = request.Sale.DeliveryId;
                            sale.ShippingId = request.Sale.ShippingId;
                            sale.BillingId = request.Sale.BillingId;
                            sale.NationalId = request.Sale.NationalId;
                            sale.DocumentType = request.Sale.DocumentType;
                            sale.IsWarranty = request.Sale.IsWarranty;
                            sale.TaxMethod = request.Sale.TaxMethod;
                            sale.PoNumber = request.Sale.PoNumber;
                            sale.PoDate = request.Sale.PoDate;
                            sale.TaxRateId = request.Sale.TaxRateId;
                            //sale.BarCode = request.Sale.BarCode;
                            sale.WareHouseId = request.Sale.WareHouseId;
                            sale.RegistrationNo = sale.RegistrationNo;
                            sale.PartiallyInvoices = sale.PartiallyInvoices;
                            sale.InvoiceType = request.Sale.InvoiceType;
                            sale.IsCredit = request.Sale.IsCredit;
                            sale.EmployeeId = request.Sale.EmployeeId;
                            sale.AreaId = request.Sale.AreaId;
                            sale.Change = request.Sale.Change;
                            sale.Note = request.Sale.Note;
                            sale.IsOverWrite = request.Sale.IsOverWrite;
                            sale.DoctorName = request.Sale.DoctorName;
                            sale.HospitalName = request.Sale.HospitalName;
                            sale.RefrenceNo = request.Sale.RefrenceNo;
                            sale.CustomerAddressWalkIn = request.Sale.CustomerAddressWalkIn;
                            sale.SaleOrderId = request.Sale.SaleOrderId;
                            sale.QuotationId = request.Sale.QuotationId;
                            sale.LogisticId = request.Sale.LogisticId;
                            sale.Mobile = request.Sale.Mobile;
                            sale.UnRegisteredVatId = request.Sale.UnRegisteredVatId;
                            sale.UnRegisteredVAtAmount = request.Sale.UnRegisteredVAtAmount;
                            sale.PeriodId = period.Id;
                            sale.Description = request.Sale.Description;
                            sale.IsService = request.Sale.IsService;
                            sale.TermAndCondition = request.Sale.TermAndCondition;
                            sale.TermAndConditionAr = request.Sale.TermAndConditionAr;
                            sale.WarrantyLogoPath = request.Sale.WarrantyLogoPath;
                            sale.Discount = request.Sale.Discount;
                            sale.IsBeforeTax = request.Sale.IsBeforeTax;
                            sale.TransactionLevelDiscount = request.Sale.TransactionLevelDiscount;
                            sale.IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction;
                            sale.IsFixed = request.Sale.IsFixed;
                            sale.VatAmount = request.Sale.VatAmount;
                            sale.DiscountAmount = request.Sale.DiscountAmount;
                            sale.TotalAfterDiscount = request.Sale.TotalAfterDiscount;
                            sale.TotalAmount = request.Sale.TotalAmount;
                            sale.SaleHoldTypes = request.Sale.SaleHoldTypes;
                            foreach (var updatedItem in request.Sale.SaleItems)
                            {
                                var existingItem = sale.SaleItems.FirstOrDefault(item => item.Id == updatedItem.RowId);
                                if (existingItem != null)
                                {
                                    // Update the necessary properties of existingItem
                                    existingItem.Description = updatedItem.Description;
                                    existingItem.StyleNumber = updatedItem.StyleNumber;
                                }
                            }

                            foreach (var updatedItem in request.Sale.SaleItems)
                            {
                                var existingItem = sale.SaleItems.FirstOrDefault(item => item.Id == updatedItem.RowId);
                                if (existingItem != null)
                                {
                                    // Update the necessary properties of existingItem
                                    existingItem.Description = updatedItem.Description;
                                    existingItem.StyleNumber = updatedItem.StyleNumber;
                                }
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = sale.BranchId,
                                Code = _code,
                                DocumentNo = sale.RegistrationNo
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);

                            request.SaleId = sale.Id;
                            request.IsPayment = true;
                            var message12 = new Message
                            {
                                Id = sale.Id,
                                IsAddUpdate = "Data has been updated successfully"
                            };

                            return message12;
                        }
                        //find invoice 

                        if (!request.Sale.IsTouch)
                        {
                            //Cash Customer paid invoice(Check cash or bank account selected or not)
                            if (request.Sale.InvoiceType == InvoiceType.Paid && request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null && !request.Sale.IsSaleReturnPost)
                            {
                                if (request.Sale.BankCashAccountId == Guid.Empty || request.Sale.BankCashAccountId == null)
                                    throw new NotFoundException("You not Select Bank/Cash Account", null);
                            }
                        }
                        if (request.Sale.InvoiceType == InvoiceType.Hold)
                        {
                            request.Sale.DocumentType = DocumentType.SaleInvoiceHold;

                        }
                        else if (request.Sale.InvoiceType == InvoiceType.ProformaInvoice)
                        {
                            request.Sale.DocumentType = DocumentType.ProformaInvoice;

                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit)
                        {
                            request.Sale.DocumentType = DocumentType.SaleInvoice;

                        }


                        if (request.Sale.IsRemove)
                        {
                            request.Sale.WarrantyLogoPath = string.IsNullOrEmpty(request.Sale.WarrantyLogoPath) ? "" : GetBaseImage(request.Sale.WarrantyLogoPath).Result;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(request.Sale.WarrantyLogoPath))
                            {
                                if (request.Sale.WarrantyLogoPath.Contains("Attachment"))
                                {
                                    request.Sale.WarrantyLogoPath = GetBaseImage(request.Sale.WarrantyLogoPath).Result;
                                }
                            }
                        }

                        request.IsPayment = false;
                        // Find Auto invoice No

                        var message = new Message();

                        var invoiceNo = await _mediator.Send(new GetSaleRegistrationNoQuery
                        {
                            TerminalId = request.Sale.TerminalId,
                            InvoicePrefix = request.Sale.InvoicePrefix,
                            BranchId = request.Sale.BranchId,
                            UserId = _contextProvider.GetUserId().ToString()
                        }, cancellationToken);


                        string invoiceNumber;
                        if (request.Sale.DocumentType == DocumentType.SaleInvoice)
                        {
                            invoiceNumber = invoiceNo.Paid;
                        }
                        else if (request.Sale.DocumentType == DocumentType.GlobalInvoice)
                        {
                            invoiceNumber = invoiceNo.GlobalInvoice;
                        }
                        else if (request.Sale.DocumentType == DocumentType.ReceiptInvoice)
                        {
                            invoiceNumber = invoiceNo.ReceiptInvoice;
                        }
                        else if (request.Sale.DocumentType == DocumentType.ProformaInvoice)
                        {
                            invoiceNumber = invoiceNo.ProformaInvoice;
                        }
                        else
                        {
                            invoiceNumber = invoiceNo.SaleReturn;
                        }


                        PartiallyInvoices partiallyInvoices = new PartiallyInvoices();
                        if (request.Sale.InvoiceType == InvoiceType.Paid)
                        {
                            request.Sale.ApprovalDate = DateTime.UtcNow;
                            partiallyInvoices = PartiallyInvoices.Fully;
                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Credit)
                        {
                            request.Sale.ApprovalDate = DateTime.UtcNow;
                            partiallyInvoices = PartiallyInvoices.UnPaid;
                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Hold)
                        {
                            invoiceNumber = request.Sale.RegistrationNo;
                        }

                        // Update Sale Record

                        //Remove previous Record
                        _context.SaleItems.RemoveRange(sale.SaleItems);
                        _context.SalePayments.RemoveRange(sale.SalePayments);

                        var attachments = await _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == sale.Id)
                        .ToListAsync(cancellationToken: cancellationToken);

                        if (attachments.Any())
                        {
                            _context.Attachments.RemoveRange(attachments);
                        }

                        //Sale Item New Record 
                        var newItem = new List<SaleItem>();
                        foreach (var item in request.Sale.SaleItems)
                        {
                            newItem.Add(new SaleItem()
                            {
                                DocumentDate = request.Sale.Date,
                                ProductId = item.ProductId,
                                SoItemId = item.ItemId,
                                TaxRateId = item.TaxRateId == Guid.Empty ? throw new ApplicationException("TaxRate is not null") : item.TaxRateId,
                                TaxMethod = item.TaxMethod ?? throw new ApplicationException("TaxMethod is not null"),
                                IsFree = item.IsFree,
                                Discount = item.Discount,
                                FixDiscount = item.FixDiscount,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                WareHouseId = request.Sale.WareHouseId,
                                UnitName = item.UnitName,
                                RemainingQuantity = item.Quantity,
                                SerialNo = item.SerialNo,
                                OfferQuantity = item.OfferQuantity,
                                PromotionOfferQuantity = item.PromotionOfferQuantity,
                                Buy = item.Buy,
                                Get = item.Get,
                                DiscountAmount = item.DiscountAmount,
                                VatAmount = item.VatAmount,
                                TotalAmount = item.TotalAmount,
                                TotalWithOutAmount = item.GrossAmount,
                                BundleId = item.BundleId,
                                PromotionItemId = item.PromotionId,
                                ServiceItem = item.ServiceItem,
                                Description = item.Description,
                                Serial = item.Serial,
                                GuaranteeDate = item.GuaranteeDate,
                                BatchExpiry = item.BatchExpiry,
                                BatchNo = item.BatchNo,
                                ColorId = item.ColorId,
                                StyleNumber = item.StyleNumber,
                                Scheme = item.Scheme,
                                SchemePhysicalQuantity = item.SchemePhysicalQuantity,
                                SchemeQuantity = item.SchemeQuantity,
                                SaleSizeAssortments = item.SaleSizeAssortment?.Select(size => new SaleSizeAssortment()
                                {
                                    SizeId = size.SizeId,
                                    Quantity = size.Quantity
                                }).ToList()
                            });
                        }


                        sale.DispatchDate = request.Sale.DispatchDate;
                        sale.PickUpDate = request.Sale.PickUpDate;
                        sale.InquiryId = request.Sale.InquiryId;
                        sale.ProformaId = request.Sale.ProformaId;
                        sale.PurchaseOrderId = request.Sale.PurchaseOrderId;
                        sale.SaleOrderNo = request.Sale.SaleOrderNo;
                        sale.DeliveryNoteAndDate = request.Sale.DeliveryNoteAndDate;
                        sale.QuotationValidUpto = request.Sale.QuotationValidUpto;
                        sale.PerfomaValidUpto = request.Sale.PerfomaValidUpto;
                        sale.PeroformaInvoiceNo = request.Sale.PeroformaInvoiceNo;
                        sale.PurchaseOrderNo = request.Sale.PurchaseOrderNo;
                        sale.IsCashCustomer = request.Sale.IsCashCustomer;
                        sale.InquiryNo = request.Sale.InquiryNo;
                        sale.QuotationNo = request.Sale.QuotationNo;
                        sale.DeliveryChallanId = request.Sale.DeliveryChallanId;
                        sale.Date = request.Sale.Date;
                        sale.CustomerInquiry = request.Sale.CustomerInquiry;
                        sale.DeliveryAddress = request.Sale.DeliveryAddress;
                        sale.DeliveryDate = request.Sale.DeliveryDate;
                        sale.BillingAddress = request.Sale.BillingAddress;
                        sale.ReferredBy = request.Sale.ReferredBy;
                        sale.DeliveryId = request.Sale.DeliveryId;
                        sale.ShippingId = request.Sale.ShippingId;
                        sale.BillingId = request.Sale.BillingId;
                        sale.NationalId = request.Sale.NationalId;
                        sale.DocumentType = request.Sale.DocumentType;
                        sale.IsWarranty = request.Sale.IsWarranty;
                        sale.TaxMethod = request.Sale.TaxMethod;
                        sale.PoNumber = request.Sale.PoNumber;
                        sale.PoDate = request.Sale.PoDate;
                        sale.TaxRateId = request.Sale.TaxRateId;
                        //sale.BarCode = request.Sale.BarCode;
                        sale.WareHouseId = request.Sale.WareHouseId;
                        sale.RegistrationNo = invoiceNumber;
                        sale.PartiallyInvoices = partiallyInvoices;
                        sale.InvoiceType = request.Sale.InvoiceType;
                        sale.IsCredit = request.Sale.IsCredit;
                        sale.EmployeeId = request.Sale.EmployeeId;
                        sale.AreaId = request.Sale.AreaId;
                        sale.Change = request.Sale.Change;
                        sale.Note = request.Sale.Note;
                        sale.IsOverWrite = request.Sale.IsOverWrite;
                        sale.DoctorName = request.Sale.DoctorName;
                        sale.HospitalName = request.Sale.HospitalName;
                        sale.RefrenceNo = request.Sale.RefrenceNo;
                        sale.CustomerAddressWalkIn = request.Sale.CustomerAddressWalkIn;
                        sale.SaleOrderId = request.Sale.SaleOrderId;
                        sale.QuotationId = request.Sale.QuotationId;
                        sale.LogisticId = request.Sale.LogisticId;
                        sale.Mobile = request.Sale.Mobile;
                        sale.UnRegisteredVatId = request.Sale.UnRegisteredVatId;
                        sale.UnRegisteredVAtAmount = request.Sale.UnRegisteredVAtAmount;
                        sale.PeriodId = period.Id;
                        sale.Description = request.Sale.Description;
                        sale.IsService = request.Sale.IsService;
                        sale.TermAndCondition = request.Sale.TermAndCondition;
                        sale.TermAndConditionAr = request.Sale.TermAndConditionAr;
                        sale.WarrantyLogoPath = request.Sale.WarrantyLogoPath;
                        sale.Discount = request.Sale.Discount;
                        sale.IsBeforeTax = request.Sale.IsBeforeTax;
                        sale.TransactionLevelDiscount = request.Sale.TransactionLevelDiscount;
                        sale.IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction;
                        sale.IsFixed = request.Sale.IsFixed;
                        sale.SaleItems = newItem;
                        sale.TerminalId = !string.IsNullOrEmpty(request.Sale.TerminalId) ? Guid.Parse(request.Sale.TerminalId) : null;
                        sale.VatAmount = request.Sale.VatAmount;
                        sale.DiscountAmount = request.Sale.DiscountAmount;
                        sale.TotalAfterDiscount = request.Sale.TotalAfterDiscount;
                        sale.TotalAmount = request.Sale.TotalAmount;
                        sale.SaleHoldTypes = request.Sale.SaleHoldTypes;
                        sale.DueDate = request.Sale.DueDate;

                        if (request.Sale.OtherCurrency.CurrencyId != Guid.Empty)
                        {
                            if (request.Sale.OtherCurrency.CurrencyId != null)
                                sale.OtherCurrency = new OtherCurrency()
                                {
                                    CurrencyId = request.Sale.OtherCurrency.CurrencyId.Value,
                                    Amount = request.Sale.OtherCurrency.Amount,
                                    Rate = request.Sale.OtherCurrency.Rate
                                };
                        }

                        //Update Customer Data
                        if (request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null && request.Sale.IsCredit)
                        {
                            //if (request.Sale.DueDate == default && request.Sale.DueDate == null)
                            //    throw new ApplicationException("Due date is required.");

                            sale.CashCustomerId = null;
                            sale.CustomerId = request.Sale.CustomerId;
                            sale.DueDate = request.Sale.DueDate;
                        }
                        else
                        {
                            sale.CashCustomerId = null;
                            sale.CustomerId = request.Sale.CustomerId;
                            sale.DueDate = request.Sale.DueDate;
                            //if (string.IsNullOrEmpty(request.Sale.CashCustomer))
                            //    throw new ApplicationException("Cash Customer is required.");

                            //var cashCustomerExist = await _context.CashCustomer.AsNoTracking().FirstOrDefaultAsync(x =>
                            //       x.Name.ToLower() == request.Sale.CashCustomer.ToLower(), cancellationToken: cancellationToken);
                            //if (cashCustomerExist == null)
                            //{
                            //    string code;
                            //    var autoCode = await _context.CashCustomer.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                            //    if (autoCode != null)
                            //    {
                            //        string fetchNo = autoCode.Code.Substring(3);
                            //        var autoNo = Convert.ToInt32((fetchNo));
                            //        var format = "00000";
                            //        autoNo++;
                            //        var prefix = "CC-";
                            //        var newCode = prefix + autoNo.ToString(format);
                            //        code = newCode;
                            //    }
                            //    else
                            //    {
                            //        code = "CC-00001";
                            //    }
                            //    sale.CashCustomer = new CashCustomer
                            //    {
                            //        Name = request.Sale.CashCustomer,
                            //        Mobile = request.Sale.Mobile,
                            //        Code = code,
                            //        Email = request.Sale.Email,
                            //        CustomerId = request.Sale.CashCustomerId,
                            //        Address = request.Sale.CustomerAddressWalkIn
                            //    };
                            //}
                            //else
                            //{
                            //    sale.CashCustomerId = cashCustomerExist.Id;
                            //    sale.CashCustomer.Name = request.Sale.CashCustomer;
                            //    sale.CashCustomer.Mobile = request.Sale.Mobile;
                            //    sale.CashCustomer.Email = request.Sale.Email;
                            //    sale.CashCustomer.CustomerId = request.Sale.CashCustomerId;
                            //}
                            //sale.CustomerId = null;
                        }

                        if (request.Sale.QuotationId != null)
                        {
                            var saleOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.Sale.QuotationId, cancellationToken: cancellationToken);
                            if (saleOrder != null)
                            {
                                sale.InvoiceNote = saleOrder.RegistrationNo + " - " + saleOrder.Date.ToString("dd/MM/yyyy");

                            }
                        }
                        else if (request.Sale.SaleOrderId != null)
                        {
                            var saleOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.Sale.SaleOrderId, cancellationToken: cancellationToken);
                            if (saleOrder != null)
                            {
                                sale.InvoiceNote = saleOrder.RegistrationNo + " - " + saleOrder.Date.ToString("dd/MM/yyyy");

                            }
                        }
                        else if (request.Sale.ProformaId != null)
                        {
                            var proformaInvoice = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.ProformaId, cancellationToken: cancellationToken);
                            if (proformaInvoice != null)
                            {
                                proformaInvoice.IsProcessed = true;
                                proformaInvoice.IsClose = true;
                                proformaInvoice.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                _context.Sales.Update(proformaInvoice);
                                sale.InvoiceNote = proformaInvoice.RegistrationNo + " - " + proformaInvoice.Date.ToString("dd/MM/yyyy");

                            }

                        }
                        else if (request.Sale.PurchaseOrderId != null)
                        {
                            var proformaInvoice = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.PurchaseOrderId, cancellationToken: cancellationToken);
                            if (proformaInvoice != null)
                            {
                                proformaInvoice.IsProcessed = true;
                                proformaInvoice.IsClose = true;
                                proformaInvoice.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                _context.Sales.Update(proformaInvoice);
                                sale.InvoiceNote = proformaInvoice.RegistrationNo + " - " + proformaInvoice.Date.ToString("dd/MM/yyyy");
                            }

                        }
                        else if (request.Sale.DeliveryChallanId != null)
                        {
                            var deliveryChallanrEC = _context.DeliveryChallanReserveds
                                .AsNoTracking()
                                .Include(x => x.DeliveryChallanReserverdItems)
                                .FirstOrDefault(x => x.DeliveryChallanId == request.Sale.DeliveryChallanId);

                            var challan = _context.DeliveryChallans.AsNoTracking().FirstOrDefault(x => x.Id == request.Sale.DeliveryChallanId);


                            if (deliveryChallanrEC != null)
                            {
                                foreach (var reservd in deliveryChallanrEC.DeliveryChallanReserverdItems)
                                {
                                    foreach (var x in request.Sale.SaleItems)
                                    {
                                        if (reservd.ProductId != null)
                                        {
                                            if (x.ProductId == reservd.ProductId)
                                            {
                                                if (reservd.Quantity > 0)
                                                {
                                                    reservd.Quantity -= x.Quantity;

                                                }
                                            }
                                        }

                                        if (x.ServiceProductId == reservd.ServiceProductId)
                                        {
                                            if (reservd.Quantity > 0)
                                            {
                                                reservd.Quantity -= x.Quantity;

                                            }

                                        }

                                    }

                                }
                                deliveryChallanrEC.IsClose = deliveryChallanrEC.DeliveryChallanReserverdItems.All(x => x.Quantity <= 0);
                                _context.DeliveryChallanReserveds.Update(deliveryChallanrEC);
                                if (deliveryChallanrEC.IsClose)
                                {
                                    challan.IsClose = true;
                                    challan.IsProcessed = true;
                                    challan.Reason = "Issued by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                    _context.DeliveryChallans.Update(challan);
                                }
                            }
                            sale.InvoiceNote = challan.RegistrationNo + " - " + challan.Date.ToString("dd/MM/yyyy");
                        }






                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        //Product Variation
                        if ((request.Sale.InvoiceType == InvoiceType.Paid || (request.Sale.IsCredit && request.Sale.InvoiceType == InvoiceType.Credit)) && request.Sale.ColorVariants)
                        {
                            foreach (var item in request.Sale.SaleItems.Where(x => !x.ServiceItem))
                            {
                                foreach (var size in item.SaleSizeAssortment.Where(size => size.Quantity > 0))
                                {
                                    var variation = await _context.VariationInventories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.ColorId == item.ColorId && x.SizeId == size.SizeId, cancellationToken: cancellationToken);
                                    if (variation == null)
                                    {
                                        var variationInventory = new VariationInventory()
                                        {
                                            ProductId = item.ProductId.Value,
                                            ColorId = item.ColorId,
                                            SizeId = size.SizeId,
                                            Quantity = size.Quantity * -1
                                        };
                                        await _context.VariationInventories.AddAsync(variationInventory, cancellationToken);
                                    }
                                    else
                                    {
                                        variation.Quantity -= size.Quantity;
                                        _context.VariationInventories.Update(variation);
                                    }

                                    var variationInventoryForReporting = new VariationInventoryForReporting()
                                    {
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleInvoice,
                                        ProductId = item.ProductId.Value,
                                        ColorId = item.ColorId.Value,
                                        SizeId = size.SizeId,
                                        Quantity = size.Quantity
                                    };
                                    await _context.VariationInventoryForReportings.AddAsync(variationInventoryForReporting, cancellationToken);
                                }
                            }
                        }



                        //Add Attachments
                        if (request.Sale.AttachmentList != null && request.Sale.AttachmentList.Count > 0)
                        {
                            foreach (var item in request.Sale.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = sale.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        //UpdateSale Quotation Record
                        if ((request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit) && request.Sale.QuotationId != null)
                        {
                            var quotation = await _context.SaleOrders.FindAsync(request.Sale.QuotationId);
                            if (request.Sale.IsService)
                            {
                                foreach (var item in request.Sale.SaleItems)
                                {
                                    var soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.Id == item.ItemId);
                                    if (soItemDetail != null)
                                    {
                                        soItemDetail.QuantityOut -= item.Quantity;
                                        _context.SaleOrderItems.Update(soItemDetail);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in request.Sale.SaleItems)
                                {
                                    var soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                    if (soItemDetail != null)
                                    {
                                        soItemDetail.QuantityOut -= item.Quantity;
                                        _context.SaleOrderItems.Update(soItemDetail);
                                    }
                                }
                            }

                            var close = quotation.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.Sale.QuotationId && x.QuantityOut > 0);
                            if (close == null || !request.Sale.PartiallyQuotation)
                            {
                                quotation.IsClose = true;
                                quotation.IsProcessed = true;
                                quotation.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");


                            }
                        }


                        //UpdateSale SaleOrder Record
                        SaleOrder soItem = null;
                        decimal totalAmount = 0;
                        if ((request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit) && request.Sale.SaleOrderId != null)
                        {
                            soItem = await _context.SaleOrders.FindAsync(request.Sale.SaleOrderId);

                            if (soItem == null)
                                throw new NotFoundException("Sale Order", "Was not found");

                            if (request.Sale.IsService)
                            {
                                foreach (var item in request.Sale.SaleItems)
                                {
                                    var soItemDetail = soItem.SaleOrderItems.FirstOrDefault(x => x.Id == item.ItemId);
                                    if (soItemDetail != null)
                                    {
                                        soItemDetail.QuantityOut -= item.Quantity;
                                        _context.SaleOrderItems.Update(soItemDetail);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in request.Sale.SaleItems)
                                {
                                    var soItemDetail = soItem.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId && x.BatchNo == item.BatchNo);
                                    if (soItemDetail != null)
                                    {
                                        soItemDetail.QuantityOut -= item.Quantity;
                                        _context.SaleOrderItems.Update(soItemDetail);
                                    }
                                }
                            }

                            var close = soItem.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.Sale.SaleOrderId && x.QuantityOut > 0);
                            if (close == null || !request.Sale.PartiallySaleOrder)
                            {
                                soItem.IsClose = true;
                                soItem.IsProcessed = true;
                                soItem.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");

                            }
                            //Payment Voucher Update

                            var totalPay = soItem.SaleOrderPayments.Sum(x => x.Amount);
                            var result = request.Sale.SalePayment.DueAmount - (totalPay - request.Payment);
                            totalAmount = totalPay - request.Payment;

                            if (request.Sale.AutoPaymentVoucher == "true" && request.Sale.IsCredit)
                            {
                                await _mediator.Send(new AddPaymentVoucherCommand
                                {
                                    SaleInvoice = sale.Id,
                                    ContactAccountId = soItem.Customer.AccountId.Value,
                                    ContactAdvanceAccountId = soItem.Customer.AdvanceAccountId.Value,
                                    Payment = request.Payment,
                                    DueAmount = request.Sale.SalePayment.DueAmount,
                                    SaleOrderPayments = soItem.SaleOrderPayments,
                                    IsCredit = request.Sale.IsCredit,
                                    BankCashAccountId = request.Sale.BankCashAccountId,
                                    PaymentType = SalePaymentType.Advance,
                                    UserName = request.Sale.UserName,
                                    BranchId = sale.BranchId,
                                }, cancellationToken);

                                if (request.Sale.IsCredit)
                                {
                                    if (result <= 0)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                        var saleAdvancePayment = new SaleInvoiceAdvancePayment()
                                        {
                                            Amount = request.Sale.SalePayment.DueAmount,
                                            Date = DateTime.UtcNow,
                                            SaleInvoiceId = sale.Id,
                                            SaleOrderId = sale.SaleOrderId,
                                        };
                                        await _context.SaleInvoiceAdvancePayments.AddAsync(saleAdvancePayment, cancellationToken);
                                    }
                                    else if (totalPay - request.Payment > 0)
                                    {
                                        sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                        var saleAdvancePayment = new SaleInvoiceAdvancePayment()
                                        {
                                            Amount = totalPay - request.Payment,
                                            Date = DateTime.UtcNow,
                                            SaleInvoiceId = sale.Id,
                                            SaleOrderId = sale.SaleOrderId,
                                        };
                                        await _context.SaleInvoiceAdvancePayments.AddAsync(saleAdvancePayment, cancellationToken);
                                    }
                                }
                            }
                        }

                        if (request.Sale.InvoiceType == InvoiceType.Hold)
                        {
                            decimal paidAmount = 0;
                            var salePayments = new List<SalePayment>();
                            if (request.Sale.CreditPays != null)
                            {
                                foreach (var credit in request.Sale.CreditPays)
                                {

                                    var salePayment = new SalePayment
                                    {
                                        DueAmount = credit.Amount,
                                        Received = credit.Amount,
                                        Balance = 0,
                                        Change = 0,
                                        PaymentType = credit.PaymentMode == "Cash" ? SalePaymentType.Cash : credit.PaymentMode == "Bank" ? SalePaymentType.Bank : SalePaymentType.Default,
                                        BankCashAccountId = credit.AccountId,
                                        Name = credit.PaymentMode,
                                        SaleId = sale.Id,
                                        Rate = 0,
                                        Amount = 0,
                                        Description = credit.Description
                                    };
                                    salePayments.Add(salePayment);


                                    paidAmount += credit.Amount;

                                }

                                await _context.SalePayments.AddRangeAsync(salePayments, cancellationToken);
                            }

                        }

                        //make Copy of Hold Invoice

                        if (sale.InvoiceType != InvoiceType.Hold && sale.InvoiceType != InvoiceType.SaleReturn && sale.InvoiceType != InvoiceType.ProformaInvoice && sale.InvoiceType != InvoiceType.GlobalInvoice && sale.InvoiceType != InvoiceType.ReceiptInvoice)
                        {
                            var sale1 = new Sale()
                            {
                                InquiryId = request.Sale.InquiryId,
                                ProformaId = request.Sale.ProformaId,
                                PurchaseOrderId = request.Sale.PurchaseOrderId,
                                DeliveryNoteAndDate = request.Sale.DeliveryNoteAndDate,
                                QuotationValidUpto = request.Sale.QuotationValidUpto,
                                PerfomaValidUpto = request.Sale.PerfomaValidUpto,
                                DeliveryChallanId = request.Sale.DeliveryChallanId,
                                Date = request.Sale.Date,
                                IsCashCustomer = request.Sale.IsCashCustomer,
                                DocumentType = DocumentType.SaleInvoiceHold,
                                IsWarranty = request.Sale.IsWarranty,
                                TaxMethod = request.Sale.TaxMethod,
                                SaleOrderNo = request.Sale.SaleOrderNo,
                                QuotationNo = request.Sale.QuotationNo,
                                PeroformaInvoiceNo = request.Sale.PeroformaInvoiceNo,
                                PurchaseOrderNo = request.Sale.PurchaseOrderNo,
                                InquiryNo = request.Sale.InquiryNo,
                                PoNumber = request.Sale.PoNumber,
                                CustomerInquiry = request.Sale.CustomerInquiry,
                                DeliveryAddress = request.Sale.DeliveryAddress,
                                DeliveryDate = request.Sale.DeliveryDate,
                                BillingAddress = request.Sale.BillingAddress,
                                ReferredBy = request.Sale.ReferredBy,
                                DeliveryId = request.Sale.DeliveryId,
                                ShippingId = request.Sale.ShippingId,
                                BillingId = request.Sale.BillingId,
                                NationalId = request.Sale.NationalId,
                                PoDate = request.Sale.PoDate,
                                TaxRateId = request.Sale.TaxRateId,
                                BarCode = request.Sale.BarCode,
                                WareHouseId = request.Sale.WareHouseId,
                                RegistrationNo = request.Sale.RegistrationNo,
                                PartiallyInvoices = 0,
                                InvoiceType = 0,
                                IsCredit = request.Sale.IsCredit,
                                EmployeeId = request.Sale.EmployeeId,
                                AreaId = request.Sale.AreaId,
                                Change = request.Sale.Change,
                                Note = request.Sale.Note,
                                IsOverWrite = request.Sale.IsOverWrite,
                                DoctorName = request.Sale.DoctorName,
                                HospitalName = request.Sale.HospitalName,
                                RefrenceNo = request.Sale.RefrenceNo,
                                CustomerAddressWalkIn = request.Sale.CustomerAddressWalkIn,
                                SaleOrderId = request.Sale.SaleOrderId,
                                QuotationId = request.Sale.QuotationId,
                                LogisticId = request.Sale.LogisticId,
                                Mobile = request.Sale.Mobile,
                                UnRegisteredVatId = request.Sale.UnRegisteredVatId,
                                UnRegisteredVAtAmount = request.Sale.UnRegisteredVAtAmount,
                                PeriodId = period.Id,
                                Description = request.Sale.Description,
                                IsService = request.Sale.IsService,
                                TermAndCondition = request.Sale.TermAndCondition,
                                TermAndConditionAr = request.Sale.TermAndConditionAr,
                                WarrantyLogoPath = request.Sale.WarrantyLogoPath,
                                Discount = request.Sale.Discount,
                                TerminalId = !string.IsNullOrEmpty(request.Sale.TerminalId) ? Guid.Parse(request.Sale.TerminalId) : null,
                                IsBeforeTax = request.Sale.IsBeforeTax,
                                TransactionLevelDiscount = request.Sale.TransactionLevelDiscount,
                                IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction,
                                IsFixed = request.Sale.IsFixed,
                                VatAmount = request.Sale.VatAmount,
                                DiscountAmount = request.Sale.DiscountAmount,
                                TotalAfterDiscount = request.Sale.TotalAfterDiscount,
                                TotalAmount = request.Sale.TotalAmount,
                                TotalWithOutAmount = request.Sale.GrossAmount,
                                IsVatChange = true,
                                DueDate = request.Sale.DueDate,
                                SaleHoldTypes = SaleHoldTypes.Used,
                                CustomerId = request.Sale.CustomerId,
                                CreatedBy = _contextProvider.GetUserId() ?? Guid.Empty,
                                DispatchDate = request.Sale.DispatchDate,
                                PickUpDate = request.Sale.PickUpDate,
                                SaleItems = request.Sale.SaleItems.Select(x =>
                                            new SaleItem()
                                            {
                                                DocumentDate = request.Sale.Date,
                                                ProductId = x.ProductId,
                                                SoItemId = x.ItemId,
                                                TaxRateId = x.TaxRateId == Guid.Empty ? throw new ApplicationException("TaxRate is not null") : x.TaxRateId,
                                                IsFree = x.IsFree,
                                                TaxMethod = x.TaxMethod ?? throw new ApplicationException("TaxMethod is not null"),
                                                Discount = x.Discount,
                                                FixDiscount = x.FixDiscount,
                                                Quantity = x.Quantity,
                                                UnitName = x.UnitName,
                                                UnitPrice = x.UnitPrice,
                                                WareHouseId = request.Sale.WareHouseId,
                                                RemainingQuantity = x.Quantity,
                                                OfferQuantity = x.OfferQuantity,
                                                VatAmount = x.VatAmount,
                                                DiscountAmount = x.DiscountAmount,
                                                TotalAmount = x.TotalAmount,
                                                TotalWithOutAmount = x.GrossAmount,
                                                Buy = x.Buy,
                                                Get = x.Get,
                                                BundleId = x.BundleId,
                                                SerialNo = x.SerialNo,
                                                PromotionItemId = x.PromotionId,
                                                PromotionOfferQuantity = x.PromotionOfferQuantity,
                                                ServiceItem = x.ServiceItem,
                                                Description = x.Description,
                                                Serial = x.Serial,
                                                GuaranteeDate = x.GuaranteeDate,
                                                BatchExpiry = x.BatchExpiry,
                                                BatchNo = x.BatchNo,
                                                AutoNumber = x.AutoNumber,
                                                ColorId = x.ColorId,
                                                StyleNumber = x.StyleNumber,
                                                Scheme = x.Scheme,
                                                SchemePhysicalQuantity = x.SchemePhysicalQuantity,
                                                SchemeQuantity = x.SchemeQuantity,
                                                SaleSizeAssortments = x.SaleSizeAssortment?.Select(size => new SaleSizeAssortment()
                                                {
                                                    SizeId = size.SizeId,
                                                    Quantity = size.Quantity
                                                }).ToList()
                                            }).ToList()
                            };

                            await _context.Sales.AddAsync(sale1, cancellationToken);

                            if (request.Sale.AttachmentList is { Count: > 0 })
                            {
                                foreach (var item in request.Sale.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = sale.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);
                                }
                            }
                        }


                        //if sale invoice contain payment details
                        if (request.Sale.InvoiceType == InvoiceType.Paid)
                        {
                            await SaveSalePaymentAsync(sale, request, invoiceNo);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);


                        //Add Transaction according to concerns accounts
                        if (request.Sale.InvoiceType == InvoiceType.Paid || (request.Sale.IsCredit && request.Sale.InvoiceType == InvoiceType.Credit))
                        {
                            await AddTransaction(sale.Id, request.CounterId, request.Sale.DayStart,
                                request.Sale.SaleOrderId, request.Sale.SoInventoryReserve, request.InvoiceWoInventory, soItem, totalAmount,
                                request.Sale.AutoPaymentVoucher, request.Sale, request.Sale.IsFifo, period);
                        }
                        scope.Complete();

                        message.Id = sale.Id;
                        request.SaleId = sale.Id;
                        message.IsAddUpdate = "Data has been updated successfully";
                        request.IsPayment = true;


                        
                        return message;
                    }
                    // Add New Sale Command
                    else
                    {
                        request.Sale.BarCode = "";
                        for (int i = 0; i < 11; i++)
                        {
                            request.Sale.BarCode += rnd.Next(0, 9).ToString();
                        }
                        //Cash Customer paid invoice(Check cash or bank account selected or not)
                        if (!request.Sale.IsTouch)
                        {
                            if (request.Sale.InvoiceType == InvoiceType.Paid && request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null && !request.Sale.IsSaleReturnPost)
                            {
                                if (request.Sale.BankCashAccountId == Guid.Empty || request.Sale.BankCashAccountId == null)
                                    throw new NotFoundException("You not Select Bank/Cash Account", null);
                            }
                        }
                        if (request.Sale.IsTouch)
                        {
                            if (request.Sale.CustomerId == null)
                            {
                                var checkCashCustomer = _context.Contacts.AsNoTracking().FirstOrDefault(x => x.EnglishName == "Walk-In");
                                if (checkCashCustomer != null)
                                {
                                    request.Sale.CustomerId = checkCashCustomer.Id;
                                    request.Sale.IsWalkIn = true;
                                }
                            }
                        }
                        if (request.Sale.InvoiceType == InvoiceType.Hold)
                        {
                            request.Sale.DocumentType = DocumentType.SaleInvoiceHold;

                        }
                        else if (request.Sale.InvoiceType == InvoiceType.ProformaInvoice)
                        {
                            request.Sale.DocumentType = DocumentType.ProformaInvoice;

                        }
                        else if (request.Sale.InvoiceType == InvoiceType.PurchaseOrder)
                        {
                            request.Sale.DocumentType = DocumentType.PurchaseOrder;

                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit)
                        {
                            request.Sale.DocumentType = DocumentType.SaleInvoice;

                        }

                        if (request.Sale.IsWarranty)
                        {
                            if (!string.IsNullOrEmpty(request.Sale.WarrantyLogoPath))
                            {
                                if (request.Sale.WarrantyLogoPath.Contains("Attachment"))
                                {
                                    request.Sale.WarrantyLogoPath = GetBaseImage(request.Sale.WarrantyLogoPath).Result;
                                }

                            }
                            else
                            {
                                request.Sale.WarrantyLogoPath = "";
                            }
                        }
                        else if (!string.IsNullOrEmpty(request.Sale.WarrantyLogoPath))
                        {
                            if (request.Sale.WarrantyLogoPath.Contains("Attachment"))
                            {
                                request.Sale.WarrantyLogoPath = GetBaseImage(request.Sale.WarrantyLogoPath).Result;
                            }

                        }
                        request.IsPayment = false;
                        // Find Auto invoice No
                        var invoiceNo = await _mediator.Send(new GetSaleRegistrationNoQuery
                        {
                            TerminalId = request.Sale.TerminalId,
                            InvoicePrefix = request.Sale.InvoicePrefix,
                            UserId = _contextProvider.GetUserId().ToString(),
                            BranchId = request.Sale.BranchId,
                        }, cancellationToken);
                        var message = new Message();

                        string invoiceNumber;
                        if (request.Sale.DocumentType == DocumentType.SaleInvoice)
                        {
                            invoiceNumber = invoiceNo.Paid;
                        }
                        else if (request.Sale.DocumentType == DocumentType.GlobalInvoice)
                        {
                            invoiceNumber = invoiceNo.GlobalInvoice;
                        }
                        else if (request.Sale.DocumentType == DocumentType.ReceiptInvoice)
                        {
                            invoiceNumber = invoiceNo.ReceiptInvoice;
                        }
                        else if (request.Sale.DocumentType == DocumentType.ProformaInvoice || request.Sale.DocumentType == DocumentType.ServiceProformaInvoice)
                        {
                            invoiceNumber = invoiceNo.ProformaInvoice;
                        }
                        else if (request.Sale.DocumentType == DocumentType.PurchaseOrder || request.Sale.DocumentType == DocumentType.PurchaseOrder)
                        {
                            invoiceNumber = invoiceNo.PurchaseOrder;
                        }
                        else
                        {
                            invoiceNumber = invoiceNo.SaleReturn;
                        }


                        PartiallyInvoices partiallyInvoices = new PartiallyInvoices();
                        if (request.Sale.InvoiceType == InvoiceType.Paid)
                        {
                            request.Sale.ApprovalDate = DateTime.UtcNow;
                            partiallyInvoices = PartiallyInvoices.Fully;
                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Hold)
                        {
                            invoiceNumber = invoiceNo.Hold;
                            request.Sale.DocumentType = DocumentType.SaleInvoiceHold;
                        }
                        else if (request.Sale.InvoiceType == InvoiceType.Credit)
                        {
                            request.Sale.ApprovalDate = DateTime.UtcNow;
                            partiallyInvoices = PartiallyInvoices.UnPaid;
                        }


                        var checkInvoiceStatus = request.Sale.SaleItems.Count(x => x.Quantity >= 0);
                        var checkInvoiceStatusReturn = request.Sale.SaleItems.Count(x => x.Quantity < 0);
                        if (checkInvoiceStatus > 0)
                        {

                            // Add Invoice
                            var sale = new Sale()
                            {
                                PickUpDate = request.Sale.PickUpDate,
                                DispatchDate = request.Sale.DispatchDate,
                                InquiryId = request.Sale.InquiryId,
                                ProformaId = request.Sale.ProformaId,
                                PurchaseOrderId = request.Sale.PurchaseOrderId,
                                DeliveryNoteAndDate = request.Sale.DeliveryNoteAndDate,
                                QuotationValidUpto = request.Sale.QuotationValidUpto,
                                PerfomaValidUpto = request.Sale.PerfomaValidUpto,
                                DeliveryChallanId = request.Sale.DeliveryChallanId,
                                Date = request.Sale.Date,
                                IsCashCustomer = request.Sale.IsCashCustomer,
                                DocumentType = request.Sale.DocumentType,
                                IsWarranty = request.Sale.IsWarranty,
                                TaxMethod = request.Sale.TaxMethod,
                                SaleOrderNo = request.Sale.SaleOrderNo,
                                QuotationNo = request.Sale.QuotationNo,
                                PeroformaInvoiceNo = request.Sale.PeroformaInvoiceNo,
                                PurchaseOrderNo = request.Sale.PurchaseOrderNo,
                                InquiryNo = request.Sale.InquiryNo,
                                PoNumber = request.Sale.PoNumber,
                                CustomerInquiry = request.Sale.CustomerInquiry,
                                DeliveryAddress = request.Sale.DeliveryAddress,
                                DeliveryDate = request.Sale.DeliveryDate,
                                BillingAddress = request.Sale.BillingAddress,
                                ReferredBy = request.Sale.ReferredBy,
                                DeliveryId = request.Sale.DeliveryId,
                                ShippingId = request.Sale.ShippingId,
                                BillingId = request.Sale.BillingId,
                                NationalId = request.Sale.NationalId,
                                PoDate = request.Sale.PoDate,
                                TaxRateId = request.Sale.TaxRateId,
                                BarCode = request.Sale.BarCode,
                                WareHouseId = request.Sale.WareHouseId,
                                RegistrationNo = invoiceNumber,
                                PartiallyInvoices = partiallyInvoices,
                                InvoiceType = request.Sale.InvoiceType,
                                IsCredit = request.Sale.IsCredit,
                                EmployeeId = request.Sale.EmployeeId,
                                AreaId = request.Sale.AreaId,
                                Change = request.Sale.Change,
                                Note = request.Sale.Note,
                                IsOverWrite = request.Sale.IsOverWrite,
                                DoctorName = request.Sale.DoctorName,
                                HospitalName = request.Sale.HospitalName,
                                RefrenceNo = request.Sale.RefrenceNo,
                                CustomerAddressWalkIn = request.Sale.CustomerAddressWalkIn,
                                SaleOrderId = request.Sale.SaleOrderId,
                                QuotationId = request.Sale.QuotationId,
                                IsPurchaseOrder = request.Sale.IsPurchaseOrder,

                                LogisticId = request.Sale.LogisticId,
                                Mobile = request.Sale.Mobile,
                                UnRegisteredVatId = request.Sale.UnRegisteredVatId,
                                UnRegisteredVAtAmount = request.Sale.UnRegisteredVAtAmount,
                                PeriodId = period.Id,
                                Description = request.Sale.Description,
                                IsService = request.Sale.IsService,
                                TermAndCondition = request.Sale.TermAndCondition,
                                TermAndConditionAr = request.Sale.TermAndConditionAr,
                                WarrantyLogoPath = request.Sale.WarrantyLogoPath,
                                Discount = request.Sale.Discount,
                                TerminalId = !string.IsNullOrEmpty(request.Sale.TerminalId) ? Guid.Parse(request.Sale.TerminalId) : null,
                                IsBeforeTax = request.Sale.IsBeforeTax,
                                TransactionLevelDiscount = request.Sale.TransactionLevelDiscount,
                                IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction,
                                IsFixed = request.Sale.IsFixed,
                                DueDate = request.Sale.DueDate,
                                VatAmount = request.Sale.VatAmount,
                                DiscountAmount = request.Sale.DiscountAmount,
                                TotalAfterDiscount = request.Sale.TotalAfterDiscount,
                                TotalAmount = request.Sale.TotalAmount,
                                TotalWithOutAmount = request.Sale.GrossAmount,
                                IsVatChange = true,
                                SaleHoldTypes = request.Sale.SaleHoldTypes,
                                CreatedBy = _contextProvider.GetUserId() ?? Guid.Empty,
                                BranchId = request.Sale.BranchId,
                                SaleItems = request.Sale.SaleItems.Select(x =>
                                    new SaleItem()
                                    {
                                        DocumentDate = request.Sale.Date,
                                        ProductId = x.ProductId,
                                        SoItemId = x.ItemId,
                                        TaxRateId = x.TaxRateId == Guid.Empty ? throw new ApplicationException("TaxRate is not null") : x.TaxRateId,
                                        IsFree = x.IsFree,
                                        TaxMethod = x.TaxMethod ?? throw new ApplicationException("TaxMethod is not null"),
                                        Discount = x.Discount,
                                        FixDiscount = x.FixDiscount,
                                        UnitName = x.UnitName,
                                        Quantity = x.Quantity,
                                        UnitPrice = x.UnitPrice,
                                        WareHouseId = request.Sale.WareHouseId,
                                        RemainingQuantity = x.Quantity,
                                        OfferQuantity = x.OfferQuantity,
                                        VatAmount = x.VatAmount,
                                        DiscountAmount = x.DiscountAmount,
                                        TotalAmount = x.TotalAmount,
                                        TotalWithOutAmount = x.GrossAmount,
                                        Buy = x.Buy,
                                        Get = x.Get,
                                        BundleId = x.BundleId,
                                        SerialNo = x.SerialNo,
                                        PromotionItemId = x.PromotionId,
                                        PromotionOfferQuantity = x.PromotionOfferQuantity,
                                        ServiceItem = x.ServiceItem,
                                        Description = x.Description,
                                        Serial = x.Serial,
                                        GuaranteeDate = x.GuaranteeDate,
                                        BatchExpiry = x.BatchExpiry,
                                        BatchNo = x.BatchNo,
                                        AutoNumber = x.AutoNumber,
                                        ColorId = x.ColorId,
                                        StyleNumber = x.StyleNumber,
                                        Scheme = x.Scheme,
                                        SchemePhysicalQuantity = x.SchemePhysicalQuantity,
                                        SchemeQuantity = x.SchemeQuantity,
                                        SaleSizeAssortments = x.SaleSizeAssortment?.Select(size => new SaleSizeAssortment()
                                        {
                                            SizeId = size.SizeId,
                                            Quantity = size.Quantity
                                        }).ToList()
                                    }).ToList()
                            };
                            if (request.Sale.QuotationId != null)
                            {
                                var saleOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.Sale.QuotationId, cancellationToken: cancellationToken);
                                if (saleOrder != null)
                                {
                                    sale.InvoiceNote = saleOrder.RegistrationNo + " - " + saleOrder.Date.ToString("dd/MM/yyyy");
                                }
                            }
                            else if (request.Sale.SaleOrderId != null)
                            {
                                var saleOrder = await _context.SaleOrders.FirstOrDefaultAsync(x => x.Id == request.Sale.SaleOrderId, cancellationToken: cancellationToken);
                                if (saleOrder != null)
                                {
                                    sale.InvoiceNote = saleOrder.RegistrationNo + " - " + saleOrder.Date.ToString("dd/MM/yyyy");
                                }
                            }
                            else if (request.Sale.ProformaId != null)
                            {
                                var proformaInvoice = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.ProformaId, cancellationToken: cancellationToken);
                                if (proformaInvoice != null)
                                {
                                    sale.InvoiceNote = proformaInvoice.RegistrationNo + " - " + proformaInvoice.Date.ToString("dd/MM/yyyy");
                                    proformaInvoice.IsProcessed = true;
                                    proformaInvoice.IsClose = true;
                                    proformaInvoice.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                    _context.Sales.Update(proformaInvoice);
                                }

                            }
                            else if (request.Sale.PurchaseOrderId != null)
                            {
                                var proformaInvoice = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.PurchaseOrderId, cancellationToken: cancellationToken);
                                if (proformaInvoice != null)
                                {
                                    proformaInvoice.IsProcessed = true;
                                    proformaInvoice.IsClose = true;
                                    proformaInvoice.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                    _context.Sales.Update(proformaInvoice);
                                    sale.InvoiceNote = proformaInvoice.RegistrationNo + " - " + proformaInvoice.Date.ToString("dd/MM/yyyy");
                                }

                            }


                            if (request.Sale.DeliveryChallanId != null)
                            {
                                var deliveryChallanrEc = _context.DeliveryChallanReserveds
                                    .AsNoTracking()
                                    .Include(x => x.DeliveryChallanReserverdItems)
                                    .FirstOrDefault(x => x.DeliveryChallanId == request.Sale.DeliveryChallanId);

                                var challan = _context.DeliveryChallans.AsNoTracking().FirstOrDefault(x => x.Id == request.Sale.DeliveryChallanId);


                                if (deliveryChallanrEc != null)
                                {
                                    foreach (var reservd in deliveryChallanrEc.DeliveryChallanReserverdItems)
                                    {
                                        foreach (var x in request.Sale.SaleItems)
                                        {
                                            if (reservd.ProductId != null)
                                            {
                                                if (x.ProductId == reservd.ProductId)
                                                {
                                                    if (reservd.Quantity > 0)
                                                    {
                                                        reservd.Quantity -= x.Quantity;
                                                    }
                                                }
                                            }

                                            if (x.ServiceProductId == reservd.ServiceProductId)
                                            {
                                                if (reservd.Quantity > 0)
                                                {
                                                    reservd.Quantity -= x.Quantity;
                                                }
                                            }
                                        }
                                    }
                                    deliveryChallanrEc.IsClose = deliveryChallanrEc.DeliveryChallanReserverdItems.All(x => x.Quantity <= 0);
                                    _context.DeliveryChallanReserveds.Update(deliveryChallanrEc);
                                    if (deliveryChallanrEc.IsClose)
                                    {
                                        challan.IsClose = true;
                                        challan.IsProcessed = true;
                                        challan.Reason = "Issued by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                        _context.DeliveryChallans.Update(challan);
                                    }
                                }
                                sale.InvoiceNote = challan.RegistrationNo + " - " + challan.Date.ToString("dd/MM/yyyy");
                            }

                            if (request.Sale.ProformaInvoiceId != null)
                            {
                                var proformaInvoice = await _context.Sales.FindAsync(request.Sale.ProformaInvoiceId);
                                if (proformaInvoice != null)
                                {
                                    proformaInvoice.IsProformaInvoice = true;
                                }
                            }

                            if (request.Sale.OtherCurrency.CurrencyId != Guid.Empty)
                            {
                                if (request.Sale.OtherCurrency.CurrencyId != null)
                                    sale.OtherCurrency = new OtherCurrency()
                                    {
                                        CurrencyId = request.Sale.OtherCurrency.CurrencyId.Value,
                                        Amount = request.Sale.OtherCurrency.Amount,
                                        Rate = request.Sale.OtherCurrency.Rate
                                    };
                            }

                            // Add Customer Detail against invoice
                            if (request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null)
                            {
                                //if (request.Sale.DueDate == default && request.Sale.DueDate == null)
                                //    throw new ApplicationException("Due date is required.");

                                sale.CashCustomerId = null;
                                sale.CustomerId = request.Sale.CustomerId;
                                sale.DueDate = request.Sale.DueDate;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(request.Sale.CashCustomer))
                                    throw new ApplicationException("Cash Customer is required.");

                                var cashCustomerExist = await _context.CashCustomer.AsNoTracking().FirstOrDefaultAsync(x =>
                                    x.Name.ToLower() == request.Sale.CashCustomer.ToLower(), cancellationToken: cancellationToken);
                                if (cashCustomerExist == null)
                                {
                                    string code;
                                    var autoCode = await _context.CashCustomer.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                                    if (autoCode != null)
                                    {
                                        string fetchNo = autoCode.Code.Substring(3);
                                        Int32 autoNo = Convert.ToInt32((fetchNo));
                                        var format = "00000";
                                        autoNo++;
                                        var prefix = "CC-";
                                        var newCode = prefix + autoNo.ToString(format);
                                        code = newCode;
                                    }
                                    else
                                    {
                                        code = "CC-00001";
                                    }
                                    sale.CashCustomer = new CashCustomer
                                    {
                                        Name = request.Sale.CashCustomer,
                                        Mobile = request.Sale.Mobile,
                                        Code = code,
                                        Email = request.Sale.Email,
                                        CustomerId = request.Sale.CashCustomerId,
                                        Address = request.Sale.CustomerAddressWalkIn
                                    };
                                }
                                else
                                {
                                    sale.CashCustomerId = cashCustomerExist.Id;
                                    sale.CustomerId = null;
                                }
                            }

                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            await _context.Sales.AddAsync(sale, cancellationToken);


                            //Product Variation
                            if ((request.Sale.InvoiceType == InvoiceType.Paid || (request.Sale.IsCredit && request.Sale.InvoiceType == InvoiceType.Credit)) && request.Sale.ColorVariants)
                            {
                                foreach (var item in request.Sale.SaleItems.Where(x => !x.ServiceItem))
                                {
                                    foreach (var size in item.SaleSizeAssortment.Where(size => size.Quantity > 0))
                                    {
                                        var variation = await _context.VariationInventories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.ColorId == item.ColorId && x.SizeId == size.SizeId, cancellationToken: cancellationToken);
                                        if (variation == null)
                                        {
                                            var variationInventory = new VariationInventory()
                                            {
                                                ProductId = item.ProductId.Value,
                                                ColorId = item.ColorId,
                                                SizeId = size.SizeId,
                                                Quantity = size.Quantity * -1
                                            };
                                            await _context.VariationInventories.AddAsync(variationInventory, cancellationToken);
                                        }
                                        else
                                        {
                                            variation.Quantity -= size.Quantity;
                                            _context.VariationInventories.Update(variation);
                                        }

                                        var variationInventoryForReporting = new VariationInventoryForReporting()
                                        {
                                            DocumentId = sale.Id,
                                            TransactionType = TransactionType.SaleInvoice,
                                            ProductId = item.ProductId.Value,
                                            ColorId = item.ColorId.Value,
                                            SizeId = size.SizeId,
                                            Quantity = size.Quantity
                                        };
                                        await _context.VariationInventoryForReportings.AddAsync(variationInventoryForReporting, cancellationToken);
                                    }
                                }
                            }

                            //Add Attachments
                            if (request.Sale.AttachmentList is { Count: > 0 })
                            {
                                foreach (var item in request.Sale.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = sale.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);
                                }
                            }


                            //UpdateSale Quotation Record
                            if ((request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit) && request.Sale.QuotationId != null)
                            {
                                var quotation = await _context.SaleOrders.FindAsync(request.Sale.QuotationId);
                                if (request.Sale.IsService)
                                {
                                    foreach (var item in request.Sale.SaleItems)
                                    {
                                        var soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.Id == item.ItemId);
                                        if (soItemDetail != null)
                                        {
                                            soItemDetail.QuantityOut -= item.Quantity;
                                            _context.SaleOrderItems.Update(soItemDetail);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var item in request.Sale.SaleItems)
                                    {
                                        var soItemDetail = quotation.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                        if (soItemDetail != null)
                                        {
                                            soItemDetail.QuantityOut -= item.Quantity;
                                            _context.SaleOrderItems.Update(soItemDetail);
                                        }
                                    }
                                }

                                var close = quotation.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.Sale.QuotationId && x.QuantityOut > 0);
                                if (close == null || !request.Sale.PartiallyQuotation)
                                {
                                    quotation.IsClose = true;
                                    quotation.IsProcessed = true;
                                    quotation.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                }
                            }


                            //UpdateSale SaleOrder Record
                            SaleOrder soItem = null;
                            decimal totalAmount = 0;
                            if ((request.Sale.InvoiceType == InvoiceType.Paid || request.Sale.InvoiceType == InvoiceType.Credit) && request.Sale.SaleOrderId != null)
                            {
                                soItem = await _context.SaleOrders.FindAsync(request.Sale.SaleOrderId);

                                if (soItem == null)
                                    throw new NotFoundException("Sale Order", "Was not found");

                                if (request.Sale.IsService)
                                {
                                    foreach (var item in request.Sale.SaleItems)
                                    {
                                        var soItemDetail = soItem.SaleOrderItems.FirstOrDefault(x => x.Id == item.ItemId);
                                        if (soItemDetail != null)
                                        {
                                            soItemDetail.QuantityOut -= item.Quantity;
                                            _context.SaleOrderItems.Update(soItemDetail);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var item in request.Sale.SaleItems)
                                    {
                                        var soItemDetail = soItem.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId && x.BatchNo == item.BatchNo);
                                        if (soItemDetail != null)
                                        {
                                            soItemDetail.QuantityOut -= item.Quantity;
                                            _context.SaleOrderItems.Update(soItemDetail);
                                        }
                                    }
                                }

                                var close = soItem.SaleOrderItems.FirstOrDefault(x => x.SaleOrderId == request.Sale.SaleOrderId && x.QuantityOut > 0);
                                if (close == null || !request.Sale.PartiallySaleOrder)
                                {
                                    soItem.IsClose = true;
                                    soItem.IsProcessed = true;
                                    soItem.Reason = "Processed by Sale" + "-" + sale.RegistrationNo + "-" + sale.Date.ToString("dd/MM/yyyy");
                                }
                                //Payment Voucher Update

                                var totalPay = soItem.SaleOrderPayments.Sum(x => x.Amount);
                                var result = request.Sale.SalePayment.DueAmount - (totalPay - request.Payment);
                                totalAmount = totalPay - request.Payment;

                                if (request.Sale.AutoPaymentVoucher == "true" && request.Sale.IsCredit)
                                {
                                    await _mediator.Send(new AddPaymentVoucherCommand
                                    {
                                        SaleInvoice = sale.Id,
                                        ContactAccountId = soItem.Customer.AccountId.Value,
                                        ContactAdvanceAccountId = soItem.Customer.AdvanceAccountId.Value,
                                        Payment = request.Payment,
                                        DueAmount = request.Sale.SalePayment.DueAmount,
                                        SaleOrderPayments = soItem.SaleOrderPayments,
                                        IsCredit = request.Sale.IsCredit,
                                        BankCashAccountId = request.Sale.BankCashAccountId,
                                        PaymentType = SalePaymentType.Advance,
                                        UserName = request.Sale.UserName,
                                        BranchId = request.Sale.BranchId,
                                    }, cancellationToken);

                                    if (request.Sale.IsCredit)
                                    {
                                        if (result <= 0)
                                        {
                                            sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                            var saleAdvancePayment = new SaleInvoiceAdvancePayment()
                                            {
                                                Amount = request.Sale.SalePayment.DueAmount,
                                                Date = DateTime.UtcNow,
                                                SaleInvoiceId = sale.Id,
                                                SaleOrderId = sale.SaleOrderId,
                                            };
                                            await _context.SaleInvoiceAdvancePayments.AddAsync(saleAdvancePayment, cancellationToken);
                                        }
                                        else if (totalPay - request.Payment > 0)
                                        {
                                            sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                            var saleAdvancePayment = new SaleInvoiceAdvancePayment()
                                            {
                                                Amount = totalPay - request.Payment,
                                                Date = DateTime.UtcNow,
                                                SaleInvoiceId = sale.Id,
                                                SaleOrderId = sale.SaleOrderId,
                                            };
                                            await _context.SaleInvoiceAdvancePayments.AddAsync(saleAdvancePayment, cancellationToken);
                                        }
                                    }
                                }
                            }

                            //generate cashVoucher for customer
                            if (request.Sale.IsCashVoucher)
                            {
                                var autoCashVoucher = await _mediator.Send(new CashVoucherAutoNoQuery(), cancellationToken);
                                var cashVoucher = new CashVoucher()
                                {
                                    VoucherNo = autoCashVoucher,
                                    Date = request.Sale.Date,
                                    Amount = Math.Abs(request.Sale.SalePayment.DueAmount),
                                    SaleReturnId = sale.Id
                                };
                                await _context.CashVouchers.AddAsync(cashVoucher, cancellationToken);
                            }

                            //if sale invoice contain payment details
                            if (request.Sale.InvoiceType == InvoiceType.Paid)
                            {
                                await SaveSalePaymentAsync(sale, request, invoiceNo);
                            }


                            if (request.Sale.InvoiceType == InvoiceType.Hold)
                            {
                                var salePayments = new List<SalePayment>();
                                if (request.Sale.CreditPays != null)
                                {
                                    foreach (var credit in request.Sale.CreditPays)
                                    {

                                        var salePayment = new SalePayment
                                        {
                                            DueAmount = credit.Amount,
                                            Received = credit.Amount,
                                            Balance = 0,
                                            Change = 0,
                                            PaymentType = credit.PaymentMode == "Cash" ? SalePaymentType.Cash : credit.PaymentMode == "Bank" ? SalePaymentType.Bank : SalePaymentType.Default,
                                            BankCashAccountId = credit.AccountId,
                                            Name = credit.PaymentMode,
                                            SaleId = sale.Id,
                                            Rate = 0,
                                            Amount = 0,
                                            Description = credit.Description
                                        };
                                        salePayments.Add(salePayment);
                                    }

                                    await _context.SalePayments.AddRangeAsync(salePayments, cancellationToken);
                                }
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = sale.BranchId,
                                Code = _code,
                                DocumentNo = sale.RegistrationNo
                            }, cancellationToken);

                            //Save changes to database
                            try
                            {
                                await _context.SaveChangesAsync(cancellationToken);
                            }
                            catch (NotFoundException exception)
                            {
                                _logger.LogError(exception.Message);
                                throw new NotFoundException(exception.Message, "");
                            };

                            //Add Transaction according to concerns accounts
                            if (request.Sale.InvoiceType == InvoiceType.Paid || (request.Sale.IsCredit && request.Sale.InvoiceType == InvoiceType.Credit))
                            {
                                await AddTransaction(sale.Id, request.CounterId, request.Sale.DayStart,
                                    request.Sale.SaleOrderId, request.Sale.SoInventoryReserve, request.InvoiceWoInventory, soItem, totalAmount,
                                    request.Sale.AutoPaymentVoucher, request.Sale, request.Sale.IsFifo, period);
                            }
                            scope.Complete();

                            message.Id = sale.Id;
                            request.SaleId = sale.Id;
                            message.IsAddUpdate = "Data has been added successfully";
                            request.IsPayment = true;
                        }
                        if (checkInvoiceStatusReturn > 0)
                        {
                            request.Sale.Id = request.Sale.SaleInvoiceId.Value;
                            request.Sale.InvoiceType = InvoiceType.SaleReturn;
                            request.Sale.IsSaleReturnPost = true;
                            request.Sale.RegistrationNo = invoiceNo.SaleReturn;

                            message = await _mediator.Send(new AddSaleReturnCommand()
                            {
                                Sale = request.Sale,
                                CounterId = request.CounterId,
                                FromTouchInvoice = true,
                                NewSaleId = request.SaleId,
                                IsPayment = request.IsPayment,
                                InvoiceAmount = request.Sale.SalePayment.DueAmount,
                                InvoiceNumber = invoiceNumber
                            }, cancellationToken);
                        }

                        if (checkInvoiceStatus > 0 && checkInvoiceStatusReturn > 0)
                        {
                            message.IsDoublePrint = true;
                            message.SaleId = request.SaleId;
                        }

                        //Emails

                        
                        return message;
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new Exception(exception.Message);
                }
                finally
                {
                    Requests.IsDuplicateSale = false;
                }


            }

            private async Task SaveSalePaymentAsync(Sale sale, AddUpdateSaleCommand request, RegistrationNoLookUp invoiceNo)
            {
                try
                {
                    var salePayments = new List<SalePayment>();
                    //Get Payment number
                    sale.RegistrationNo = invoiceNo.Paid;
                    //if there are multiple payment details
                    foreach (var paymentMethod in request.Sale.SalePayment.PaymentTypes)
                    {
                        var salePayment = new SalePayment
                        {
                            DueAmount = request.Sale.SalePayment.DueAmount,
                            Received = paymentMethod.Amount,
                            Balance = paymentMethod.PaymentType == SalePaymentType.Bank ? 0 : request.Sale.SalePayment.Balance,
                            Change = paymentMethod.PaymentType == SalePaymentType.Bank ? 0 : request.Sale.SalePayment.Change,
                            PaymentType = paymentMethod.PaymentType,
                            BankCashAccountId = paymentMethod.BankCashAccountId,
                            Name = paymentMethod.Name,
                            SaleId = sale.Id,
                            Rate = paymentMethod.Rate,
                            Amount = paymentMethod.OtherAmount
                        };
                        if (paymentMethod.PaymentType == SalePaymentType.CashVoucher)
                        {
                            var cashVoucher = await _context.CashVouchers.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == request.Sale.VoucherNo && !x.IsActive);
                            if (cashVoucher == null)
                                throw new NotFoundException("Cash Voucher", request.Sale.VoucherNo);
                            cashVoucher.IsActive = true;
                            cashVoucher.SaleInvoiceId = sale.Id;
                            _context.CashVouchers.Update(cashVoucher);
                        }
                        salePayments.Add(salePayment);
                    }
                    await _context.SalePayments.AddRangeAsync(salePayments);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new Exception(e.Message);
                }
            }
            private async Task AddTransaction(Guid id, string counterId, bool dayStart, Guid? saleOrderId, string soInventoryReserve, bool invoiceWoInventory,
                           SaleOrder soItem, decimal totalAmount, string autoPaymentVoucher, SaleLookupModel request, bool isFifo, CompanySubmissionPeriod period)
            {

                var sale = await _context.Sales
                      .Include(x => x.UnRegisteredVAT)
                      .Include(x => x.SalePayments)
                      .Include(x => x.Customer)
                      .Include(x => x.SaleItems)
                      .ThenInclude(x => x.TaxRate)
                      .Include(x => x.SaleItems)
                      .ThenInclude(x => x.Product)
                      .ThenInclude(x => x.Category)
                      .FirstOrDefaultAsync(x => x.Id == id);


                var transactions = new List<Domain.Entities.Transaction>();
                var ledgerTransactions = new List<LedgerTransaction>();

                var accounts = await _context.Accounts.Where(x =>
                    x.Code == "25000001" ||
                    x.Code == "42600001" ||
                    x.Code == "10100001" ||
                    x.Code == "10500001" ||
                    x.Code == "60505001" ||
                    x.Code == "42000001" ||
                    x.Code == "60000101" ||
                    x.Code == "11100001" ||
                    x.Code == "TR-00001" ||
                    x.Code == "CC-00001"
                ).ToListAsync();

                var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                decimal inventoryAccount = 0;
                decimal cogsAccount = 0;
                decimal saleAccount = 0;




                //Find stock for Inventory transaction
                var stocks = _context.Stocks.AsQueryable();
                foreach (var item in sale.SaleItems)
                {
                    item.SchemePhysicalQuantity ??= 0;
                    if (item.ProductId != null)
                    {
                        var stock = sale.BranchId == null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == sale.WareHouseId) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == sale.WareHouseId && x.BranchId == sale.BranchId);

                        if (!(soInventoryReserve == "true" && saleOrderId != null && saleOrderId != Guid.Empty))
                        {
                            if (stock == null)
                            {
                                var newStock = new Stock()
                                {
                                    ProductId = item.ProductId.Value,
                                    WareHouseId = sale.WareHouseId,
                                    CurrentQuantity = -(item.SchemePhysicalQuantity == null ? item.Quantity : (decimal)item.SchemePhysicalQuantity + item.Quantity),
                                    BranchId = sale.BranchId,
                                };
                                await _context.Stocks.AddAsync(newStock);
                                stock = newStock;
                            }
                            else
                            {
                                stock.CurrentQuantity -= (item.SchemePhysicalQuantity == null ? item.Quantity : (decimal)item.SchemePhysicalQuantity + item.Quantity);
                                stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                if (!request.IsFifo && !request.ColorVariants)
                                {
                                    _context.Stocks.Update(stock);
                                }
                            }
                        }




                        //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                        //{
                        //    ProductId = item.ProductId.Value,
                        //    WareHouseId = item.WareHouseId
                        //});


                        if (!item.ServiceItem)
                        {
                            Inventory batchInventory = null;


                            if (soInventoryReserve == "true")
                            {
                                if (saleOrderId == null || saleOrderId == Guid.Empty)
                                {
                                    if (isFifo)
                                    {
                                        batchInventory = _context.Inventories.FirstOrDefault(x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                        if (batchInventory != null)
                                        {
                                            if (batchInventory.RemainingQuantity > item.Quantity)
                                            {
                                                batchInventory.RemainingQuantity -= item.Quantity;
                                            }
                                            else if (batchInventory.RemainingQuantity == item.Quantity)
                                            {
                                                batchInventory.RemainingQuantity = 0;
                                                batchInventory.IsActive = false;
                                                batchInventory.IsOpen = false;
                                            }
                                            else
                                            {


                                                var batchInventoryForRemaining = _context.Inventories.FirstOrDefault(x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                                batchInventoryForRemaining.RemainingQuantity -= (item.Quantity - batchInventory.RemainingQuantity);

                                                var inv = new Inventory()
                                                {
                                                    Date = sale.Date,
                                                    DocumentId = sale.Id,
                                                    DocumentNumber = sale.RegistrationNo,
                                                    Quantity = item.Quantity - batchInventory.RemainingQuantity,
                                                    ProductId = item.ProductId.Value,
                                                    StockId = stock.Id,
                                                    WareHouseId = sale.WareHouseId,
                                                    PromotionId = item.PromotionId,
                                                    BundleId = item.BundleId,
                                                    OfferQuantity = item.OfferQuantity,
                                                    Buy = item.Buy,
                                                    Get = item.Get,
                                                    TransactionType = TransactionType.SaleInvoice,
                                                    //Price = currentInventory.Price,
                                                    SalePrice = item.UnitPrice,
                                                    //AveragePrice = currentInventory.AveragePrice,
                                                    //ExpiryDate = currentInventory.ExpiryDate,
                                                    BatchNumber = batchInventoryForRemaining.BatchNumber,
                                                    AutoNumbering = batchInventoryForRemaining.AutoNumbering,
                                                    //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                    //CurrentStockValue = ((currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity)) * currentInventory.AveragePrice)
                                                    BranchId = sale.BranchId,
                                                };
                                                //currentInventory.CurrentQuantity = currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                                item.Quantity = batchInventory.RemainingQuantity;
                                                if (inv.CurrentQuantity == 0)
                                                {
                                                    batchInventoryForRemaining.RemainingQuantity = 0;
                                                    batchInventoryForRemaining.IsActive = false;
                                                    batchInventoryForRemaining.IsOpen = false;
                                                }
                                                batchInventory.RemainingQuantity = 0;
                                                batchInventory.IsActive = false;
                                                batchInventory.IsOpen = false;
                                                await _context.Inventories.AddAsync(inv);
                                            }
                                        }
                                    }

                                    var itemDetail = request.SaleItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                    if (request.IsSerial && itemDetail.IsSerial)
                                    {
                                        var serialArray = item.Serial.Split(',');
                                        for (int i = 0; i < item.Quantity; i++)
                                        {
                                            var inv = new Inventory()
                                            {
                                                Date = sale.Date,
                                                DocumentId = sale.Id,
                                                DocumentNumber = sale.RegistrationNo,
                                                Quantity = 1,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = sale.WareHouseId,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                OfferQuantity = item.OfferQuantity,
                                                Buy = item.Buy,
                                                Get = item.Get,
                                                Serial = serialArray[i],
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                BatchNumber = batchInventory?.BatchNumber,
                                                AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - (i + 1),
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - (i + 1)) * currentInventory.AveragePrice)
                                                BranchId = sale.BranchId,
                                            };
                                            await _context.Inventories.AddAsync(inv);
                                        }
                                    }
                                    else
                                    {
                                        var inv = new Inventory()
                                        {
                                            Date = sale.Date,
                                            DocumentId = sale.Id,
                                            DocumentNumber = sale.RegistrationNo,
                                            Quantity = item.Quantity,
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = sale.WareHouseId,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            OfferQuantity = item.OfferQuantity,
                                            Buy = item.Buy,
                                            Get = item.Get,
                                            Serial = item.Serial,
                                            TransactionType = TransactionType.SaleInvoice,
                                            //Price = currentInventory.Price,
                                            SalePrice = item.UnitPrice,
                                            //AveragePrice = currentInventory.AveragePrice,
                                            //ExpiryDate = currentInventory.ExpiryDate,
                                            BatchNumber = batchInventory?.BatchNumber,
                                            AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                            //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice)
                                            BranchId = sale.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(inv);
                                    }

                                }
                                else
                                {
                                    var soProduct = soItem.SaleOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId && x.BatchNo == item.BatchNo);
                                    if (soProduct != null)
                                    {
                                        var totalQuantity = item.Quantity - (item.Quantity + soProduct.QuantityOut);
                                        if (totalQuantity > 0)
                                        {
                                            if (stock == null)
                                            {
                                                var newStock = new Stock()
                                                {
                                                    ProductId = item.ProductId.Value,
                                                    WareHouseId = sale.WareHouseId,
                                                    CurrentQuantity = -totalQuantity,
                                                    BranchId = sale.BranchId,
                                                };
                                                await _context.Stocks.AddAsync(newStock);
                                                stock = newStock;
                                            }
                                            else
                                            {
                                                stock.CurrentQuantity -= totalQuantity;
                                                stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                                if (!request.IsFifo && !request.ColorVariants)
                                                {
                                                    _context.Stocks.Update(stock);
                                                }
                                            }

                                            var inv = new Inventory()
                                            {
                                                Date = sale.Date,
                                                DocumentId = sale.Id,
                                                DocumentNumber = sale.RegistrationNo,
                                                Quantity = totalQuantity,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = sale.WareHouseId,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                OfferQuantity = item.OfferQuantity,
                                                Buy = item.Buy,
                                                Get = item.Get,
                                                Serial = item.Serial,
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                BatchNumber = batchInventory?.BatchNumber,
                                                AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - totalQuantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - totalQuantity) * currentInventory.AveragePrice)
                                                BranchId = sale.BranchId,
                                            };
                                            await _context.Inventories.AddAsync(inv);
                                        }



                                    }
                                    else
                                    {
                                        if (stock == null)
                                        {
                                            var newStock = new Stock()
                                            {
                                                ProductId = item.ProductId.Value,
                                                WareHouseId = sale.WareHouseId,
                                                CurrentQuantity = -item.Quantity,
                                                BranchId = sale.BranchId,
                                            };
                                            await _context.Stocks.AddAsync(newStock);
                                            stock = newStock;
                                        }
                                        else
                                        {
                                            stock.CurrentQuantity -= item.Quantity;
                                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                            if (!request.IsFifo && !request.ColorVariants)
                                            {
                                                _context.Stocks.Update(stock);
                                            }
                                        }

                                        var inv = new Inventory()
                                        {
                                            Date = sale.Date,
                                            DocumentId = sale.Id,
                                            DocumentNumber = sale.RegistrationNo,
                                            Quantity = item.Quantity,
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = sale.WareHouseId,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            OfferQuantity = item.OfferQuantity,
                                            Buy = item.Buy,
                                            Get = item.Get,
                                            Serial = item.Serial,
                                            TransactionType = TransactionType.SaleInvoice,
                                            //Price = currentInventory.Price,
                                            SalePrice = item.UnitPrice,
                                            //AveragePrice = currentInventory.AveragePrice,
                                            //ExpiryDate = currentInventory.ExpiryDate,
                                            BatchNumber = batchInventory?.BatchNumber,
                                            AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                            //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice)
                                            BranchId = sale.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(inv);
                                    }
                                }
                            }
                            else
                            {
                                if (isFifo)
                                {
                                    batchInventory = _context.Inventories.FirstOrDefault(x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                    if (batchInventory != null)
                                    {
                                        if (batchInventory.RemainingQuantity > item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity = batchInventory.RemainingQuantity - item.Quantity;
                                        }
                                        else if (batchInventory.RemainingQuantity == item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity = 0;
                                            batchInventory.IsActive = false;
                                            batchInventory.IsOpen = false;
                                        }
                                        else
                                        {


                                            var batchInventoryForRemaining = _context.Inventories.FirstOrDefault(x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                            batchInventoryForRemaining.RemainingQuantity -= (item.Quantity - batchInventory.RemainingQuantity);

                                            var inv = new Inventory()
                                            {
                                                Date = sale.Date,
                                                DocumentId = sale.Id,
                                                DocumentNumber = sale.RegistrationNo,
                                                Quantity = item.Quantity - batchInventory.RemainingQuantity,
                                                ProductId = item.ProductId.Value,
                                                StockId = stock.Id,
                                                WareHouseId = sale.WareHouseId,
                                                PromotionId = item.PromotionId,
                                                BundleId = item.BundleId,
                                                OfferQuantity = item.OfferQuantity,
                                                Buy = item.Buy,
                                                Get = item.Get,
                                                TransactionType = TransactionType.SaleInvoice,
                                                //Price = currentInventory.Price,
                                                SalePrice = item.UnitPrice,
                                                //AveragePrice = currentInventory.AveragePrice,
                                                //ExpiryDate = currentInventory.ExpiryDate,
                                                BatchNumber = batchInventoryForRemaining.BatchNumber,
                                                AutoNumbering = batchInventoryForRemaining.AutoNumbering,
                                                //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity)) * currentInventory.AveragePrice)
                                                BranchId = sale.BranchId,
                                            };
                                            //currentInventory.CurrentQuantity = currentInventory.CurrentQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                            item.Quantity = batchInventory.RemainingQuantity;
                                            if (inv.CurrentQuantity == 0)
                                            {
                                                batchInventoryForRemaining.RemainingQuantity = 0;
                                                batchInventoryForRemaining.IsActive = false;
                                                batchInventoryForRemaining.IsOpen = false;
                                            }
                                            batchInventory.RemainingQuantity = 0;
                                            batchInventory.IsActive = false;
                                            batchInventory.IsOpen = false;
                                            await _context.Inventories.AddAsync(inv);
                                        }
                                    }
                                }

                                var itemDetail = request.SaleItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                if (request.IsSerial && itemDetail.IsSerial)
                                {
                                    var serialArray = item.Serial.Split(',');
                                    for (int i = 0; i < item.Quantity; i++)
                                    {
                                        var inv = new Inventory()
                                        {
                                            Date = sale.Date,
                                            DocumentId = sale.Id,
                                            DocumentNumber = sale.RegistrationNo,
                                            Quantity = 1,
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = sale.WareHouseId,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            OfferQuantity = item.OfferQuantity,
                                            Buy = item.Buy,
                                            Get = item.Get,
                                            Serial = serialArray[i],
                                            TransactionType = TransactionType.SaleInvoice,
                                            //Price = currentInventory.Price,
                                            SalePrice = item.UnitPrice,
                                            //AveragePrice = currentInventory.AveragePrice,
                                            //ExpiryDate = currentInventory.ExpiryDate,
                                            BatchNumber = batchInventory?.BatchNumber,
                                            AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                            //CurrentQuantity = currentInventory.CurrentQuantity - (i + 1),
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - (i + 1)) * currentInventory.AveragePrice)
                                            BranchId = sale.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(inv);
                                    }

                                }
                                else
                                {
                                    var inv = new Inventory()
                                    {
                                        Date = sale.Date,
                                        DocumentId = sale.Id,
                                        DocumentNumber = sale.RegistrationNo,
                                        Quantity = item.Quantity,
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = sale.WareHouseId,
                                        PromotionId = item.PromotionItemId,
                                        BundleId = item.BundleId,
                                        OfferQuantity = item.OfferQuantity,
                                        Buy = item.Buy,
                                        Get = item.Get,
                                        Serial = item.Serial,
                                        TransactionType = TransactionType.SaleInvoice,
                                        //Price = currentInventory.Price,
                                        SalePrice = item.UnitPrice,
                                        //AveragePrice = currentInventory.AveragePrice,
                                        //ExpiryDate = currentInventory.ExpiryDate,
                                        BatchNumber = batchInventory?.BatchNumber,
                                        AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                        //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                        //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice)
                                        BranchId = sale.BranchId,
                                    };
                                    await _context.Inventories.AddAsync(inv);

                                    if (item.SchemePhysicalQuantity is > 0)
                                    {
                                        var schemeInventory = new Inventory()
                                        {
                                            Date = sale.Date,
                                            DocumentId = sale.Id,
                                            DocumentNumber = sale.RegistrationNo,
                                            Quantity = (decimal)item.SchemePhysicalQuantity,
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = sale.WareHouseId,
                                            PromotionId = item.PromotionId,
                                            BundleId = item.BundleId,
                                            OfferQuantity = (decimal)item.SchemePhysicalQuantity,
                                            Buy = item.Buy,
                                            Get = item.Get,
                                            Serial = item.Serial,
                                            TransactionType = TransactionType.SaleInvoice,
                                            //Price = currentInventory.Price,
                                            SalePrice = item.UnitPrice,
                                            //AveragePrice = currentInventory.AveragePrice,
                                            //ExpiryDate = currentInventory.ExpiryDate,
                                            BatchNumber = batchInventory?.BatchNumber,
                                            AutoNumbering = batchInventory?.AutoNumbering ?? 0,
                                            //CurrentQuantity = currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - (decimal)item.SchemePhysicalQuantity) * currentInventory.AveragePrice)
                                            BranchId = sale.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(schemeInventory);
                                    }
                                }
                            }


                            inventoryAccount += Math.Abs(Math.Round(stock.AveragePrice * (item.Quantity + (decimal)item.SchemePhysicalQuantity), 4));
                            cogsAccount += Math.Abs(Math.Round(stock.AveragePrice * (item.Quantity + (decimal)item.SchemePhysicalQuantity), 4));
                            saleAccount += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                : (item.Quantity * item.UnitPrice), 4);

                            if (isPos)
                            {
                                //inventory Account
                                ledgerTransactions.Add(new LedgerTransaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = item.Product.InventoryAccountId,
                                    Debit = 0,
                                    Credit = Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4)),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });

                                //cost of good sale Account
                                ledgerTransactions.Add(new LedgerTransaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = item.Product.CogsAccountId,
                                    Credit = 0,
                                    Debit = Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4)),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });

                                if (item.SchemePhysicalQuantity is > 0)
                                {
                                    //inventory Account Scheme
                                    ledgerTransactions.Add(new LedgerTransaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = item.Product.InventoryAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(stock.AveragePrice * (decimal)item.SchemePhysicalQuantity, 4)),
                                        Description = TransactionType.SaleInvoice.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleInvoice,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });

                                    //cost of good sale Account Scheme
                                    ledgerTransactions.Add(new LedgerTransaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = item.Product.CogsAccountId,
                                        Credit = 0,
                                        Debit = Math.Abs(Math.Round(stock.AveragePrice * (decimal)item.SchemePhysicalQuantity, 4)),
                                        Description = TransactionType.SaleInvoice.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleInvoice,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });
                                }
                            }
                        }
                    }



                    if (!(item.ServiceItem && item.IsFree))
                    {
                        if (item.BundleId != null)
                        {
                            if (isPos)
                            {
                                ledgerTransactions.Add(new LedgerTransaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = item.Product.SaleAccountId,
                                    Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.UnitPrice * item.OfferQuantity)) * 100) / (100 + item.TaxRate.Rate) + (item.UnitPrice * item.OfferQuantity)
                                   : (item.Quantity * item.UnitPrice), 4),
                                    Debit = 0,

                                    //Credit = Math.Round(((item.Quantity * item.UnitPrice)) , 2),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });
                            }
                            //sale Account

                            // Discount
                            if (!sale.IsDiscountOnTransaction)
                            {
                                transactions.Add(new Domain.Entities.Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                    Credit = 0,
                                    Debit = Math.Abs(Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.UnitPrice * item.OfferQuantity * 100) / (100 + item.TaxRate.Rate) : (item.UnitPrice * item.OfferQuantity), 4)),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });
                            }
                            var bundleCategory = await _context.BundleCategories.FindAsync(item.BundleId);
                            if (bundleCategory != null)
                            {
                                bundleCategory.QuantityOut += item.OfferQuantity;
                                bundleCategory.IsActive = (bundleCategory.StockLimit - bundleCategory.QuantityOut) > 0;
                            }
                        }
                        else if (item.PromotionItemId != null)
                        {
                            //sale Account
                            if (isPos)
                            {
                                ledgerTransactions.Add(new LedgerTransaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = item.Product.SaleAccountId,
                                    Debit = 0,
                                    Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * (decimal)item.PromotionOfferQuantity * item.Discount) / 100 : item.FixDiscount * (decimal)item.PromotionOfferQuantity)) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * (decimal)item.PromotionOfferQuantity * item.Discount) / 100 : item.FixDiscount * (decimal)item.PromotionOfferQuantity)
                                   : (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * (decimal)item.PromotionOfferQuantity * item.Discount) / 100 : item.FixDiscount * (decimal)item.PromotionOfferQuantity)) + (item.Discount != 0 ? (item.UnitPrice * (decimal)item.PromotionOfferQuantity * item.Discount) / 100 : item.FixDiscount * (decimal)item.PromotionOfferQuantity)), 4),

                                    //Credit = Math.Round(((item.Quantity * item.UnitPrice)) , 2),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });
                            }

                            var promotionOffer = await _context.PromotionOfferItems.FindAsync(item.PromotionItemId);
                            if (promotionOffer.PromotionType == "BuyNGetNAnother")
                            {
                                if (promotionOffer != null)
                                {
                                    promotionOffer.QuantityOut += (item.PromotionOfferQuantity ?? 0);
                                    promotionOffer.IsActive = (promotionOffer.StockLimit - promotionOffer.QuantityOut) > 0;
                                    promotionOffer.PromotionOffer.IsActive = promotionOffer.PromotionOffer.PromotionOfferItems.Any(x => x.IsActive);
                                }
                            }
                            else if (promotionOffer.PromotionType == "BuyNGetNSameGroup" || promotionOffer.PromotionType == "BuyNGetNSameItem")
                            {
                                if (promotionOffer != null)
                                {
                                    promotionOffer.QuantityOut += item.OfferQuantity;
                                    promotionOffer.IsActive = (promotionOffer.StockLimit - promotionOffer.QuantityOut) > 0;
                                    promotionOffer.PromotionOffer.IsActive = promotionOffer.PromotionOffer.PromotionOfferItems.Any(x => x.IsActive);
                                }
                                // Discount
                                if (!sale.IsDiscountOnTransaction)
                                {
                                    transactions.Add(new Domain.Entities.Transaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                        Credit = 0,
                                        Debit = Math.Abs(Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.UnitPrice * item.OfferQuantity * 100) / (100 + item.TaxRate.Rate) : (item.UnitPrice * item.OfferQuantity), 4)),
                                        Description = TransactionType.SaleInvoice.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleInvoice,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });
                                }
                            }
                            else
                            {
                                if (promotionOffer != null)
                                {
                                    if (promotionOffer.DiscountType == "%")
                                    {
                                        promotionOffer.QuantityOut += ((item.PromotionOfferQuantity ?? 0) / promotionOffer.BaseQuantity);
                                        promotionOffer.IsActive = (promotionOffer.StockLimit - promotionOffer.QuantityOut) > 0;
                                        promotionOffer.PromotionOffer.IsActive = promotionOffer.PromotionOffer.PromotionOfferItems.Any(x => x.IsActive);
                                    }
                                    else
                                    {
                                        promotionOffer.QuantityOut += (item.PromotionOfferQuantity ?? 0);
                                        promotionOffer.IsActive = (promotionOffer.StockLimit - promotionOffer.QuantityOut) > 0;
                                        promotionOffer.PromotionOffer.IsActive = promotionOffer.PromotionOffer.PromotionOfferItems.Any(x => x.IsActive);
                                    }
                                }
                                // Discount
                                transactions.Add(new Domain.Entities.Transaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                    Credit = 0,
                                    Debit = Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * (decimal)item.PromotionOfferQuantity * item.Discount) / 100 : item.FixDiscount * (decimal)item.PromotionOfferQuantity), 4)),
                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });
                            }
                        }
                        else
                        {
                            //Discount Taken
                            if (!sale.IsDiscountOnTransaction)
                            {
                                if (Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100) : item.FixDiscount), 4)) != 0)
                                {
                                    transactions.Add(new Domain.Entities.Transaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                        Credit = 0,
                                        Debit = Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100) : item.FixDiscount), 4)),
                                        Description = TransactionType.SaleReturn.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleInvoice,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });
                                }


                            }


                            //sale Account
                            if (isPos)
                            {
                                ledgerTransactions.Add(new Domain.Entities.LedgerTransaction
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = sale.Date,
                                    ApprovalDate = request.ApprovalDate,
                                    ContactId = sale.CustomerId,
                                    AccountId = item.Product.SaleAccountId,
                                    Debit = 0,
                                    Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                   : (item.Quantity * item.UnitPrice), 4),

                                    Description = TransactionType.SaleInvoice.ToString(),
                                    DocumentId = sale.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = sale.RegistrationNo,
                                    Year = sale.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = sale.BranchId,
                                });
                            }

                        }
                    }

                    if (item.ServiceItem)
                    {
                        saleAccount += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                            : (item.Quantity * item.UnitPrice), 4);
                    }


                    if (request.IsFifo)
                    {
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        });

                        await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
                    }

                }


                //Inventory Account
                transactions.Add(new Domain.Entities.Transaction
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                    Debit = 0,
                    Credit = inventoryAccount,
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = Guid.Empty,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                //cost of good sale Account
                transactions.Add(new Domain.Entities.Transaction
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                    Credit = 0,
                    Debit = cogsAccount,
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = Guid.Empty,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                //Sale Account
                transactions.Add(new Domain.Entities.Transaction
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "42000001").Id,
                    Credit = saleAccount,
                    Debit = 0,

                    //Credit = Math.Round(((item.Quantity * item.UnitPrice)) , 2),
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = Guid.Empty,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                #region Transaction Level Discount

                var sumQuantity = sale.SaleItems.Sum(product => product.IsFree ? 0 : product.Quantity);

                var totalVat = sale.VatAmount;
                //var totalVat = sale.SaleItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? (((((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - ((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (sale.IsBeforeTax ? ((((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                //(((((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice) - prod.FixDiscount - ((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (sale.IsBeforeTax && !sale.IsFixed && sale.IsDiscountOnTransaction ? ((((prod.Quantity - prod.OfferQuantity) * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : (sale.IsBeforeTax && sale.IsFixed && sale.IsDiscountOnTransaction ? (sale.TransactionLevelDiscount / sumQuantity * (prod.Quantity - prod.OfferQuantity)) : 0)))) * prod.TaxRate.Rate) / 100));

                //vat
                transactions.Add(new Domain.Entities.Transaction
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "25000001").Id,
                    Debit = 0,
                    Credit = Math.Abs(Math.Round((totalVat), 4)),
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                var totalVatWithoutFree = sale.SaleItems.Sum(prod => prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (sale.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : 0)) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (sale.IsBeforeTax && !sale.IsFixed && sale.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : (sale.IsBeforeTax && sale.IsFixed && sale.IsDiscountOnTransaction ? (sale.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0))) * prod.TaxRate.Rate) / 100));

                // Discount
                if (sale.IsDiscountOnTransaction)
                {
                    var transactionLevelTotalDiscount = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity) - (x.UnitPrice * x.Quantity * x.TaxRate.Rate / (100 + x.TaxRate.Rate)))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                    if (transactionLevelTotalDiscount != 0)
                    {
                        transactions.Add(new Domain.Entities.Transaction
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                            Credit = 0,
                            Debit = Math.Abs(Math.Round((transactionLevelTotalDiscount), 4)),
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }
                }
                #endregion
                //x.Code == "10100001" || // Cash in Hand
                //x.Code == "10500001" || // Bank
                Guid terminalAccountId = default;
                Guid cashAccountId = default;
                if (dayStart)
                {
                    if (counterId != "")
                    {
                        var terminalId = Guid.Parse(counterId);
                        var terminalAccount = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == terminalId);
                        if (terminalAccount.AccountId != null)
                        {
                            terminalAccountId = terminalAccount.AccountId.Value;
                            cashAccountId = terminalAccount.CashAccountId.Value;
                        }

                    }

                }
                else
                {
                    var invoiceSetting = _context.PrintSettings.AsNoTracking().FirstOrDefault();
                    if (invoiceSetting != null)
                    {
                        terminalAccountId = invoiceSetting.BankAccountId ?? Guid.Empty;
                        cashAccountId = invoiceSetting.CashAccountId ?? Guid.Empty;
                    }
                }

                if (request.IsService)
                {
                    decimal total = 0;
                    var freeItem = sale.SaleItems.Where(x => x.IsFree && !x.ServiceItem);
                    foreach (var item in freeItem)
                    {
                        total += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount)
                            : (item.Quantity * item.UnitPrice), 2);

                        total += Math.Abs(Math.Round((((item.UnitPrice * (item.Quantity)) - ((item.Discount != 0 ? (item.UnitPrice * (item.Quantity) * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100)))) * item.TaxRate.Rate) / ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.TaxRate.Rate + 100) : 100), 2));
                    }

                    transactions.Add(new Domain.Entities.Transaction
                    {
                        Date = DateTime.UtcNow,
                        DocumentDate = sale.Date,
                        ApprovalDate = request.ApprovalDate,
                        ContactId = sale.CustomerId,
                        AccountId = accounts.FirstOrDefault(x => x.Code == "60505001").Id,
                        Debit = Math.Abs(Math.Round((total), 4)),
                        Credit = 0,
                        Description = TransactionType.SaleInvoice.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });
                }

                // Discount on Over all Sale
                if (sale.Discount != 0)
                {
                    var account = await _context.Accounts
    .AsNoTracking()
    .FirstOrDefaultAsync(x => x.Name == "Other Expense");
                    Guid accountId = account?.Id ?? Guid.Empty;
                    if (account == null)
                    {
                        var lastInventoryCode = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter).OrderBy(x => x.CostCenter.Name == "Expense Adjustment").LastOrDefaultAsync();

                        var newAccount = new Account
                        {
                            Code = (Convert.ToInt32(lastInventoryCode.Code) + 1).ToString(),
                            Name = "Other Expense",
                            Description = "Other Expense",
                            IsActive = true,
                            CostCenterId = lastInventoryCode.CostCenterId
                        };
                        await _context.Accounts.AddAsync(newAccount);
                        accountId = newAccount.Id;
                    }
                    transactions.Add(new Domain.Entities.Transaction
                    {
                        Date = DateTime.UtcNow,
                        ContactId = sale.CustomerId,
                        AccountId = accountId,
                        Debit = sale.Discount < 0 ? Math.Abs(Math.Round((sale.Discount), 4)) : 0,
                        Credit = sale.Discount > 0 ? Math.Abs(Math.Round((sale.Discount), 4)) : 0,
                        Description = "Discount/ Adjustment",
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });


                }


                // Un Registered VAT Transactions
                if (sale.UnRegisteredVatId != null)
                {

                    decimal total = (sale.SaleItems.Where(x => !x.IsFree).Sum(x => Math.Round((x.OfferQuantity == 0 ? (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))
                                : (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.OfferQuantity * x.Discount) / 100 : x.FixDiscount * x.OfferQuantity)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) - sale.Discount;

                    transactions.Add(new Domain.Entities.Transaction
                    {
                        Date = DateTime.UtcNow,
                        ContactId = sale.CustomerId,
                        AccountId = accounts.FirstOrDefault(x => x.Code == "25000001").Id,
                        Debit = 0,
                        Credit = Math.Abs(Math.Round((((sale.UnRegisteredVAT.Rate / 100) * total)), 4)),
                        Description = "Un Registered VAT",
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });


                }

                // account Receivable
                if (sale.CustomerId != null && !request.IsWalkIn && sale.IsCredit)
                {
                    decimal total = 0;
                    if (sale.IsDiscountOnTransaction)
                    {
                        var saleTotal = sale.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity);
                        var disc = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                        total = Math.Round(saleTotal + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + sale.Discount, 4);
                    }
                    else
                    {
                        total = (sale.SaleItems.Where(x => !x.IsFree).Sum(x => Math.Round((((x.UnitPrice * (x.Quantity - x.OfferQuantity)) - (x.Discount != 0 ? (x.UnitPrice * (x.Quantity - x.OfferQuantity) * x.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100), 2))) + sale.Discount;

                    }

                    total = sale.TotalAmount;
                    //For Credit Note sale Invoice
                    transactions.Add(new Domain.Entities.Transaction
                    {
                        Date = DateTime.UtcNow,
                        DocumentDate = sale.Date,
                        ApprovalDate = request.ApprovalDate,
                        ContactId = sale.CustomerId,
                        AccountId = sale.Customer.AccountId.Value,
                        //Debit = sale.SaleItems.Sum(x => Math.Round((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : x.FixDiscount * x.Quantity)) * (sale.TaxMethod == "Inclusive" ?  0: (100 + x.TaxRate.Rate))) / (sale.TaxMethod == "Inclusive" ? (100 + x.TaxRate.Rate) : 100))), 2)),
                        Debit = Math.Abs(Math.Round(total + ((sale.UnRegisteredVAT == null ? 0 : sale.UnRegisteredVAT.Rate / 100) * total), 4)),
                        Credit = 0,
                        Description = "Account Payment",
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });
                    if (!sale.IsCredit)
                    {
                        foreach (var payment in sale.SalePayments)
                        {
                            //For Credit Note sale Invoice
                            transactions.Add(new Domain.Entities.Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = sale.Date,
                                ApprovalDate = request.ApprovalDate,
                                ContactId = sale.CustomerId,
                                AccountId = sale.Customer.AccountId.Value,
                                Debit = 0,
                                Credit = payment.Received,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = sale.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = sale.RegistrationNo,
                                Year = sale.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = sale.BranchId,
                            });
                            //For Bank Cash Note sale Invoice
                            transactions.Add(new Domain.Entities.Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = sale.Date,
                                ApprovalDate = request.ApprovalDate,
                                ContactId = sale.CustomerId,
                                AccountId = payment.BankCashAccountId ?? terminalAccountId,
                                Debit = payment.Received,
                                Credit = 0,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = sale.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = sale.RegistrationNo,
                                Year = sale.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = sale.BranchId,
                            });

                            if (autoPaymentVoucher == "true" && payment.PaymentType == SalePaymentType.Advance && sale.SaleOrderId != null)
                            {
                                var saleAdvancePayment = new SaleInvoiceAdvancePayment()
                                {
                                    Amount = payment.Received,
                                    Date = DateTime.UtcNow,
                                    SaleInvoiceId = sale.Id,
                                    SaleOrderId = sale.SaleOrderId,
                                };
                                await _context.SaleInvoiceAdvancePayments.AddAsync(saleAdvancePayment);
                            }
                        }
                    }

                    if (request.CreditPays != null && sale.IsCredit)
                    {
                        decimal paidAmount = 0;
                        var salePayments = new List<SalePayment>();
                        foreach (var credit in request.CreditPays)
                        {
                            var pvNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery { PaymentVoucherType = PaymentVoucherType.BankReceipt });
                            await _mediator.Send(new AddUpdatePaymentVoucherCommand
                            {
                                VoucherNumber = pvNumber,
                                Amount = credit.Amount,
                                ApprovalStatus = ApprovalStatus.Approved,
                                SaleInvoice = sale.Id,
                                ContactAccountId = sale.Customer.AccountId.Value,
                                Date = sale.Date,
                                PaymentMode = credit.PaymentMode == "Cash" ? SalePaymentType.Cash : credit.PaymentMode == "Bank" ? SalePaymentType.Bank : SalePaymentType.Default,
                                BankCashAccountId = credit.PaymentMode == "Cash" ? cashAccountId : (Guid)credit.AccountId,
                                PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                UserName = request.UserName,
                                FromCreditInvoice = true,
                                Narration = credit.Description,
                                BranchId = sale.BranchId,
                            });




                            var salePayment = new SalePayment
                            {
                                DueAmount = credit.Amount,
                                Received = credit.Amount,
                                Balance = 0,
                                Change = 0,
                                PaymentType = credit.PaymentMode == "Cash" ? SalePaymentType.Cash : credit.PaymentMode == "Bank" ? SalePaymentType.Bank : SalePaymentType.Default,
                                BankCashAccountId = credit.AccountId,
                                Name = credit.PaymentMode,
                                SaleId = sale.Id,
                                Rate = 0,
                                Amount = 0,
                                Description = credit.Description
                            };
                            salePayments.Add(salePayment);

                            //decimal unRegisteredTax =  ((sale.UnRegisteredVAT.Rate / 100) * (credit.Amount + paidAmount + sale.Discount));

                            if (request.SalePayment.DueAmount + sale.UnRegisteredVAtAmount <= credit.Amount + paidAmount + sale.Discount)
                            {
                                sale.PartiallyInvoices = PartiallyInvoices.Fully;
                                _context.Sales.Update(sale);
                            }
                            else if (request.SalePayment.DueAmount + sale.UnRegisteredVAtAmount > credit.Amount + paidAmount + sale.Discount)
                            {
                                sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                _context.Sales.Update(sale);
                            }
                            paidAmount += credit.Amount;

                        }
                        if (paidAmount < request.SalePayment.DueAmount + sale.UnRegisteredVAtAmount)
                        {
                            var salePayment = new SalePayment
                            {
                                DueAmount = request.SalePayment.DueAmount - paidAmount,
                                Received = request.SalePayment.DueAmount - paidAmount,
                                Balance = 0,
                                Change = 0,
                                PaymentType = SalePaymentType.Credit,
                                Name = "Credit",
                                SaleId = sale.Id,
                                Rate = 0,
                                Amount = 0
                            };
                            salePayments.Add(salePayment);
                        }
                        await _context.SalePayments.AddRangeAsync(salePayments);
                    }
                }
                else
                {
                    //For walk in customer Cash sale Invoice
                    foreach (var payment in sale.SalePayments)
                    {
                        if (payment.Name == "Credit")
                        {
                            transactions.Add(new Domain.Entities.Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = sale.Date,
                                ApprovalDate = request.ApprovalDate,
                                ContactId = sale.CustomerId,
                                AccountId = sale.Customer.AccountId.Value,
                                //Debit = sale.SaleItems.Sum(x => Math.Round((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : x.FixDiscount * x.Quantity)) * (sale.TaxMethod == "Inclusive" ?  0: (100 + x.TaxRate.Rate))) / (sale.TaxMethod == "Inclusive" ? (100 + x.TaxRate.Rate) : 100))), 2)),
                                Debit = (autoPaymentVoucher == "true" && soItem != null) ? totalAmount : payment.Received,
                                Credit = 0,
                                Description = "Account Payment",
                                DocumentId = sale.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = sale.RegistrationNo,
                                Year = sale.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = sale.BranchId,
                            });

                            if (payment.Received > 0)
                            {
                                sale.PartiallyInvoices = PartiallyInvoices.Partially;
                                _context.Sales.Update(sale);
                            }

                        }
                        else

                        {
                            transactions.Add(new Domain.Entities.Transaction
                            {
                                Date = DateTime.UtcNow,
                                ContactId = sale.CustomerId,
                                AccountId = payment.PaymentType == SalePaymentType.Bank ? terminalAccountId //Bank "10500001"
                                    : cashAccountId, //Cash
                                Debit = (autoPaymentVoucher == "true" && soItem != null) ? totalAmount : payment.Received,
                                Credit = 0,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = sale.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = sale.RegistrationNo,
                                Year = sale.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = sale.BranchId,
                            });

                        }


                        if (payment.PaymentType == SalePaymentType.Bank)
                        {
                            if (terminalAccountId == Guid.Empty || terminalAccountId == null)
                            {
                                throw new NotFoundException("You not Select Bank Account in Invoice Setting", "Was not found");
                            }
                        }
                        if (payment.PaymentType == SalePaymentType.Cash)
                        {
                            if (cashAccountId == Guid.Empty || cashAccountId == null)
                            {
                                throw new NotFoundException("You not Select Cash Account in Invoice Setting", "Was not found");
                            }
                        }
                    }


                    if (sale.Change != 0)
                    { //change transaction
                        transactions.Add(new Domain.Entities.Transaction
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = cashAccountId,
                            Debit = 0,
                            Credit = sale.Change,
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                        if (cashAccountId == Guid.Empty || cashAccountId == null)
                        {
                            throw new NotFoundException("You not Select Cash Account in Invoice Setting", "Was not found");
                        }
                    }



                }
                await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions);
                await _context.Transactions.AddRangeAsync(transactions);
                await _mediator.Send(new AddUpdateSyncRecordCommand()
                {
                    SyncRecordModels = _context.SyncLog(),
                    BranchId = sale.BranchId,
                    Code = _code,
                    DocumentNo = sale.RegistrationNo
                });
                await _context.SaveChangesAsync();
            }

            private async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
