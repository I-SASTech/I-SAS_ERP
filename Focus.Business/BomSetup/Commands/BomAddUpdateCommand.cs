using DocumentFormat.OpenXml.Drawing.Charts;
using Focus.Business.BomSetup.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.BomSetup.Commands
{
    public class BomAddUpdateCommand : IRequest<Message>
    {
        public BomsLookupModel BomsInfo { get; set; }
        public Guid WareHouseId { get; set; }
        public bool IsPos { get; set; }

        public class Handler : IRequestHandler<BomAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<BomAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(BomAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.BomsInfo.Id != Guid.Empty)
                    {
                        var boms = await _context.Boms.FindAsync(request.BomsInfo.Id);
                        if (boms == null)
                            throw new NotFoundException("Boms Data Not Found", "");

                             var bomsRawsProducts = new List<BomRawProducts>();
                            foreach (var x in boms.BomSaleOrderItems)
                            {
                                foreach (var item in x.BomRawProducts)
                                {
                                    bomsRawsProducts.Add(item);
                                }

                            }

                            _context.BomRawProducts.RemoveRange(bomsRawsProducts);

                            _context.BomSaleOrderItems.RemoveRange(boms.BomSaleOrderItems);
                            
                            var bomSaleOrderItem = request.BomsInfo.BomSaleOrderItem.Select(x => new BomSaleOrderItems
                            {
                                BomId = boms.Id,
                                SaleOrderId = x.SaleOrderId,
                                ProductId = Guid.Parse(x.ProductId.ToString()),
                                Quantity = x.Quantity,
                                BomRawProducts = x.BomRawProducts.Select(y => new BomRawProducts
                                {
                                    ProductId = y.ProductId,
                                    Quantity = y.Quantity,
                                    SaleOrderId = x.SaleOrderId,
                                    Price = y.salePrice,
                                    CurrentQuantity = y.CurrentQuantity,
                                }).ToList(),
                            });

                            _context.BomSaleOrderItems.AddRange(bomSaleOrderItem);

                            boms.SaleOrderId = request.BomsInfo.SaleOrderId;
                            boms.Date = request.BomsInfo.Date;
                            boms.Code = request.BomsInfo.Code;
                            boms.CreatedDate = request.BomsInfo.CreatedDate;
                            boms.WareHouseId = request.BomsInfo.WareHouseId;
                            boms.ApprovalStatus = request.BomsInfo.ApprovalStatus;

                            // SaleOrder Updation
                            var saleOrder = await _context.SaleOrders.Include(x => x.SaleOrderItems).FirstOrDefaultAsync(x => x.Id == request.BomsInfo.SaleOrderId);

                            if (saleOrder != null)
                            {
                                foreach (var x in boms.BomSaleOrderItems)
                                {
                                    foreach (var item in saleOrder.SaleOrderItems)
                                    {
                                        if (item.Id == x.Id)
                                        {
                                            item.IsUsedForBom = true;
                                        }
                                    }
                                }
                            }

                            var bomSaleOrderItemCount = request.BomsInfo.BomSaleOrderItem.Sum(x => x.Quantity);
                            var saleOrderItemCount = saleOrder.SaleOrderItems.Sum(x => x.Quantity);

                            if (saleOrderItemCount == bomSaleOrderItemCount)
                            {
                                saleOrder.IsUsedForBom = true;
                            }


                            var transactions = new List<Transaction>();
                            var ledgerTransaction = new List<LedgerTransaction>();
                            // Stocks And Inventory
                            var stocks = await _context.Stocks.ToListAsync();

                            var accounts = await _context.Accounts.Where(x => x.Code == "60000101" || x.Code == "11100001").ToListAsync();

                            decimal total = 0;

                            foreach (var x in request.BomsInfo.BomSaleOrderItem)
                            {
                                foreach (var item in x.BomRawProducts)
                                {
                                    total = total + item.salePrice;
                                    var stock = stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == request.BomsInfo.WareHouseId);


                                    if (stock != null)
                                    {
                                        stock.CurrentQuantity -= (item.Quantity);
                                        stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    }


                                    var inv = new Inventory()
                                    {
                                        Date = request.BomsInfo.Date,
                                        DocumentId = boms.Id,
                                        DocumentNumber = boms.Code,
                                        Quantity = item.Quantity,
                                        ProductId = item.ProductId,
                                        StockId = stock.Id,
                                        WareHouseId = Guid.Parse(request.BomsInfo.WareHouseId.ToString()),
                                        Serial = "",
                                        TransactionType = TransactionType.ProductionBatch,
                                        SalePrice = item.salePrice,
                                        Price = item.salePrice,
                                    };

                                    await _context.Inventories.AddAsync(inv);


                                    if (request.IsPos)
                                    {
                                        ledgerTransaction.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = boms.Date,
                                            ApprovalDate = boms.Date,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                            Debit = 0,
                                            Credit = item.salePrice,
                                            Description = TransactionType.ProductionBatch.ToString(),
                                            DocumentId = boms.Id,
                                            TransactionType = TransactionType.ProductionBatch,
                                            DocumentNumber = boms.Code,
                                            Year = boms.Date.Year.ToString(),
                                            ProductId = Guid.Empty,
                                        });

                                        ledgerTransaction.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = boms.Date,
                                            ApprovalDate = boms.Date,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                                            Credit = 0,
                                            Debit = item.salePrice,
                                            Description = TransactionType.ProductionBatch.ToString(),
                                            DocumentId = boms.Id,
                                            TransactionType = TransactionType.SaleInvoice,
                                            DocumentNumber = boms.Code,
                                            Year = boms.Date.Year.ToString(),
                                            ProductId = Guid.Empty,
                                        });

                                    }
                                }

                            }

                            //Inventory Account
                            transactions.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = boms.Date,
                                ApprovalDate = boms.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                Debit = 0,
                                Credit = total,
                                Description = TransactionType.ProductionBatch.ToString(),
                                DocumentId = boms.Id,
                                TransactionType = TransactionType.ProductionBatch,
                                DocumentNumber = boms.Code,
                                Year = boms.Date.Year.ToString(),
                                ProductId = Guid.Empty,
                            });

                            //cost of good sale Account
                            transactions.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = boms.Date,
                                ApprovalDate = boms.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                                Credit = 0,
                                Debit = total,
                                Description = TransactionType.ProductionBatch.ToString(),
                                DocumentId = boms.Id,
                                TransactionType = TransactionType.ProductionBatch,
                                DocumentNumber = boms.Code,
                                Year = boms.Date.Year.ToString(),
                                ProductId = Guid.Empty,
                            });

                            if (ledgerTransaction.Count > 0)
                            {
                                await _context.LedgerTransactions.AddRangeAsync(ledgerTransaction);
                            }
                            await _context.Transactions.AddRangeAsync(transactions);


                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,

                            }, cancellationToken);

                            await _context.SaveChangesAsync();


                            return new Message
                            {
                                IsSuccess = true,
                                IsAddUpdate = "Data has been Added successfully"
                            };
                        

                    }
                    else
                    {
                        if(request.BomsInfo.ApprovalStatus == "Draft")
                        {
                            var bom = new Bom
                            {
                                SaleOrderId = request.BomsInfo.SaleOrderId,
                                Date = request.BomsInfo.Date,
                                Code = request.BomsInfo.Code,
                                CreatedDate = request.BomsInfo.CreatedDate,
                                WareHouseId = request.BomsInfo.WareHouseId,
                                ApprovalStatus = request.BomsInfo.ApprovalStatus,
                                BomSaleOrderItems = request.BomsInfo.BomSaleOrderItem.Select(x => new BomSaleOrderItems
                                {
                                    SaleOrderId = x.SaleOrderId,
                                    ProductId = Guid.Parse(x.ProductId.ToString()),
                                    Quantity = x.Quantity,
                                    BomRawProducts = x.BomRawProducts.Select(y => new BomRawProducts
                                    {
                                        ProductId = y.ProductId,
                                        Quantity = y.Quantity,
                                        SaleOrderId = x.SaleOrderId,
                                        Price = y.salePrice,
                                        CurrentQuantity = y.CurrentQuantity,
                                    }).ToList(),
                                }).ToList()
                            };

                            await _context.Boms.AddAsync(bom);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,

                            }, cancellationToken);

                            await _context.SaveChangesAsync();


                            return new Message
                            {
                                IsSuccess = true,
                                IsAddUpdate = "Data has been updated successfully"
                            };
                        }
                        else
                        {
                            var bom = new Bom
                            {
                                SaleOrderId = request.BomsInfo.SaleOrderId,
                                Date = request.BomsInfo.Date,
                                Code = request.BomsInfo.Code,
                                CreatedDate = request.BomsInfo.CreatedDate,
                                WareHouseId = request.BomsInfo.WareHouseId,
                                ApprovalStatus = request.BomsInfo.ApprovalStatus,
                                BomSaleOrderItems = request.BomsInfo.BomSaleOrderItem.Select(x => new BomSaleOrderItems
                                {
                                    SaleOrderId = x.SaleOrderId,
                                    ProductId = Guid.Parse(x.ProductId.ToString()),
                                    Quantity = x.Quantity,
                                    BomRawProducts = x.BomRawProducts.Select(y => new BomRawProducts
                                    {
                                        ProductId = y.ProductId,
                                        Quantity = y.Quantity,
                                        SaleOrderId = x.SaleOrderId,
                                        Price = y.salePrice,
                                        CurrentQuantity = y.CurrentQuantity,
                                    }).ToList(),
                                }).ToList()
                            };

                            await _context.Boms.AddAsync(bom);

                            // SaleOrder Updation
                            var saleOrder = await _context.SaleOrders.Include(x => x.SaleOrderItems).FirstOrDefaultAsync(x => x.Id == request.BomsInfo.SaleOrderId);

                            if (saleOrder != null)
                            {
                                foreach (var x in bom.BomSaleOrderItems)
                                {
                                    foreach (var item in saleOrder.SaleOrderItems)
                                    {
                                        if (item.Id == x.Id)
                                        {
                                            item.IsUsedForBom = true;
                                        }
                                    }
                                }
                            }

                            var bomSaleOrderItemCount = request.BomsInfo.BomSaleOrderItem.Sum(x => x.Quantity);
                            var saleOrderItemCount = saleOrder.SaleOrderItems.Sum(x => x.Quantity);

                            if (saleOrderItemCount == bomSaleOrderItemCount)
                            {
                                saleOrder.IsUsedForBom = true;
                            }


                            var transactions = new List<Transaction>();
                            var ledgerTransaction = new List<LedgerTransaction>();
                            // Stocks And Inventory
                            var stocks = await _context.Stocks.ToListAsync();

                            var accounts = await _context.Accounts.Where(x => x.Code == "60000101" || x.Code == "11100001").ToListAsync();

                            decimal total = 0;

                            foreach (var x in request.BomsInfo.BomSaleOrderItem)
                            {
                                foreach (var item in x.BomRawProducts)
                                {
                                    total = total + item.salePrice;
                                    var stock = stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == request.BomsInfo.WareHouseId);


                                    if (stock != null)
                                    {
                                        stock.CurrentQuantity -= (item.Quantity);
                                        stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    }


                                    var inv = new Inventory()
                                    {
                                        Date = request.BomsInfo.Date,
                                        DocumentId = bom.Id,
                                        DocumentNumber = bom.Code,
                                        Quantity = item.Quantity,
                                        ProductId = item.ProductId,
                                        StockId = stock.Id,
                                        WareHouseId = Guid.Parse(request.BomsInfo.WareHouseId.ToString()),
                                        Serial = "",
                                        TransactionType = TransactionType.ProductionBatch,
                                        SalePrice = item.salePrice,
                                        Price = item.salePrice,
                                    };

                                    await _context.Inventories.AddAsync(inv);


                                    if (request.IsPos)
                                    {
                                        ledgerTransaction.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = bom.Date,
                                            ApprovalDate = bom.Date,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                            Debit = 0,
                                            Credit = item.salePrice,
                                            Description = TransactionType.ProductionBatch.ToString(),
                                            DocumentId = bom.Id,
                                            TransactionType = TransactionType.ProductionBatch,
                                            DocumentNumber = bom.Code,
                                            Year = bom.Date.Year.ToString(),
                                            ProductId = Guid.Empty,
                                        });

                                        ledgerTransaction.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = bom.Date,
                                            ApprovalDate = bom.Date,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                                            Credit = 0,
                                            Debit = item.salePrice,
                                            Description = TransactionType.ProductionBatch.ToString(),
                                            DocumentId = bom.Id,
                                            TransactionType = TransactionType.SaleInvoice,
                                            DocumentNumber = bom.Code,
                                            Year = bom.Date.Year.ToString(),
                                            ProductId = Guid.Empty,
                                        });

                                    }
                                }

                            }

                            //Inventory Account
                            transactions.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = bom.Date,
                                ApprovalDate = bom.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                Debit = 0,
                                Credit = total,
                                Description = TransactionType.ProductionBatch.ToString(),
                                DocumentId = bom.Id,
                                TransactionType = TransactionType.ProductionBatch,
                                DocumentNumber = bom.Code,
                                Year = bom.Date.Year.ToString(),
                                ProductId = Guid.Empty,
                            });

                            //cost of good sale Account
                            transactions.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = bom.Date,
                                ApprovalDate = bom.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                                Credit = 0,
                                Debit = total,
                                Description = TransactionType.ProductionBatch.ToString(),
                                DocumentId = bom.Id,
                                TransactionType = TransactionType.ProductionBatch,
                                DocumentNumber = bom.Code,
                                Year = bom.Date.Year.ToString(),
                                ProductId = Guid.Empty,
                            });

                            if (ledgerTransaction.Count > 0)
                            {
                                await _context.LedgerTransactions.AddRangeAsync(ledgerTransaction);
                            }
                            await _context.Transactions.AddRangeAsync(transactions);


                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,

                            }, cancellationToken);

                            await _context.SaveChangesAsync();


                            return new Message
                            {
                                IsSuccess = true,
                                IsAddUpdate = "Data has been Added successfully"
                            };
                        }
                       
                    }

                   
                }

                catch (Exception e)
                {
                    var message = new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = e.Message
                    };
                    _logger.LogError(e.Message);

                    return message;
                }
            }
        }
    }
}
