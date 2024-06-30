using Focus.Business.Interface;
using Focus.Business.StockAdjustments.Queries.GetStockAdjustmentList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Enum;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentDetails
{
   public class StockAdjustmentDetailQuery : IRequest<StockAdjustmentLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsFifo { get; set; }
        public int OpenBatch { get; set; }
        public class Handler : IRequestHandler<StockAdjustmentDetailQuery, StockAdjustmentLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;
            public readonly IMediator Mediator;


            public Handler(IApplicationDbContext context, ILogger<StockAdjustmentDetailQuery> logger, IMediator mediator)
            {
                _context = context;
                Logger = logger;
                Mediator = mediator;

            }

            public async Task<StockAdjustmentLookUpModel> Handle(StockAdjustmentDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sa = await _context.StockAdjustments
                        .AsNoTracking()
                        .Include(x => x.Warehouse)
                        .Include(x => x.TaxRate)
                        .Include(x => x.StockAdjustmentDetails)
                        .ThenInclude(x=>x.Product)
                        .ThenInclude(x=>x.Inventories)
                        .Include(x => x.StockAdjustmentDetails)
                        .ThenInclude(x=>x.Product)
                        .ThenInclude(x=>x.Unit)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == sa.Id)
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

                    IQueryable<Inventory> inventoryData = null;
                    if (request.IsFifo)
                    {

                        inventoryData = _context.Inventories.Where(x =>
                            x.IsActive && x.IsOpen && (x.TransactionType == TransactionType.PurchasePost ||
                                                       x.TransactionType == TransactionType.StockIn)).AsQueryable();

                    }
                    var items = new List<StockAdjustmentLookDetailUpModel>();
                    var stocks = await _context.Stocks.AsNoTracking().Where(x=>x.WareHouseId == sa.WarehouseId).ToListAsync(cancellationToken: cancellationToken);
                    foreach (var item in sa.StockAdjustmentDetails)
                    {
                        List<InventoryBatchLookUpModel> inventoryBatchDetail = null;
                        if (request.IsFifo)
                        {
                            inventoryBatchDetail = inventoryData?.Where(x => x.ProductId == item.ProductId ).Take(request.OpenBatch)
                                .Select(p =>
                                    new InventoryBatchLookUpModel()
                                    {
                                        ExpiryDate = p.ExpiryDate,
                                        AutoNumbering = p.AutoNumbering,
                                        BatchNumber = p.BatchNumber,
                                        IsOpen = p.IsOpen,
                                        ProductId = p.ProductId,
                                        Price = p.Price,
                                        RemainingQuantity = p.RemainingQuantity
                                    }).ToList();
                        }
                        items.Add(new StockAdjustmentLookDetailUpModel
                        {
                            Id = item.Id,
                            WarehouseId = item.WarehouseId,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            SalePrice = item.Product.SalePrice,
                            Serial = item.Serial,
                            GuaranteeDate = item.GuaranteeDate,
                            WarrantyTypeId = item.WarrantyTypeId,
                            SerialNo = item.SerialNo,


                            StockAdjustmentId = item.StockAdjustmentId,
                            BatchExpiry = item.BatchExpiry,
                            BatchNo = item.BatchNo,
                            Product = new ProductDropdownLookUpModel
                            {
                                Id = item.Product.Id,
                                Code = item.Product.Code,
                                EnglishName = item.Product.EnglishName,
                                ArabicName = item.Product.ArabicName,
                                DisplayName = item.Product.DisplayName,
                                SalePrice = item.Product.SalePrice,
                                UnitPerPack = item.Product.UnitPerPack,
                                LevelOneUnit = item.Product.LevelOneUnit,
                                BasicUnit = item.Product.Unit?.Name,
                                IsExpire = item.Product.IsExpire,
                                InventoryBatch = inventoryBatchDetail,
                                Serial = item.Product.Serial,
                                Guarantee = item.Product.Guarantee,
                                Inventory = sa.WarehouseId == null || sa.WarehouseId == Guid.Empty ? null : new Inventory()
                                {
                                    CurrentQuantity = stocks.FirstOrDefault(x => x.ProductId == item.Product.Id)?.CurrentQuantity ?? 0
                                },

                            }
                            
                        });
                    }
                    return new StockAdjustmentLookUpModel
                    {
                        Id = sa.Id,
                        Date = sa.Date,
                        Code = sa.Code,
                        Reason = sa.Reason,
                        Narration = sa.Narration,
                        WarehouseId = sa.WarehouseId,
                        isDraft = sa.isDraft,
                        TaxMethod = sa.TaxMethod,
                        TaxRateId = sa.TaxRateId,
                        IsSerial = sa.IsSerial,
                        BankCashAccountId = sa.BankCashAccountId,
                        TaxRateName = sa.TaxRate?.Name+ ""+ sa.TaxRate?.Rate,
                        WarehouseName = sa.Warehouse?.Name,
                        StockAdjustmentDetails = items,
                        AttachmentList = attachmentList,
                    };
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}