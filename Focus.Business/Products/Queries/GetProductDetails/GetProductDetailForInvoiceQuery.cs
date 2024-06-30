using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Business.Sales.Commands.AddSale;
using Focus.Domain.Enum;

namespace Focus.Business.Products.Queries.GetProductDetails
{
    public class GetProductDetailForInvoiceQuery : IRequest<ProductLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsFifo { get; set; }
        public bool ColorVariants { get; set; }
        public Guid? WareHouseId { get; set; }
        public int OpenBatch { get; set; }
        public Guid? BranchId { get; set; }
        public string BarCode { get; set; }

        public class Handler : IRequestHandler<GetProductDetailForInvoiceQuery, ProductLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductDetailForInvoiceQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ProductLookUpModel> Handle(GetProductDetailForInvoiceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.BarCode))
                    {
                        //Get Product Detail
                        var query = await _context.Products.Select(product => new ProductLookUpModel
                        {
                            Assortment = product.Assortment,
                            BasicUnit = product.BasicUnit,
                            BarCode = product.BarCode,
                            ColorId = product.ColorId,
                            Description = product.Description,
                            DisplayName = product.DisplayName,
                            Id = product.Id,
                            Image = product.Image,
                            IsExpire = product.IsExpire,
                            Length = product.Length,
                            LevelOneUnit = product.LevelOneUnit,
                            StockLevel = product.StockLevel,
                            SalePrice = product.SalePrice,
                            WholesalePrice = product.WholesalePrice,
                            SalePriceUnit = product.SalePriceUnit,
                            SaleReturnDays = product.SaleReturnDays,
                            Shelf = product.Shelf,
                            SizeId = product.SizeId,
                            StyleNumber = product.StyleNumber,
                            TaxMethod = product.TaxMethod,
                            TaxRateValue = product.TaxRate.Rate,
                            TaxRateId = product.TaxRateId,
                            UnitPerPack = product.UnitPerPack,
                            Width = product.Width,
                            Guarantee = product.Guarantee,
                            Serial = product.Serial,
                            CostPrice = product.CostPrice ?? 0,
                            SchemeQuantity = product.SchemeQuantity,
                            Scheme = product.Scheme,
                            WholesaleQuantity = product.WholesaleQuantity,
                            PurchasePrices = product.PurchasePrice,
                            ProductGroupId = product.ProductGroupId,

                            ServiceItem = product.ServiceItem,
                            BarCodeType = product.BarCodeType,
                            //PromotionOffer = promotionOffers.FirstOrDefault(x => x.ProductId == product.Id),
                            //BundleCategory = bundleCategories.FirstOrDefault(x => x.ProductId == product.Id),
                            SKU = product.SKU,
                            PartNumber = product.PartNumber,
                            BarCodeDisplayName = product.BarCodeDisplayName,
                            UnitNameEn = product.Unit.Name,
                            HighUnitPrice = product.HighUnitPrice,
                            ProductSizes = product.ProductSizes.Select(x => new ProductSize
                            {
                                ProductId = x.ProductId,
                                SizeId = x.SizeId,
                            }).ToList()
                        }).FirstOrDefaultAsync(x => x.BarCode == request.BarCode, cancellationToken: cancellationToken);


                        //Get Current Quantity
                        decimal currentQuantity;
                        if (request.WareHouseId != null)
                        {
                            currentQuantity = await _context.Stocks
                                .Where(x => x.ProductId == query.Id && x.WareHouseId == request.WareHouseId)
                                .Select(x => x.CurrentQuantity)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else
                        {
                            currentQuantity = await _context.Stocks
                                .Where(x => x.ProductId == query.Id)
                                .SumAsync(x => x.CurrentQuantity, cancellationToken: cancellationToken);
                        }


                        //Get Fifo Batch info
                        List<InventoryBatchLookUpModel> inventoryData = null;
                        if (request.IsFifo)
                        {
                            inventoryData = await _context.Inventories
                                .Where(x => x.ProductId == query.Id && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn) && x.IsActive && x.IsOpen)
                                .Take(request.OpenBatch)
                                .Select(x => new InventoryBatchLookUpModel
                                {

                                }).ToListAsync(cancellationToken: cancellationToken);
                        }


                        // Get Color Variants
                        var saleSizeAssortment = request.ColorVariants == false ? null : await _context.Sizes.AsNoTracking().OrderBy(x => x.Name).Select(x => new SaleSizeAssortmentModel
                        {
                            SizeId = x.Id,
                            Name = x.Name,
                            Quantity = 0
                        }).ToListAsync(cancellationToken: cancellationToken);



                        query.SaleSizeAssortment = saleSizeAssortment;
                        query.InventoryBatch = inventoryData;
                        query.Inventory = new Inventory
                        {
                            CurrentQuantity = currentQuantity
                        };


                        return query;
                    }
                    else
                    {
                        //Get Current Quantity
                        decimal currentQuantity;
                        if (request.WareHouseId != null)
                        {
                            currentQuantity = await _context.Stocks
                                .Where(x => x.ProductId == request.Id && x.WareHouseId == request.WareHouseId)
                                .Select(x => x.CurrentQuantity)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        }
                        else
                        {
                            currentQuantity = await _context.Stocks
                                .Where(x => x.ProductId == request.Id)
                                .SumAsync(x => x.CurrentQuantity, cancellationToken: cancellationToken);
                        }


                        //Get Fifo Batch info
                        List<InventoryBatchLookUpModel> inventoryData = null;
                        if (request.IsFifo)
                        {
                            inventoryData = await _context.Inventories
                                .Where(x => x.ProductId == request.Id && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn) && x.IsActive && x.IsOpen)
                                .Take(request.OpenBatch)
                                .Select(x => new InventoryBatchLookUpModel
                                {

                                }).ToListAsync(cancellationToken: cancellationToken);
                        }


                        // Get Color Variants
                        var saleSizeAssortment = request.ColorVariants == false ? null : await _context.Sizes.AsNoTracking().OrderBy(x => x.Name).Select(x => new SaleSizeAssortmentModel
                        {
                            SizeId = x.Id,
                            Name = x.Name,
                            Quantity = 0
                        }).ToListAsync(cancellationToken: cancellationToken);


                        //Get Product Detail
                        var query = await _context.Products.Select(product => new ProductLookUpModel
                        {
                            Assortment = product.Assortment,
                            BasicUnit = product.BasicUnit,
                            ColorId = product.ColorId,
                            Description = product.Description,
                            DisplayName = product.DisplayName,
                            Id = product.Id,
                            Image = product.Image,
                            IsExpire = product.IsExpire,
                            Length = product.Length,
                            LevelOneUnit = product.LevelOneUnit,
                            StockLevel = product.StockLevel,
                            SalePrice = product.SalePrice,
                            WholesalePrice = product.WholesalePrice,
                            SalePriceUnit = product.SalePriceUnit,
                            SaleReturnDays = product.SaleReturnDays,
                            Shelf = product.Shelf,
                            SizeId = product.SizeId,
                            StyleNumber = product.StyleNumber,
                            TaxMethod = product.TaxMethod,
                            //TaxRate = product.TaxRate,
                            TaxRateValue = product.TaxRate.Rate,
                            TaxRateId = product.TaxRateId,
                            UnitPerPack = product.UnitPerPack,
                            Width = product.Width,
                            Guarantee = product.Guarantee,
                            Serial = product.Serial,
                            CostPrice = product.CostPrice ?? 0,
                            SchemeQuantity = product.SchemeQuantity,
                            Scheme = product.Scheme,
                            WholesaleQuantity = product.WholesaleQuantity,
                            PurchasePrices = product.PurchasePrice,
                            ProductGroupId = product.ProductGroupId,
                            SaleSizeAssortment = saleSizeAssortment,
                            InventoryBatch = inventoryData,
                            ServiceItem = product.ServiceItem,
                            BarCodeType = product.BarCodeType,
                            //PromotionOffer = promotionOffers.FirstOrDefault(x => x.ProductId == product.Id),
                            //BundleCategory = bundleCategories.FirstOrDefault(x => x.ProductId == product.Id),
                            SKU = product.SKU,
                            PartNumber = product.PartNumber,
                            BarCodeDisplayName = product.BarCodeDisplayName,
                            Inventory = new Inventory()
                            {
                                CurrentQuantity = currentQuantity
                            },
                            HighUnitPrice = product.HighUnitPrice,
                            UnitNameEn = product.Unit.Name,
                            ProductSizes = product.ProductSizes.Select(x => new ProductSize
                            {
                                ProductId = x.ProductId,
                                SizeId = x.SizeId,
                            }).ToList()
                        }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);




                        return query;
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
