using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.StocksReceived.Models;
using Focus.Business.StocksTransfer.Commands;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.OpenXml4Net.OPC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.BranchUsers.Models;

namespace Focus.Business.StocksReceived.Commands
{
    public class StockReceivedAddUpdateCommand : IRequest<Message>
    {
        public StockReceivedLookupModel StockReceived { get; set; }

        public class Handler : IRequestHandler<StockReceivedAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            private IConfiguration Configuration { get; }
            public Handler(IApplicationDbContext context, ILogger<StockReceivedAddUpdateCommand> logger, IMediator mediator, IConfiguration configuration)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                Configuration = configuration;
            }
            public async Task<Message> Handle(StockReceivedAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                        _code += rnd.Next(0, 9).ToString();


                    if (request.StockReceived.Id != Guid.Empty)
                    {
                        var stockReceived = await _context.StockReceived.FindAsync(request.StockReceived.Id);
                        if (stockReceived == null)
                            throw new NotFoundException("No Stock Received Found", "");

                        stockReceived.Id = request.StockReceived.Id;
                        stockReceived.ApprovalStatus = request.StockReceived.ApprovalStatus;
                        stockReceived.Code = request.StockReceived.Code;
                        stockReceived.Date = request.StockReceived.Date;
                        stockReceived.WarehouseId = request.StockReceived.WarehouseId;
                        stockReceived.StockTransferId = request.StockReceived.StockTransferId;
                        stockReceived.BranchId = request.StockReceived.BranchId;
                        stockReceived.StockRequestId = request.StockReceived.StockRequestId;
                        stockReceived.TotalAmount = request.StockReceived.TotalAmount;

                        _context.StockReceivedItems.RemoveRange(stockReceived.StockReceivedItems);

                        stockReceived.StockReceivedItems = request.StockReceived.WareHouseTransferItems.Select(x => new StockReceivedItems
                        {
                            ProductId = x.ProductId,
                            Amount = x.Amount,
                            TransferAmount = x.TransferAmount,
                            BranchId = stockReceived.BranchId,
                            Quantity = x.Quantity,
                            ReceivedQuantity = x.ReceivedQuantity,
                            StockReceivedId = stockReceived.Id,
                            TransferQuantity = x.TransferQuantity,
                            RemainingQuantity = x.RemainingQuantity,
                            lineTotal = x.lineTotal,
                        }).ToList();


                        var stockTransfer = _context.StockTransfers.FirstOrDefault(x => x.Id == request.StockReceived.StockTransferId);
                        if (stockTransfer == null)
                            throw new NotFoundException("Stock Transfer Not Found", "");

                        var receivedQty = request.StockReceived.WareHouseTransferItems.All(x => x.TransferQuantity - x.ReceivedQuantity >= 0);

                        if (request.StockReceived.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var stocks = _context.Stocks.AsQueryable();

                            foreach (var item in request.StockReceived.WareHouseTransferItems)
                            {
                                var stock = stockReceived.BranchId == null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockReceived.WarehouseId, cancellationToken: cancellationToken) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockReceived.WarehouseId && x.BranchId == stockReceived.BranchId, cancellationToken: cancellationToken);

                                if (stock == null)
                                {
                                    var newStock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = stockReceived.WarehouseId.Value,
                                        CurrentQuantity = +item.ReceivedQuantity,
                                        BranchId = stockReceived.BranchId,
                                    };
                                    await _context.Stocks.AddAsync(newStock, cancellationToken);
                                    stock = newStock;
                                }
                                else
                                {
                                    stock.CurrentQuantity += item.ReceivedQuantity;
                                    stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    _context.Stocks.Update(stock);
                                }

                                var inv = new Inventory()
                                {
                                    Date = stockReceived.Date,
                                    DocumentId = stockReceived.Id,
                                    DocumentNumber = stockReceived.Code,
                                    Quantity = item.TransferQuantity,
                                    ProductId = item.ProductId,
                                    StockId = stock.Id,
                                    WareHouseId = stockReceived.WarehouseId.Value,
                                    TransactionType = TransactionType.StockTransferToBranch,
                                    BranchId = stockReceived.BranchId,
                                };
                                await _context.Inventories.AddAsync(inv, cancellationToken);
                            }
                        }

                        if (receivedQty)
                        {
                            stockTransfer.StockTransferStatus = StockTransferStatus.Received;

                            _context.StockTransfers.Update(stockTransfer);

                            await _context.SaveChangesAsync(cancellationToken);
                            var isDayStart = await _mediator.Send(new AddStockTransferVoucherCommand(), cancellationToken);

                        }
                        else
                        {
                            stockTransfer.StockTransferStatus = StockTransferStatus.PartiallyReceived;

                            _context.StockTransfers.Update(stockTransfer);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = stockTransfer.BranchId,
                            Code = _code,
                        }, cancellationToken);


                        await _context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            IsAddUpdate = "Data has been updated successfully",
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        var stockReceived = new StockReceived
                        {
                            ApprovalStatus = request.StockReceived.ApprovalStatus,
                            Code = request.StockReceived.Code,
                            Date = request.StockReceived.Date,
                            WarehouseId = request.StockReceived.WarehouseId,
                            StockTransferId = request.StockReceived.StockTransferId,
                            BranchId = request.StockReceived.BranchId,
                            FromBranchId = request.StockReceived.FromBranchId,
                            StockRequestId = request.StockReceived.StockRequestId,
                            TotalAmount = request.StockReceived.TotalAmount
                        };

                        await _context.StockReceived.AddAsync(stockReceived, cancellationToken);

                        var stockReceivedItems = new List<StockReceivedItems>();

                        foreach (var x in request.StockReceived.WareHouseTransferItems)
                        {
                            stockReceivedItems.Add(new StockReceivedItems
                            {
                                ProductId = x.ProductId,
                                Amount = x.Amount,
                                TransferAmount = x.TransferAmount,
                                BranchId = stockReceived.BranchId,
                                Quantity = x.Quantity,
                                ReceivedQuantity = x.ReceivedQuantity,
                                StockReceivedId = stockReceived.Id,
                                TransferQuantity = x.TransferQuantity,
                                RemainingQuantity = x.RemainingQuantity,
                                lineTotal = x.lineTotal
                            });
                        };
                        await _context.StockReceivedItems.AddRangeAsync(stockReceivedItems, cancellationToken);


                        var stockTransfer = await _context.StockTransfers
                            .AsNoTracking()
                            .Include(x => x.StockTransferItems)
                            .FirstOrDefaultAsync(x => x.Id == request.StockReceived.StockTransferId, cancellationToken: cancellationToken);


                        if (request.StockReceived.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var stocks = _context.Stocks.AsQueryable();

                            foreach (var item in request.StockReceived.WareHouseTransferItems)
                            {
                                var stock = stockReceived.BranchId == null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockReceived.WarehouseId, cancellationToken: cancellationToken) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockReceived.WarehouseId && x.BranchId == stockReceived.BranchId, cancellationToken: cancellationToken);
                                var transferItem = stockTransfer.StockTransferItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                if (transferItem != null)
                                {
                                    transferItem.RemainingQuantity -= item.ReceivedQuantity;
                                    _context.StockTransferItems.Update(transferItem);
                                }


                                if (stock == null)
                                {
                                    var newStock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = stockReceived.WarehouseId??Guid.Empty,
                                        CurrentQuantity = +item.ReceivedQuantity,
                                        BranchId = stockReceived.BranchId
                                    };
                                    await _context.Stocks.AddAsync(newStock, cancellationToken);
                                    stock = newStock;
                                }
                                else
                                {
                                    stock.CurrentQuantity += item.ReceivedQuantity;
                                    stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    _context.Stocks.Update(stock);
                                }

                                var inv = new Inventory()
                                {
                                    Date = stockReceived.Date,
                                    DocumentId = stockReceived.Id,
                                    DocumentNumber = stockReceived.Code,
                                    Quantity = item.TransferQuantity,
                                    ProductId = item.ProductId,
                                    StockId = stock.Id,
                                    WareHouseId = stockReceived.WarehouseId??Guid.Empty,
                                    TransactionType = TransactionType.StockTransferToBranch,
                                    BranchId = stockReceived.BranchId
                                };
                                await _context.Inventories.AddAsync(inv, cancellationToken);
                            }
                        }

                        var receivedQty = stockTransfer.StockTransferItems.Any(x => x.RemainingQuantity > 0);

                        if (!receivedQty)
                        {
                            stockTransfer.StockTransferStatus = StockTransferStatus.Received;
                        }
                        else
                        {
                            stockTransfer.StockTransferStatus = StockTransferStatus.PartiallyReceived;
                        }
                        _context.StockTransfers.Update(stockTransfer);
                        

                        if (request.StockReceived.BranchType=="Branches With Account")
                        {
                            await using var conn = new SqlConnection(Configuration.GetConnectionString("ServerConnection"));

                            var sqlQuery = "select Code from Branches where Id='" + stockReceived.FromBranchId + "';";
                            var branchCode = conn.Query<string>(sqlQuery).FirstOrDefault();
                            conn.Close();

                            //var branch = await _context.Branches.AsNoTracking().FirstOrDefaultAsync(x => x.Id==stockReceived.FromBranchId, cancellationToken);
                            var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= stockReceived.Date.Date && x.PeriodEnd >= stockReceived.Date.Date, cancellationToken: cancellationToken);

                            var inventoryAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code=="11100001", cancellationToken);
                            var branchAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code==branchCode && x.CostCenter.Code=="270000", cancellationToken);

                            var transactionList = new List<Transaction>
                                {
                                    // Inventory
                                    new Transaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = stockReceived.Date,
                                        ApprovalDate = stockReceived.Date,
                                        AccountId = inventoryAccount.Id,
                                        Credit = 0,
                                        Debit = stockReceived.TotalAmount,
                                        Description = "Stock Transfer",
                                        DocumentId = stockReceived.Id,
                                        TransactionType = TransactionType.StockTransferToBranch,
                                        DocumentNumber = stockReceived.Code,
                                        Year = stockReceived.Date.Year.ToString(),
                                        ProductId = Guid.Empty,
                                        BranchId = stockReceived.BranchId,
                                        PeriodId = period?.Id
                                    },
                                    // Account Payable
                                    new Transaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = stockReceived.Date,
                                        ApprovalDate = stockReceived.Date,
                                        AccountId = branchAccount.Id,
                                        Credit = stockReceived.TotalAmount,
                                        Debit = 0,
                                        Description = "Stock Transfer",
                                        DocumentId = stockReceived.Id,
                                        TransactionType = TransactionType.StockTransferToBranch,
                                        DocumentNumber = stockReceived.Code,
                                        Year = stockReceived.Date.Year.ToString(),
                                        ProductId = Guid.Empty,
                                        PeriodId = period?.Id,
                                        BranchId = stockReceived.BranchId
                                    }
                                };
                            await _context.Transactions.AddRangeAsync(transactionList, cancellationToken);
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = stockTransfer.BranchId,
                            Code = _code,
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsAddUpdate = "Data has been updated successfully",
                            IsSuccess = true,
                        };
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
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
