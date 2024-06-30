using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Purchases.Queries.GetPurchaseRegistrationNo;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Purchases.Commands.AddPurchase
{
    public class AddPurchaseCommand : IRequest<Message>
    {
        public PurchaseLookupModel Purchase { get; set; }

        public class Handler : IRequestHandler<AddPurchaseCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddPurchaseCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Purchase.Id == Guid.Empty)
                    {
                        var invoiceNo = await _mediator.Send(new GetPurchaseRegistrationNoQuery(), cancellationToken);
                        var invoiceNumber = invoiceNo.Draft;
                        //if (request.Purchase.InvoiceType == InvoiceType.Paid)
                        //{
                        //    invoiceNumber = invoiceNo.Paid;
                        //}
                        var itemList = new List<PurchaseItem>();
                        foreach (var item in request.Purchase.PurchaseItems)
                        {
                            if (item.Serial && Convert.ToInt32(string.IsNullOrEmpty(item.SerialNo)?"0": item.SerialNo) > 0)
                            {
                                for (int i = 0; i <= item.Quantity; i++)
                                {
                                    itemList.Add(new PurchaseItem
                                    {
                                        ProductId = item.ProductId,
                                        TaxRateId = request.Purchase.TaxRateId,
                                        Discount = item.Discount,
                                        FixDiscount = Math.Round(item.FixDiscount, 2),
                                        Quantity = 1,
                                        SerialNo = (Convert.ToInt32(item.SerialNo) + i).ToString(""),
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        HighQty = item.HighQty,
                                        GuaranteeDate = item.GuaranteeDate,
                                        UnitPerPack = item.UnitPerPack,
                                        UnitPrice = Math.Round(item.UnitPrice, 2),
                                        WareHouseId = request.Purchase.WareHouseId
                                    });
                                }
                            }
                            else
                            {
                                itemList.Add(new PurchaseItem
                                {
                                    ProductId = item.ProductId,
                                    TaxRateId = request.Purchase.TaxRateId,
                                    Discount = item.Discount,
                                    FixDiscount = Math.Round(item.FixDiscount, 2),
                                    Quantity = item.Quantity,
                                    ExpiryDate = item.ExpiryDate,
                                    BatchNo = item.BatchNo,
                                    HighQty = item.HighQty,
                                    SerialNo = item.SerialNo,
                                    GuaranteeDate = item.GuaranteeDate,
                                    UnitPerPack = item.UnitPerPack,
                                    UnitPrice = Math.Round(item.UnitPrice, 2),
                                    WareHouseId = request.Purchase.WareHouseId
                                });
                            }

                        }

                        var purchase = new Purchase()
                        {
                            Date = request.Purchase.Date,
                            PurchaseOrderNo = request.Purchase.PurchaseOrderNo,
                            RegistrationNo = invoiceNumber,
                            SupplierId = request.Purchase.SupplierId,
                            InvoiceNo = request.Purchase.InvoiceNo,
                            InvoiceDate = request.Purchase.InvoiceDate,
                            WareHouseId = request.Purchase.WareHouseId,
                            InvoiceFixDiscount = request.Purchase.InvoiceFixDiscount,
                            PurchaseOrderId = request.Purchase.PurchaseOrderId,
                            TaxMethod = request.Purchase.TaxMethod,
                            TaxRateId = request.Purchase.TaxRateId,
                            Raw = request.Purchase.IsRaw,
                            PurchaseItems = itemList
                        };

                        await _context.Purchases.AddAsync(purchase, cancellationToken);

                        var purchaseInvoiceAction = new List<PurchaseInvoiceAction>();
                        foreach (var action in request.Purchase.ActionProcess)
                        {
                            purchaseInvoiceAction.Add(new PurchaseInvoiceAction
                            {
                                PurchaseInvoiceId = purchase.Id,
                                ProcessId = action.ProcessId,
                                CurrentDate = DateTime.UtcNow,
                                Date = action.Date,
                                Description = action.Description
                            });
                        }
                        await _context.PurchaseInvoiceActions.AddRangeAsync(purchaseInvoiceAction, cancellationToken);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = purchase.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var purchase = await _context.Purchases
                          .FindAsync(request.Purchase.Id);

                        if (purchase == null)
                            throw new ApplicationException("Purchase invoice not found.");

                        purchase.Date = request.Purchase.Date;
                        purchase.InvoiceDate = request.Purchase.InvoiceDate;
                        purchase.InvoiceFixDiscount = request.Purchase.InvoiceFixDiscount;
                        purchase.InvoiceNo = request.Purchase.InvoiceNo;
                        purchase.RegistrationNo = request.Purchase.RegistrationNo;
                        purchase.SupplierId = request.Purchase.SupplierId;
                        purchase.WareHouseId = request.Purchase.WareHouseId;
                        purchase.PurchaseOrderId = request.Purchase.PurchaseOrderId;
                        purchase.TaxMethod = request.Purchase.TaxMethod;
                        purchase.TaxRateId = request.Purchase.TaxRateId;
                        purchase.Raw = request.Purchase.IsRaw;

                        _context.PurchaseItems.RemoveRange(purchase.PurchaseItems);

                        purchase.PurchaseItems = request.Purchase.PurchaseItems.Select(x =>
                                new PurchaseItem()
                                {
                                    ProductId = x.ProductId,
                                    TaxRateId = request.Purchase.TaxRateId,
                                    Discount = x.Discount,
                                    FixDiscount = Math.Round(x.FixDiscount, 2),
                                    Quantity = x.Quantity,
                                    UnitPrice = Math.Round(x.UnitPrice, 2),
                                    WareHouseId = request.Purchase.WareHouseId,
                                    ExpiryDate = x.ExpiryDate,
                                    BatchNo = x.BatchNo,
                                    HighQty = x.HighQty,
                                    UnitPerPack = x.UnitPerPack,
                                    SerialNo = x.SerialNo,
                                    GuaranteeDate = x.GuaranteeDate,
                                }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        
                        var message = new Message
                        {
                            Id = purchase.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;
                    }
                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        IsAddUpdate = notFoundException.Message
                    };
                    _logger.LogInformation(notFoundException.Message);
                    return message;
                }
                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsAddUpdate = "Something went wrong! Contact to Support"
                    };
                    _logger.LogInformation(e.Message);
                    return message;
                }
            }
        }
    }
}
