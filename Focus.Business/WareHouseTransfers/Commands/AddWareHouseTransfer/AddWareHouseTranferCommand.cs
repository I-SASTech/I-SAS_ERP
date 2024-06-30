using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Domain.Enum;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.WareHouseTransfers.Commands.AddWareHouseTransfer
{
    public class AddWareHouseTransferCommand : IRequest<Guid>
    {
        public WareHouseTransferLookupModel WareHouseTransfer { get; set; }

        public class Handler : IRequestHandler<AddWareHouseTransferCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddWareHouseTransferCommand> logger,
                IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddWareHouseTransferCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.WareHouseTransfer.Id == Guid.Empty)
                    {

                        var wareHouseTransfer = new WareHouseTransfer()
                        {
                            Date = request.WareHouseTransfer.Date,
                            ApprovalStatus = request.WareHouseTransfer.ApprovalStatus,
                            Code = request.WareHouseTransfer.Code,
                            FromWareHouseId = request.WareHouseTransfer.FromWareHouseId,
                            ToWareHouseId = request.WareHouseTransfer.ToWareHouseId,
                            BranchId = request.WareHouseTransfer.BranchId,
                            FromBranchId = request.WareHouseTransfer.FromBranchId,
                            ToBranchId = request.WareHouseTransfer.ToBranchId,
                            WareHouseTransferItems = request.WareHouseTransfer.WareHouseTransferItems.Select(x =>
                                new WareHouseTransferItem()
                                {
                                    ProductId = x.ProductId,

                                    Quantity = x.Quantity,


                                }).ToList()
                        };
                        await _context.WareHouseTransfers.AddAsync(wareHouseTransfer, cancellationToken);

                        if (request.WareHouseTransfer.AttachmentList != null && request.WareHouseTransfer.AttachmentList.Count > 0)
                        {
                            foreach (var item in request.WareHouseTransfer.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = wareHouseTransfer.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }


                        var invList = new List<Inventory>();
                        if (request.WareHouseTransfer.ApprovalStatus == ApprovalStatus.Approved)

                        {
                            foreach (var item in request.WareHouseTransfer.WareHouseTransferItems.ToList())
                            {
                                var currentInventoryFrom = await _mediator.Send(new GetLatestInventoryQuery()
                                {
                                    ProductId = item.ProductId,
                                    WareHouseId = wareHouseTransfer.FromWareHouseId,
                                    BranchId = wareHouseTransfer.FromBranchId,
                                }, cancellationToken);

                                var stockId = wareHouseTransfer.FromBranchId == null ?  await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.FromWareHouseId) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.FromWareHouseId);

                                if (stockId == null)
                                {
                                    var stock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = wareHouseTransfer.FromWareHouseId,
                                        BranchId = wareHouseTransfer.FromBranchId
                                    };
                                    stock.CurrentQuantity -= item.Quantity;
                                    _context.Stocks.Add(stock);

                                    stockId = stock;
                                }
                                else
                                {
                                    stockId.CurrentQuantity -= item.Quantity;
                                }
                                if (stockId.CurrentQuantity < item.Quantity)
                                    throw new ApplicationException("The quantity of product is  increase not available in 'From Ware House'");

                                invList.Add(new Inventory()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentNumber = wareHouseTransfer.Code,
                                    DocumentId = wareHouseTransfer.Id,
                                    Quantity = Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    //Price = Math.Round(currentInventoryFrom.AveragePrice, 2), //need to update
                                    Price = currentInventoryFrom.Price,
                                    ProductId = item.ProductId,
                                    StockId = currentInventoryFrom.StockId,
                                    PromotionId = Guid.Empty, //need to update
                                    BundleId = Guid.Empty, //need to update
                                    OfferQuantity = 0, //need to update
                                    Buy = 0, //need to update
                                    Get = 0, //need to update
                                    AveragePrice = currentInventoryFrom.AveragePrice,
                                    WareHouseId = wareHouseTransfer.FromWareHouseId,
                                    TransactionType = TransactionType.WareHouseTransferFrom,  //need to update
                                    //AveragePrice = currentInventoryFrom.Price == 0 ?
                                    //        Math.Round(currentInventoryFrom.AveragePrice, 2) :
                                    //        Math.Round((currentInventoryFrom.AveragePrice + currentInventoryFrom.AveragePrice) / 2, 2),  //need to update
                                    ExpiryDate = currentInventoryFrom.ExpiryDate,
                                    CurrentQuantity = currentInventoryFrom.CurrentQuantity - Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    CurrentStockValue = ((currentInventoryFrom.CurrentQuantity - item.Quantity) * currentInventoryFrom.AveragePrice),
                                    BranchId = wareHouseTransfer.FromBranchId

                                });

                                var currentInventoryTo = await _mediator.Send(new GetLatestInventoryQuery()
                                {
                                    ProductId = item.ProductId,
                                    WareHouseId = wareHouseTransfer.ToWareHouseId,
                                    BranchId = wareHouseTransfer.ToBranchId,
                                }, cancellationToken);

                                var stockTo = wareHouseTransfer.ToBranchId == null ?  await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.ToWareHouseId) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.ToWareHouseId && x.BranchId == wareHouseTransfer.ToBranchId);
                                if (stockTo == null)
                                {
                                    var stock1 = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = wareHouseTransfer.ToWareHouseId,
                                        BranchId = wareHouseTransfer.ToBranchId
                                    };
                                    stock1.CurrentQuantity += item.Quantity;
                                    _context.Stocks.Add(stock1);

                                    stockTo = stock1;
                                }
                                else
                                {
                                    stockTo.CurrentQuantity += item.Quantity;
                                }
                                invList.Add(new Inventory()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentNumber = wareHouseTransfer.Code,
                                    DocumentId = wareHouseTransfer.Id,
                                    Quantity = Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    //Price = Math.Round(currentInventoryTo.AveragePrice, 2), //need to update
                                    Price = currentInventoryFrom.Price,
                                    ProductId = item.ProductId,
                                    StockId = currentInventoryFrom.StockId,
                                    PromotionId = Guid.Empty, //need to update
                                    BundleId = Guid.Empty, //need to update
                                    OfferQuantity = 0, //need to update
                                    Buy = 0, //need to update
                                    Get = 0, //need to update
                                    WareHouseId = wareHouseTransfer.ToWareHouseId,
                                    TransactionType = TransactionType.WareHouseTransferTo,  //need to update
                                    AveragePrice = currentInventoryFrom.AveragePrice,
                                    //AveragePrice = currentInventoryTo.Price == 0 ?
                                    //            Math.Round(currentInventoryTo.AveragePrice, 2) :
                                    //            Math.Round((currentInventoryTo.AveragePrice + currentInventoryTo.AveragePrice) / 2, 2),  //need to update
                                    ExpiryDate = currentInventoryTo.ExpiryDate,
                                    CurrentQuantity = currentInventoryTo.CurrentQuantity + Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    CurrentStockValue = ((currentInventoryFrom.CurrentQuantity - item.Quantity) * currentInventoryFrom.AveragePrice),
                                    BranchId = wareHouseTransfer.ToBranchId

                                });

                            }

                            await _context.Inventories.AddRangeAsync(invList, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                           
                            Code = _code,
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return wareHouseTransfer.Id;
                    }
                    else
                    {
                        var wareHouseTransfer = await _context.WareHouseTransfers
                          .FindAsync(request.WareHouseTransfer.Id);

                        wareHouseTransfer.Date = request.WareHouseTransfer.Date;
                        wareHouseTransfer.Code = request.WareHouseTransfer.Code;
                        wareHouseTransfer.FromWareHouseId = request.WareHouseTransfer.FromWareHouseId;
                        wareHouseTransfer.ToWareHouseId = request.WareHouseTransfer.ToWareHouseId;
                        wareHouseTransfer.ApprovalStatus = request.WareHouseTransfer.ApprovalStatus;
                        wareHouseTransfer.BranchId = request.WareHouseTransfer.BranchId;
                        wareHouseTransfer.FromBranchId = request.WareHouseTransfer.FromBranchId;
                        wareHouseTransfer.ToBranchId = request.WareHouseTransfer.ToBranchId;

                        if (request.WareHouseTransfer.AttachmentList != null)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == wareHouseTransfer.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }
                            foreach (var item in request.WareHouseTransfer.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = wareHouseTransfer.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }

                        _context.WareHouseTransferItems.RemoveRange(wareHouseTransfer.WareHouseTransferItems);

                        wareHouseTransfer.WareHouseTransferItems = request.WareHouseTransfer.WareHouseTransferItems.Select(x =>
                                new WareHouseTransferItem()
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,

                                }).ToList();

                        var invList = new List<Inventory>();
                        if (request.WareHouseTransfer.ApprovalStatus == ApprovalStatus.Approved)

                        {
                            foreach (var item in request.WareHouseTransfer.WareHouseTransferItems.ToList())
                            {
                                var currentInventoryFrom = await _mediator.Send(new GetLatestInventoryQuery()
                                {
                                    ProductId = item.ProductId,
                                    WareHouseId = wareHouseTransfer.FromWareHouseId,
                                    BranchId = wareHouseTransfer.FromBranchId,
                                }, cancellationToken);

                                var stockId = wareHouseTransfer.FromBranchId == null ? await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.FromWareHouseId) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.FromWareHouseId);
                                if (stockId == null)
                                {
                                    var stock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = wareHouseTransfer.FromWareHouseId,
                                        BranchId = wareHouseTransfer.FromBranchId,
                                    };
                                    stock.CurrentQuantity -= item.Quantity;
                                    _context.Stocks.Add(stock);

                                    stockId = stock;
                                }
                                else
                                {
                                    stockId.CurrentQuantity -= item.Quantity;
                                }
                                if (currentInventoryFrom.CurrentQuantity < item.Quantity)
                                    throw new ApplicationException("The quantity of product is  increase not available in 'From Ware House'");

                                invList.Add(new Inventory()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentNumber = wareHouseTransfer.Code,
                                    DocumentId = wareHouseTransfer.Id,
                                    Quantity = Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    Price = Math.Round(currentInventoryFrom.AveragePrice, 2), //need to update
                                    ProductId = item.ProductId,
                                    StockId = currentInventoryFrom.StockId,
                                    PromotionId = Guid.Empty, //need to update
                                    BundleId = Guid.Empty, //need to update
                                    OfferQuantity = 0, //need to update
                                    Buy = 0, //need to update
                                    Get = 0, //need to update
                                    WareHouseId = wareHouseTransfer.FromWareHouseId,
                                    TransactionType = TransactionType.WareHouseTransferFrom,  //need to update
                                    AveragePrice = currentInventoryFrom.Price == 0 ?
                                            Math.Round(currentInventoryFrom.AveragePrice, 2) :
                                            Math.Round((currentInventoryFrom.AveragePrice + currentInventoryFrom.AveragePrice) / 2, 2),  //need to update
                                    ExpiryDate = currentInventoryFrom.ExpiryDate,
                                    CurrentQuantity = currentInventoryFrom.CurrentQuantity - Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    BranchId = wareHouseTransfer.FromBranchId
                                });

                                var currentInventoryTo = await _mediator.Send(new GetLatestInventoryQuery()
                                {
                                    ProductId = item.ProductId,
                                    WareHouseId = wareHouseTransfer.ToWareHouseId,
                                    BranchId = wareHouseTransfer.ToBranchId,
                                }, cancellationToken);

                                var stockTo = wareHouseTransfer.ToBranchId == null ? await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.ToWareHouseId) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == wareHouseTransfer.ToWareHouseId && x.BranchId == wareHouseTransfer.ToBranchId);
                                if (stockTo == null)
                                {
                                    var stock1 = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = wareHouseTransfer.ToWareHouseId,
                                        BranchId = wareHouseTransfer.ToBranchId
                                    };
                                    stock1.CurrentQuantity += item.Quantity;
                                    _context.Stocks.Add(stock1);

                                    stockTo = stock1;
                                }
                                else
                                {
                                    stockTo.CurrentQuantity += item.Quantity;
                                }
                                invList.Add(new Inventory()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentNumber = wareHouseTransfer.Code,
                                    DocumentId = wareHouseTransfer.Id,
                                    Quantity = Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    Price = Math.Round(currentInventoryTo.AveragePrice, 2), //need to update
                                    ProductId = item.ProductId,
                                    StockId = currentInventoryFrom.StockId,
                                    PromotionId = Guid.Empty, //need to update
                                    BundleId = Guid.Empty, //need to update
                                    OfferQuantity = 0, //need to update
                                    Buy = 0, //need to update
                                    Get = 0, //need to update
                                    WareHouseId = wareHouseTransfer.ToWareHouseId,
                                    TransactionType = TransactionType.WareHouseTransferTo,  //need to update
                                    AveragePrice = currentInventoryTo.Price == 0 ?
                                                Math.Round(currentInventoryTo.AveragePrice, 2) :
                                                Math.Round((currentInventoryTo.AveragePrice + currentInventoryTo.AveragePrice) / 2, 2),  //need to update
                                    ExpiryDate = currentInventoryTo.ExpiryDate,
                                    CurrentQuantity = currentInventoryTo.CurrentQuantity + Math.Abs(Convert.ToDecimal(item.Quantity)),
                                    BranchId = wareHouseTransfer.ToBranchId
                                });

                            }

                            await _context.Inventories.AddRangeAsync(invList, cancellationToken);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            
                            Code = _code,
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                        return wareHouseTransfer.Id;



                    }

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
