using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseTemplate.Command
{
    public class AddPurchaseTemplateCommand : IRequest<Message>
    {
        public PurchaseTemplateLookUpModel PurchaseTemplate { get; set; }

        public class Handler : IRequestHandler<AddPurchaseTemplateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddPurchaseTemplateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddPurchaseTemplateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.PurchaseTemplate.Id == Guid.Empty)
                    {
                        var purchaseTemplate = new AutoPurchaseTemplate()
                        {
                            Day = request.PurchaseTemplate.Day,
                            Date = request.PurchaseTemplate.Date,
                            RegistrationNo = request.PurchaseTemplate.RegistrationNo,
                            SupplierId = request.PurchaseTemplate.SupplierId,
                            Note = request.PurchaseTemplate.Note,
                            TaxMethod = request.PurchaseTemplate.TaxMethod,
                            TaxRateId = request.PurchaseTemplate.TaxRateId,
                            Raw = request.PurchaseTemplate.IsRaw,
                            IsActive = request.PurchaseTemplate.IsActive,
                            WareHouseId = request.PurchaseTemplate.WareHouseId,
                            BranchId = request.PurchaseTemplate.BranchId,
                            AutoPurchaseTemplateItems = request.PurchaseTemplate.PurchaseOrderItems.Select(x =>
                                new AutoPurchaseTemplateItem()
                                {
                                    ProductId = x.ProductId,
                                    TaxRateId = request.PurchaseTemplate.TaxRateId,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Quantity = request.PurchaseTemplate.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                    UnitPrice = x.UnitPrice,
                                    UnitPerPack = x.UnitPerPack,
                                    HighQty = x.HighQty
                                }).ToList()
                        };
                        await _context.AutoPurchaseTemplates.AddAsync(purchaseTemplate, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchaseTemplate.RegistrationNo,
                            BranchId= purchaseTemplate.BranchId
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = purchaseTemplate.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var purchase = await _context.AutoPurchaseTemplates.FindAsync(request.PurchaseTemplate.Id);

                        if (purchase == null)
                            throw new NotFoundException("Purchase Template Not Found", "");

                        purchase.Day = request.PurchaseTemplate.Day;
                        purchase.SupplierId = request.PurchaseTemplate.SupplierId;
                        purchase.Note = request.PurchaseTemplate.Note;
                        purchase.IsActive = request.PurchaseTemplate.IsActive;
                        purchase.TaxMethod = request.PurchaseTemplate.TaxMethod;
                        purchase.TaxRateId = request.PurchaseTemplate.TaxRateId;
                        purchase.Raw = request.PurchaseTemplate.IsRaw;
                        purchase.WareHouseId = request.PurchaseTemplate.WareHouseId;

                        _context.AutoPurchaseTemplateItems.RemoveRange(purchase.AutoPurchaseTemplateItems);

                        purchase.AutoPurchaseTemplateItems = request.PurchaseTemplate.PurchaseOrderItems.Select(x =>
                            new AutoPurchaseTemplateItem()
                            {
                                ProductId = x.ProductId,
                                TaxRateId = request.PurchaseTemplate.TaxRateId,
                                Discount = x.Discount,
                                FixDiscount = x.FixDiscount,
                                Quantity = request.PurchaseTemplate.IsMultiUnit ? Convert.ToInt32(((x.HighQty == null || x.HighQty == 0) ? 0 : x.HighQty * x.UnitPerPack) + x.Quantity) : x.Quantity,
                                UnitPrice = x.UnitPrice,
                                UnitPerPack = x.UnitPerPack,
                                HighQty = x.HighQty
                            }).ToList();


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=purchase.RegistrationNo,
                            BranchId = purchase.BranchId
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
                    _logger.LogError(notFoundException.Message);
                    return message;
                }
                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsAddUpdate = "Something went wrong! Contact to Support"
                    };
                    _logger.LogError(e.Message);

                    return message;
                }
            }
        }
    }
}
