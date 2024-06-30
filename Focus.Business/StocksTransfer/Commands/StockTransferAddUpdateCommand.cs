using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.StocksTransfer.Models;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Domain.Enum;
using System.Linq;
using Focus.Business.SyncRecords.Commands;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Models;
using System.Collections.Generic;

namespace Focus.Business.StocksTransfer.Commands
{
    public class StockTransferAddUpdateCommand : IRequest<Message>
    {
        public StockTransferLookupModel StockTransfer { get; set; }

        public class Handler : IRequestHandler<StockTransferAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<StockTransferAddUpdateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(StockTransferAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.StockTransfer.Id != Guid.Empty)
                    {
                        var stockTransfer = await _context.StockTransfers.FirstOrDefaultAsync(x => x.Id==request.StockTransfer.Id, cancellationToken: cancellationToken);
                        if (stockTransfer == null)
                            throw new NotFoundException("No Stock Transfer Found", "");



                        stockTransfer.Code = request.StockTransfer.Code;
                        stockTransfer.Date = request.StockTransfer.Date;
                        stockTransfer.ApprovalStatus = request.StockTransfer.ApprovalStatus == ApprovalStatus.Draft ? ApprovalStatus.Draft : ApprovalStatus.Approved;

                        stockTransfer.StockRequestId = request.StockTransfer.StockRequestId;
                        stockTransfer.DriverName = request.StockTransfer.DriverName;
                        stockTransfer.DriverNumber = request.StockTransfer.DriverNumber;
                        stockTransfer.DriverNationalId = request.StockTransfer.DriverNationalId;
                        stockTransfer.VehicalNo = request.StockTransfer.VehicalNo;
                        stockTransfer.WarehouseId = request.StockTransfer.WareHouseId;
                        stockTransfer.TotalAmount = request.StockTransfer.TotalAmount;

                        _context.StockTransferItems.RemoveRange(stockTransfer.StockTransferItems);


                        var stockTransferItems = request.StockTransfer.wareHouseTransferItems.Select(x => new StockTransferItems
                        {
                            BranchId = x.BranchId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            StockTransferId = stockTransfer.Id,
                            Amount= x.Amount,
                            TransferAmount= x.TransferAmount,
                            TransferQuantity = x.TransferQuantity,
                            RemainingQuantity = x.TransferQuantity,
                            LineTotal = x.LineTotal,
                        }).ToList();
                        await _context.StockTransferItems.AddRangeAsync(stockTransferItems, cancellationToken);



                        if (request.StockTransfer.StockTransferStatus == StockTransferStatus.Issued)
                        {
                            var stockRequest = await _context.StockRequests.AsNoTracking()
                                .Include(x => x.StockRequestItems)
                                .FirstOrDefaultAsync(x => x.Id == request.StockTransfer.StockRequestId, cancellationToken: cancellationToken);

                            if (stockRequest==null)
                            {
                                throw new NotFoundException("NotFoundException", "");
                            }

                            stockTransfer.StockTransferStatus = StockTransferStatus.Transit;
                            stockTransfer.BranchId = stockTransfer.BranchId;
                            stockTransfer.StockRequesBranchtId = stockRequest?.FromBranchId;


                            var stocks = _context.Stocks.AsQueryable();

                            foreach (var item in request.StockTransfer.wareHouseTransferItems)
                            {
                                var stock = stockTransfer.BranchId == null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockTransfer.WarehouseId, cancellationToken: cancellationToken) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockTransfer.WarehouseId && x.BranchId == stockTransfer.BranchId, cancellationToken: cancellationToken);

                                var stockRequestItem = stockRequest.StockRequestItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                if (stockRequestItem != null)
                                {
                                    stockRequestItem.RemainingQuantity -= item.TransferQuantity;
                                    _context.StockRequestItems.Update(stockRequestItem);
                                }

                                if (stock == null)
                                {
                                    var newStock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = stockTransfer.WarehouseId.Value,
                                        CurrentQuantity = -item.TransferQuantity,
                                        BranchId = stockTransfer.BranchId,
                                    };
                                    await _context.Stocks.AddAsync(newStock, cancellationToken);
                                }
                                else
                                {
                                    stock.CurrentQuantity -= item.TransferQuantity;
                                    stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    _context.Stocks.Update(stock);
                                }
                            }

                            var remainingQty = stockRequest.StockRequestItems.Any(x => x.RemainingQuantity > 0);
                            if (!remainingQty)
                            {
                                stockRequest.StockRequestStatus = StockRequestStatus.Fully;
                                _context.StockRequests.Update(stockRequest);
                            }
                            else
                            {
                                stockRequest.StockRequestStatus = StockRequestStatus.Partially;
                                _context.StockRequests.Update(stockRequest);
                            }
                            _context.StockTransfers.Update(stockTransfer);


                            if (request.StockTransfer.BranchType=="Branches With Account")
                            {
                                var branch = await _context.Branches.AsNoTracking().FirstOrDefaultAsync(x => x.Id==stockTransfer.StockRequesBranchtId, cancellationToken);
                                var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= stockTransfer.Date.Date && x.PeriodEnd >= stockTransfer.Date.Date, cancellationToken: cancellationToken);

                                var inventoryAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code=="11100001", cancellationToken);
                                var branchAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code==branch.Code && x.CostCenter.Code=="140000", cancellationToken);

                                var transactionList = new List<Transaction>
                                {
                                    // Inventory
                                    new()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = stockTransfer.Date,
                                        ApprovalDate = stockTransfer.Date,
                                        AccountId = inventoryAccount.Id,
                                        Credit = stockTransfer.TotalAmount,
                                        Debit = 0,
                                        Description = "Stock Transfer",
                                        DocumentId = stockTransfer.Id,
                                        TransactionType = TransactionType.StockTransferToBranch,
                                        DocumentNumber = stockTransfer.Code,
                                        Year = stockTransfer.Date.Year.ToString(),
                                        ProductId = Guid.Empty,
                                        BranchId = stockTransfer.BranchId,
                                        PeriodId = period?.Id
                                    },
                                    // Account Payable
                                    new()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = stockTransfer.Date,
                                        ApprovalDate = stockTransfer.Date,
                                        AccountId = branchAccount.Id,
                                        Credit = 0,
                                        Debit = stockTransfer.TotalAmount,
                                        Description = "Stock Transfer",
                                        DocumentId = stockTransfer.Id,
                                        TransactionType = TransactionType.StockTransferToBranch,
                                        DocumentNumber = stockTransfer.Code,
                                        Year = stockTransfer.Date.Year.ToString(),
                                        ProductId = Guid.Empty,
                                        PeriodId = period?.Id,
                                        BranchId = stockTransfer.BranchId
                                    }
                                };
                                await _context.Transactions.AddRangeAsync(transactionList, cancellationToken);
                            }


                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = stockRequest.BranchId,
                                Code = _code,
                            }, cancellationToken);
                        }
                        else if (request.StockTransfer.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            stockTransfer.StockTransferStatus = StockTransferStatus.Issued;
                            stockTransfer.BranchId = request.StockTransfer.BranchId;
                            _context.StockTransfers.Update(stockTransfer);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = stockTransfer.BranchId,
                                Code = _code,
                            }, cancellationToken);
                        }



                        await _context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsAddUpdate = "Data has been updated successfully",
                            IsSuccess = true,
                        };
                    }
                    else
                    {
                        //var stockRequest = await _context.StockRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.StockTransfer.Id, cancellationToken: cancellationToken);

                        var stockTransfers = new StockTransfer
                        {
                            Code = request.StockTransfer.Code,
                            Date = request.StockTransfer.Date,
                            ApprovalStatus = request.StockTransfer.ApprovalStatus == ApprovalStatus.Draft ? ApprovalStatus.Draft : ApprovalStatus.Approved,
                            StockRequestId = request.StockTransfer.StockRequestId,
                            DriverName = request.StockTransfer.DriverName,
                            DriverNumber = request.StockTransfer.DriverNumber,
                            DriverNationalId = request.StockTransfer.DriverNationalId,
                            VehicalNo = request.StockTransfer.VehicalNo,
                            WarehouseId = request.StockTransfer.WareHouseId,
                            BranchId = request.StockTransfer.BranchId,
                            StockTransferStatus = request.StockTransfer.ApprovalStatus == ApprovalStatus.Draft ? StockTransferStatus.Default : StockTransferStatus.Issued,
                            StockRequesBranchtId = request.StockTransfer.StockRequesBranchtId,
                            TotalAmount = request.StockTransfer.TotalAmount,
                            StockTransferItems = request.StockTransfer.wareHouseTransferItems.Select(x => new StockTransferItems
                            {
                                BranchId = x.BranchId,
                                ProductId = x.ProductId,
                                Quantity = x.Quantity,
                                //StockTransferId = stockTransfers.Id,
                                Amount = x.Amount,
                                TransferAmount = x.TransferAmount,
                                TransferQuantity = x.TransferQuantity,
                                RemainingQuantity = x.TransferQuantity,
                                LineTotal = x.LineTotal,
                            }).ToList()
                        };
                        await _context.StockTransfers.AddAsync(stockTransfers, cancellationToken);

                        if (request.StockTransfer.ApprovalStatus == ApprovalStatus.Approved && request.StockTransfer.StockTransferStatus == StockTransferStatus.Transit)
                        {
                            var stocks = _context.Stocks.AsQueryable();

                            foreach (var item in request.StockTransfer.wareHouseTransferItems)
                            {
                                var stock = stockTransfers.BranchId==null ? await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockTransfers.WarehouseId, cancellationToken: cancellationToken) : await stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == stockTransfers.WarehouseId && x.BranchId==stockTransfers.BranchId, cancellationToken: cancellationToken);

                                if (stock == null)
                                {
                                    var newStock = new Stock()
                                    {
                                        ProductId = item.ProductId,
                                        WareHouseId = stockTransfers.WarehouseId.Value,
                                        CurrentQuantity = -item.TransferQuantity,
                                        BranchId = stockTransfers.BranchId,
                                    };
                                    await _context.Stocks.AddAsync(newStock, cancellationToken);
                                    stock = newStock;
                                }
                                else
                                {
                                    stock.CurrentQuantity -= item.TransferQuantity;
                                    stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    _context.Stocks.Update(stock);
                                }
                            }
                        }

                        
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = stockTransfers.StockRequesBranchtId,
                            Code = _code,
                        }, cancellationToken);


                        // Payable Liabilities
                        var branches = await _context.Branches.AsNoTracking()
                            .Where(x => x.Id==stockTransfers.BranchId || x.Id==stockTransfers.StockRequesBranchtId)
                            .Select(x => new
                            {
                                x.Id,
                                x.Code,
                                x.BranchName,
                            }).ToListAsync(cancellationToken: cancellationToken);

                        var costCenter = await _context.CostCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "270000", cancellationToken: cancellationToken);
                        var costCenterBranchReceivable = await _context.CostCenters.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "140000", cancellationToken: cancellationToken);
                        //If Cost Center is not created
                        if (costCenter == null)
                        {
                            var accountTypes = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Liabilities", cancellationToken: cancellationToken);
                            if (accountTypes == null)
                                throw new NotFoundException("Chart Of Account Not Found", "");
                            
                            var newCostCenter = new CostCenter
                            {
                                AccountTypeId = accountTypes.Id,
                                IsActive = true,
                                Code = "270000",
                                Name = "Payable Liabilities",
                                Description = "Payable Liabilities",
                                NameArabic = "الالتزامات المستحقة الدفع",
                                VatDeductible = false,
                            };
                            await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                            var newAccount1 = new Account
                            {
                                Code = branches[0].Code,
                                Name = branches[0].BranchName,
                                NameArabic = "",
                                Description = branches[0].BranchName,
                                IsActive = true,
                                CostCenterId = newCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(newAccount1, cancellationToken);

                            var newAccount2 = new Account
                            {
                                Code = branches[1].Code,
                                Name = branches[1].BranchName,
                                NameArabic = "",
                                Description = branches[1].BranchName,
                                IsActive = true,
                                CostCenterId = newCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(newAccount2, cancellationToken);
                        }
                        else
                        {
                            var account1 = await _context.Accounts.FirstOrDefaultAsync(x => x.Code == branches[0].Code && x.CostCenter.Code==costCenter.Code, cancellationToken: cancellationToken);
                            if (account1 == null)
                            {
                                var newAccount = new Account
                                {
                                    Code = branches[0].Code,
                                    Name = branches[0].BranchName,
                                    NameArabic = "",
                                    Description = branches[0].BranchName,
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            }
                            var account2 = await _context.Accounts.FirstOrDefaultAsync(x => x.Code == branches[1].Code && x.CostCenter.Code==costCenter.Code, cancellationToken: cancellationToken);
                            if (account2 == null)
                            {
                                var newAccount = new Account
                                {
                                    Code = branches[1].Code,
                                    Name = branches[1].BranchName,
                                    NameArabic = "",
                                    Description = branches[1].BranchName,
                                    IsActive = true,
                                    CostCenterId = costCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            }
                        }

                        if (costCenterBranchReceivable == null)
                        {
                            var accountTypes = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets", cancellationToken: cancellationToken);
                            if (accountTypes == null)
                                throw new NotFoundException("Chart Of Account Not Found", "");

                            var newCostCenter = new CostCenter
                            {
                                AccountTypeId = accountTypes.Id,
                                IsActive = true,
                                Code = "140000",
                                Name = "Branch Receivables",
                                Description = "Branch Receivables",
                                NameArabic = "مستحقات الفروع",
                                VatDeductible = false
                            };
                            await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                            var newAccount1 = new Account
                            {
                                Code = branches[0].Code,
                                Name = branches[0].BranchName,
                                NameArabic = "",
                                Description = branches[0].BranchName,
                                IsActive = true,
                                CostCenterId = newCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(newAccount1, cancellationToken);

                            var newAccount2 = new Account
                            {
                                Code = branches[1].Code,
                                Name = branches[1].BranchName,
                                NameArabic = "",
                                Description = branches[1].BranchName,
                                IsActive = true,
                                CostCenterId = newCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(newAccount2, cancellationToken);
                        }
                        else
                        {
                            var account1 = await _context.Accounts.FirstOrDefaultAsync(x => x.Code == branches[0].Code && x.CostCenter.Code==costCenterBranchReceivable.Code, cancellationToken: cancellationToken);
                            if (account1 == null)
                            {
                                var newAccount = new Account
                                {
                                    Code = branches[0].Code,
                                    Name = branches[0].BranchName,
                                    NameArabic = "",
                                    Description = branches[0].BranchName,
                                    IsActive = true,
                                    CostCenterId = costCenterBranchReceivable.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            }
                            var account2 = await _context.Accounts.FirstOrDefaultAsync(x => x.Code == branches[1].Code && x.CostCenter.Code==costCenterBranchReceivable.Code, cancellationToken: cancellationToken);
                            if (account2 == null)
                            {
                                var newAccount = new Account
                                {
                                    Code = branches[1].Code,
                                    Name = branches[1].BranchName,
                                    NameArabic = "",
                                    Description = branches[1].BranchName,
                                    IsActive = true,
                                    CostCenterId = costCenterBranchReceivable.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                            }
                        }

                        var log = _context.SyncLog();
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //    BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);

                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
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
