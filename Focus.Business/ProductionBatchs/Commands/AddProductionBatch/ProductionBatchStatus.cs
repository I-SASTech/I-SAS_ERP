using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.ProductionBatchs.Queries.GetProductionBatchList;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Transactions.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductionBatchs.Commands.AddProductionBatch
{
    public class ProductionBatchStatus : IRequest<Guid>
    {
        public ProductionBatchStatusModel productionBatch { get; set; }
        public class Handler : IRequestHandler<ProductionBatchStatus, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<ProductionBatchStatus> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(ProductionBatchStatus request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.productionBatch.ApprovalStatus == ApprovalStatus.InProcess)
                    {
                        var purchase = await _context.ProductionBatchs.FindAsync(request.productionBatch.Id);

                        purchase.ApprovalStatus = request.productionBatch.ApprovalStatus;
                        purchase.LateReason = request.productionBatch.LateReason;
                        purchase.ProcessDate = System.DateTime.UtcNow;
                        purchase.ProcessBy = request.productionBatch.ProcessBy;
                    }
                    else if (request.productionBatch.ApprovalStatus == ApprovalStatus.Transfer)
                    {
                        // For Update Approval Status To Transfer and  Save Transfer User

                        var batch = _context.ProductionBatchs.AsNoTracking()
                            .Include(x => x.RecipeNo)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)
                             .FirstOrDefault(x => x.Id == request.productionBatch.Id);

                        if (batch == null)
                            throw new ApplicationException("Production Batch Not Found");

                        batch.ApprovalStatus = request.productionBatch.ApprovalStatus;
                        batch.TransferBy = request.productionBatch.TransferBy;
                        batch.TransferDate = System.DateTime.UtcNow;

                        _context.ProductionBatchs.Update(batch);



                        var depriciationAccount = _context.Accounts.FirstOrDefault(x => x.Code == "17000001");
                        if (depriciationAccount == null)
                        {
                            throw new ApplicationException("Depriciation Account Not Found");
                        }
                        //var accountInventory = _context.Accounts.FirstOrDefault(x => x.Code == "11100001");
                        //if (accountInventory == null)
                        //{
                        //    throw new ApplicationException("Inventory Account Not Found");
                        //}


                        //Production Stock Transfer Code
                        var transferCode = await _mediator.Send(new GetProductionTransferCode { }, cancellationToken);

                        //Production Stock Transfer 
                        var productionBatch = new ProductionStockTransfer()
                        {
                            Date = System.DateTime.UtcNow,
                            Code = transferCode,
                            DamageStock = batch.DamageStock,
                            RemainingStock = batch.RemainingStock,
                            UnitPrice = request.productionBatch.UnitPrice,
                            RemainingWareHouse = request.productionBatch.RemainingWareHouse.Value,
                            DamageWareHouse = request.productionBatch.DamageWareHouse.Value,
                            ProductionBatchId = batch.Id,
                            ProductId = batch.RecipeNo.ProductId

                        };
                        await _context.ProductionStockTransfers.AddAsync(productionBatch, cancellationToken);


                        // Transaction List Model
                        var transactions = new Collection<TransactionLookupModel>();

                        #region Remaining Stock

                        // Remaining Stock



                        var stockForRemainingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == batch.RecipeNo.ProductId && x.WareHouseId == request.productionBatch.RemainingWareHouse, cancellationToken);


                        if (stockForRemainingStock == null)
                        {
                            var newStock = new Stock()
                            {
                                ProductId = batch.RecipeNo.ProductId,
                                WareHouseId = (Guid)request.productionBatch.RemainingWareHouse
                            };

                            await _context.Stocks.AddAsync(newStock, cancellationToken);

                            stockForRemainingStock = newStock;
                        }
                        var CurrentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                        {
                            ProductId = batch.RecipeNo.ProductId,
                            StockId = stockForRemainingStock.Id,
                            WareHouseId = (Guid)request.productionBatch.RemainingWareHouse,
                        }, cancellationToken);


                        var remainingInventory = new Inventory()
                        {
                            Date = DateTime.UtcNow,
                            DocumentId = productionBatch.Id,
                            StockId = stockForRemainingStock.Id,
                            Quantity = Convert.ToInt32(batch.RemainingStock),
                            ProductId = batch.RecipeNo.ProductId,
                            WareHouseId = (Guid)request.productionBatch.RemainingWareHouse,
                            TransactionType = TransactionType.ProductionRemainingStock,
                            AveragePrice = request.productionBatch.RemainingStock == 0 ? Math.Round(request.productionBatch.UnitPrice, 2) : Math.Round((CurrentInventory.AveragePrice + request.productionBatch.UnitPrice) / 2, 2),
                            CurrentQuantity = CurrentInventory.CurrentQuantity + Convert.ToInt32(batch.RemainingStock),
                            CurrentStockValue = (CurrentInventory.CurrentQuantity + Convert.ToInt32(request.productionBatch.RemainingStock)) * (CurrentInventory.CurrentQuantity == 0 ? Math.Round(request.productionBatch.UnitPrice, 2) : Math.Round((CurrentInventory.AveragePrice + request.productionBatch.UnitPrice) / 2, 2)),
                            SalePrice = batch.RecipeNo.Product.SalePrice,
                        };
                        await _context.Inventories.AddAsync(remainingInventory, cancellationToken);


                        //inventory Acccount fro Remiaining Stock
                        // Depriciation Account for Credit
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            AccountId = depriciationAccount.Id,
                            Debit = 0,
                            Credit = request.productionBatch.UnitPrice * productionBatch.RemainingStock,
                            Description = TransactionType.ProductionRemainingStock.ToString(),
                            DocumentId = batch.Id,
                            TransactionType = TransactionType.ProductionRemainingStock,
                            DocumentNumber = productionBatch.Code,
                            Year = DateTime.UtcNow.Year.ToString(),
                            ProductId = batch.RecipeNo.ProductId
                        });

                        // Inventory Account for Debit
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            AccountId = batch.RecipeNo.Product.Category.InventoryAccountId,
                            Debit = request.productionBatch.UnitPrice * productionBatch.RemainingStock,
                            Credit = 0,
                            Description = TransactionType.ProductionRemainingStock.ToString(),
                            DocumentId = batch.Id,
                            TransactionType = TransactionType.ProductionRemainingStock,
                            DocumentNumber = productionBatch.Code,
                            Year = DateTime.UtcNow.Year.ToString(),
                            ProductId = batch.RecipeNo.ProductId
                        });


                        #endregion
                        #region Damage Stock


                        // Damage Stock
                        var stockForDamageStock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == batch.RecipeNo.ProductId && x.WareHouseId == request.productionBatch.DamageWareHouse, cancellationToken);
                        if (stockForDamageStock == null)
                        {
                            var damageStock = new Stock()
                            {
                                ProductId = batch.RecipeNo.ProductId,
                                WareHouseId = (Guid)request.productionBatch.DamageWareHouse
                            };

                            await _context.Stocks.AddAsync(damageStock, cancellationToken);

                            stockForDamageStock = damageStock;
                        }

                        var damageCurrentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                        {
                            ProductId = batch.RecipeNo.ProductId,
                            StockId = stockForDamageStock.Id,
                            WareHouseId = (Guid)request.productionBatch.DamageWareHouse,
                        }, cancellationToken);



                        var damageInventory = new Inventory()
                        {
                            Date = DateTime.UtcNow,
                            DocumentId = productionBatch.Id,
                            StockId = stockForDamageStock.Id,
                            Quantity = Convert.ToInt32(batch.DamageStock),
                            ProductId = batch.RecipeNo.ProductId,
                            WareHouseId = (Guid)request.productionBatch.DamageWareHouse,
                            TransactionType = TransactionType.ProductionDamageStock,
                            AveragePrice = request.productionBatch.DamageStock == 0 ? Math.Round(request.productionBatch.UnitPrice, 2) : Math.Round((CurrentInventory.AveragePrice + request.productionBatch.UnitPrice) / 2, 2),
                            CurrentQuantity = CurrentInventory.CurrentQuantity + Convert.ToInt32(batch.DamageStock),
                            CurrentStockValue = (CurrentInventory.CurrentQuantity + Convert.ToInt32(request.productionBatch.DamageStock)) * (CurrentInventory.CurrentQuantity == 0 ? Math.Round(request.productionBatch.UnitPrice, 2) : Math.Round((CurrentInventory.AveragePrice + request.productionBatch.UnitPrice) / 2, 2)),
                        };

                        await _context.Inventories.AddAsync(damageInventory, cancellationToken);


                        //inventory Acccount fro Damage Stock

                        //Damage Account for Inventory
                        //Debit
                        transactions.Add(new Focus.Business.Transactions.Commands.TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            AccountId = batch.RecipeNo.Product.Category.InventoryAccountId,
                            Debit = request.productionBatch.UnitPrice * productionBatch.DamageStock,
                            Credit = 0,
                            Description = TransactionType.ProductionDamageStock.ToString(),
                            DocumentId = batch.Id,
                            TransactionType = TransactionType.ProductionDamageStock,
                            DocumentNumber = productionBatch.Code,
                            Year = DateTime.UtcNow.Year.ToString(),
                            ProductId = batch.RecipeNo.ProductId
                        });



                        // Damage Account for Depriciation Credit

                        transactions.Add(new Focus.Business.Transactions.Commands.TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            AccountId = depriciationAccount.Id,
                            Debit = 0,
                            Credit = request.productionBatch.UnitPrice * productionBatch.DamageStock,
                            Description = TransactionType.ProductionDamageStock.ToString(),
                            DocumentId = batch.Id,
                            TransactionType = TransactionType.ProductionDamageStock,
                            DocumentNumber = productionBatch.Code,
                            Year = DateTime.UtcNow.Year.ToString(),
                            ProductId = batch.RecipeNo.ProductId
                        });


                        #endregion



                        //Transacrion Query
                        foreach (var transaction in transactions)
                        {
                            await _mediator.Send(new AddTransactionCommand { Transaction = transaction, Code = _code }, cancellationToken);
                        }

                    }




                    else
                    {
                        var purchase = await _context.ProductionBatchs
                     .FindAsync(request.productionBatch.Id);
                        purchase.ApprovalStatus = request.productionBatch.ApprovalStatus;
                        purchase.LateReasonCompletion = request.productionBatch.LateReasonCompletion;
                        purchase.DamageStock = request.productionBatch.DamageStock;
                        purchase.RemainingStock = request.productionBatch.RemainingStock;
                        purchase.CompleteBy = request.productionBatch.CompleteBy;
                        purchase.CompleteDate = System.DateTime.UtcNow;
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    return (Guid)request.productionBatch.Id;

                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}

