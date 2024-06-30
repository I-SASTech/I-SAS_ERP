using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.GoodReceives.Commands.AddGoodReceive
{
    public class AddUpdateGoodReceiveCommand : IRequest<Message>
    {
        //Get all variable in entity
        public GoodReceiveLookUp GoodReceive { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateGoodReceiveCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            private readonly ISendEmail sendEmail;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateGoodReceiveCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail)
            {
                Context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
            }


            public async Task<Message> Handle(AddUpdateGoodReceiveCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    // Check Financial Year
                    var period = await Context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.GoodReceive.Date.Year && x.PeriodStart.Month == request.GoodReceive.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    

                    if (request.GoodReceive.Id != Guid.Empty)
                    {
                        var goodReceives = await Context.GoodReceives.FindAsync(request.GoodReceive.Id);

                        if (goodReceives == null)
                            throw new NotFoundException("Good Receive", request.GoodReceive.RegistrationNo);



                        goodReceives.NationalId = request.GoodReceive.NationalId;
                        goodReceives.BillingId = request.GoodReceive.BillingId;
                        goodReceives.ShippingId = request.GoodReceive.ShippingId;
                        goodReceives.DeliveryId = request.GoodReceive.DeliveryId;


                        goodReceives.Date = request.GoodReceive.Date;
                        goodReceives.PurchaseOrderId = request.GoodReceive.PurchaseOrderId;
                        goodReceives.InvoiceDate = request.GoodReceive.InvoiceDate;
                        goodReceives.InvoiceNo = request.GoodReceive.InvoiceNo;
                        goodReceives.RegistrationNo = request.GoodReceive.RegistrationNo;
                        goodReceives.SupplierId = request.GoodReceive.SupplierId;
                        goodReceives.Note = request.GoodReceive.Note;
                        goodReceives.ApprovalStatus = request.GoodReceive.ApprovalStatus;
                        goodReceives.ApprovedBy = request.GoodReceive.ApprovedBy;

                        goodReceives.TaxMethod = request.GoodReceive.TaxMethod;
                        goodReceives.TaxRateId = request.GoodReceive.TaxRateId;
                        goodReceives.IsClose = request.GoodReceive.IsClose;
                        goodReceives.TotalAmount = request.GoodReceive.TotalAmount;
                        goodReceives.DiscountAmount = request.GoodReceive.DiscountAmount;
                        goodReceives.VatAmount = request.GoodReceive.VatAmount;
                        goodReceives.TotalWithOutAmount = request.GoodReceive.GrossAmount;
                        goodReceives.ReceivedAmount = request.GoodReceive.ReceivedAmount;
                        goodReceives.BranchId = request.GoodReceive.BranchId;
                        goodReceives.IsCredit = request.GoodReceive.IsCredit;
                        goodReceives.TransactionLevelDiscount = request.GoodReceive.TransactionLevelDiscount;
                        goodReceives.IsDiscountOnTransaction = request.GoodReceive.IsDiscountOnTransaction;
                        goodReceives.IsFixed = request.GoodReceive.IsFixed;
                        goodReceives.IsBeforeTax = request.GoodReceive.IsBeforeTax;
                        goodReceives.Discount = request.GoodReceive.Discount;
                        goodReceives.SupplierQuotationId = request.GoodReceive.SupplierQuotationId;
                        goodReceives.TotalAfterDiscount = request.GoodReceive.TotalAfterDiscount;
                        goodReceives.PoNumber = request.GoodReceive.PoNumber;
                        goodReceives.PoDate = request.GoodReceive.PoDate;
                        goodReceives.Reference = request.GoodReceive.Reference;
                        goodReceives.PaymentType = request.GoodReceive.PaymentType;
                        goodReceives.BankCashAccountId = request.GoodReceive.BankCashAccountId;
                        goodReceives.SoNumber = request.GoodReceive.GoodsRecieveNumberAndDate;
                        goodReceives.BomId = request.GoodReceive.BomId;
                        goodReceives.SaleOrderId = request.GoodReceive.SaleOrderId;

                        Context.GoodReceiveNoteItems.RemoveRange(goodReceives.GoodReceiveNoteItems);

                        goodReceives.GoodReceiveNoteItems = request.GoodReceive.GoodReceiveNoteItems.Select(x =>
                            new GoodReceiveNoteItem()
                            {
                                ProductId = x.ProductId,
                                UnitName = x.UnitName,
                                TaxRateId = x.TaxRateId,
                                Description = x.Description,
                                IsService = x.IsService,
                                Discount = x.Discount,
                                FixDiscount = Math.Round(x.FixDiscount, 2),
                                Quantity = x.Quantity,
                                PoQuantity = x.PoQuantity,
                                RemainingQuantity = x.Quantity,
                                ExpiryDate = x.ExpiryDate,
                                BatchNo = x.BatchNo,
                                HighQty = x.HighQty,
                                UnitPerPack = x.UnitPerPack,
                                UnitPrice = Math.Round(x.UnitPrice, 2),

                                TotalAmount = x.TotalAmount,
                                DiscountAmount = x.DiscountAmount,
                                VatAmount = x.VatAmount,
                                TotalWithOutAmount = x.GrossAmount,
                            }).ToList();






                        if (request.GoodReceive.BomId != null && request.GoodReceive.BomId != Guid.Empty)
                        {
                            var bom = await Context.Boms.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.GoodReceive.BomId.ToString()));

                            bom.Status = "Converted";

                            Context.Boms.Update(bom);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            BranchId = goodReceives.BranchId,
                            Code = _code,
                            DocumentNo = goodReceives.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = goodReceives.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;



                    }
                    else
                    {
                        var goodReceive = new GoodReceiveNote
                        {
                            NationalId = request.GoodReceive.NationalId,
                            BillingId = request.GoodReceive.BillingId,
                            ShippingId = request.GoodReceive.ShippingId,
                            DeliveryId = request.GoodReceive.DeliveryId,
                            Date = request.GoodReceive.Date,
                            RegistrationNo = request.GoodReceive.RegistrationNo,
                            PurchaseOrderId = request.GoodReceive.PurchaseOrderId,
                            SupplierId = request.GoodReceive.SupplierId,
                            ApprovedBy = request.GoodReceive.ApprovedBy,
                            InvoiceNo = request.GoodReceive.InvoiceNo,
                            InvoiceDate = request.GoodReceive.InvoiceDate,
                            Note = request.GoodReceive.Note,
                            ApprovalStatus = request.GoodReceive.ApprovalStatus,
                            TaxMethod = request.GoodReceive.TaxMethod,
                            TaxRateId = request.GoodReceive.TaxRateId,
                            IsClose = request.GoodReceive.IsClose,
                            TotalAmount = request.GoodReceive.TotalAmount,
                            DiscountAmount = request.GoodReceive.DiscountAmount,
                            VatAmount = request.GoodReceive.VatAmount,
                            TotalWithOutAmount = request.GoodReceive.GrossAmount,
                            ReceivedAmount = request.GoodReceive.ReceivedAmount,
                            BranchId = request.GoodReceive.BranchId,
                            IsCredit = request.GoodReceive.IsCredit,
                            TransactionLevelDiscount = request.GoodReceive.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.GoodReceive.IsDiscountOnTransaction,
                            IsFixed = request.GoodReceive.IsFixed,
                            IsBeforeTax = request.GoodReceive.IsBeforeTax,
                            Discount = request.GoodReceive.Discount,
                            SupplierQuotationId = request.GoodReceive.SupplierQuotationId,
                            TotalAfterDiscount = request.GoodReceive.TotalAfterDiscount,
                            PoNumber = request.GoodReceive.PoNumber,
                            PoDate = request.GoodReceive.PoDate,
                            PaymentType = request.GoodReceive.PaymentType,
                            BankCashAccountId = request.GoodReceive.BankCashAccountId,
                            Reference = request.GoodReceive.Reference,
                            SoNumber = request.GoodReceive.GoodsRecieveNumberAndDate,
                            BomId = request.GoodReceive.BomId,
                            SaleOrderId = request.GoodReceive.SaleOrderId,

                            GoodReceiveNoteItems = request.GoodReceive.GoodReceiveNoteItems.Select(x =>
                                new GoodReceiveNoteItem()
                                {

                                    UnitName = x.UnitName,
                                    ProductId = x.ProductId,
                                    TaxRateId = x.TaxRateId,
                                    Discount = x.Discount,
                                    Description = x.Description,
                                    IsService = x.IsService,
                                    FixDiscount = Math.Round(x.FixDiscount, 2),
                                    Quantity = x.Quantity,
                                    PoQuantity = x.PoQuantity,
                                    RemainingQuantity = x.Quantity,
                                    ExpiryDate = x.ExpiryDate,
                                    BatchNo = x.BatchNo,
                                    HighQty = x.HighQty,
                                    UnitPerPack = x.UnitPerPack,
                                    UnitPrice = Math.Round(x.UnitPrice, 2),

                                    TotalAmount = x.TotalAmount,
                                    DiscountAmount = x.DiscountAmount,
                                    VatAmount = x.VatAmount,
                                    TotalWithOutAmount = x.GrossAmount,
                                }).ToList(),



                        };
                        if (request.GoodReceive.PurchaseOrderId != null)
                        {
                            {
                                var purchaseOrder = await Context.PurchaseOrders.FindAsync(request.GoodReceive.PurchaseOrderId);


                                if (purchaseOrder != null)
                                {

                                    {
                                        foreach (var item in request.GoodReceive.GoodReceiveNoteItems)
                                        {
                                            if (item.ProductId == null)
                                            {
                                                var poDetail = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.GoodReceive.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                    Context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                            else
                                            {
                                                var poDetail = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.GoodReceive.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                    Context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                        }
                                    }

                                    var close = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x => x.PurchaseOrderId == request.GoodReceive.PurchaseOrderId && x.RemainingQuantity > 0);
                                    if (close == null)
                                    {
                                        purchaseOrder.IsProcessed = true;

                                        if (String.IsNullOrEmpty(purchaseOrder.Reason))
                                        {
                                            purchaseOrder.Reason = request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        else

                                        {
                                            purchaseOrder.Reason = purchaseOrder.Reason + "     ||   ||   " + request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }



                                        purchaseOrder.IsClose = true;
                                        purchaseOrder.PartiallyReceived = Domain.Enum.PartiallyInvoices.Fully;
                                    }
                                    else

                                    {
                                        purchaseOrder.IsProcessed = true;
                                        if (String.IsNullOrEmpty(purchaseOrder.Reason))
                                        {
                                            purchaseOrder.Reason = request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        else

                                        {
                                            purchaseOrder.Reason = purchaseOrder.Reason + "     ||   ||  " + request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        purchaseOrder.PartiallyReceived = Domain.Enum.PartiallyInvoices.Partially;

                                    }

                                }


                                
                            }
                        }
                        else if (request.GoodReceive.SupplierQuotationId != null)
                        {
                            {
                                var purchaseOrder = await Context.PurchaseOrders.FindAsync(request.GoodReceive.SupplierQuotationId);


                                if (purchaseOrder != null)
                                {

                                    {
                                        foreach (var item in request.GoodReceive.GoodReceiveNoteItems)
                                        {
                                            if (item.ProductId == null)
                                            {
                                                var poDetail = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.GoodReceive.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                    Context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                            else
                                            {
                                                var poDetail = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                                if (poDetail != null)
                                                {
                                                    poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.GoodReceive.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                                    Context.PurchaseOrderItems.Update(poDetail);

                                                }
                                            }
                                        }
                                    }

                                    var close = purchaseOrder.PurchaseOrderItems.FirstOrDefault(x =>  x.RemainingQuantity > 0);
                                    if (close == null)
                                    {
                                        purchaseOrder.IsProcessed = true;

                                        if (String.IsNullOrEmpty(purchaseOrder.Reason))
                                        {
                                            purchaseOrder.Reason = request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        else

                                        {
                                            purchaseOrder.Reason = purchaseOrder.Reason  + "     ||   ||      " + request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }



                                        purchaseOrder.IsClose = true;
                                        purchaseOrder.PartiallyReceived = Domain.Enum.PartiallyInvoices.Fully;
                                    }
                                    else

                                    {
                                        purchaseOrder.IsProcessed = true;
                                        if (String.IsNullOrEmpty(purchaseOrder.Reason))
                                        {
                                            purchaseOrder.Reason = request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        else

                                        {
                                            purchaseOrder.Reason = purchaseOrder.Reason + "     ||   ||      " + request.GoodReceive.RegistrationNo + " " + request.GoodReceive.Date.ToString("dd/MM/yyyy");

                                        }
                                        purchaseOrder.PartiallyReceived = Domain.Enum.PartiallyInvoices.Partially;

                                    }

                                }






                            }
                        }

                        //Add Department to database
                        await Context.GoodReceives.AddAsync(goodReceive, cancellationToken);

                        if (request.GoodReceive.BomId != null && request.GoodReceive.BomId != Guid.Empty)
                        {
                            var bom = await Context.Boms.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.GoodReceive.BomId.ToString()));

                            bom.Status = "Converted";

                            Context.Boms.Update(bom);
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            BranchId = goodReceive.BranchId,
                            Code = _code,
                            DocumentNo = goodReceive.RegistrationNo
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = goodReceive.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
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
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
