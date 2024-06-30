using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ItemsBarCode.Queries
{
    public class GetItemsForBarCodeChangeQuery : PagedRequest, IRequest<PagedResult<ProductListModel>>
    {
        public Guid? WareHouseId { get; set; }
        public Guid? ProdutMasterId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? SizeId { get; set; }
        public string BarCodeType { get; set; }

        public class Handler : IRequestHandler<GetItemsForBarCodeChangeQuery, PagedResult<ProductListModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IConfiguration _configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetItemsForBarCodeChangeQuery> logger, IConfiguration configuration, IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _configuration = configuration;
                _contextProvider = contextProvider;
                _mediator = mediator;
            }
            public async Task<PagedResult<ProductListModel>> Handle(GetItemsForBarCodeChangeQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = await (from product in _context.Products.Include(x => x.Category)
                                       select new
                                       {
                                           product.Id,
                                           product.IsRaw,
                                           product.CostPrice,
                                           product.IsActive,
                                           product.ServiceItem,
                                           product.ProductMasterId,
                                           product.CategoryId,
                                           product.SubCategoryId,
                                           product.SizeId,
                                           product.ColorId,
                                           product.OriginId,
                                           BarCode = product.BarCode ?? "",
                                           product.Code,
                                           EnglishName = product.EnglishName ?? "",
                                           ArabicName = product.ArabicName ?? "",
                                           DisplayName = product.DisplayName ?? "",
                                           product.Assortment,
                                           product.BrandId,
                                           product.Image,
                                           product.IsExpire,
                                           product.Length,
                                           product.LevelOneUnit,
                                           product.StockLevel,
                                           product.SalePrice,
                                           product.Description,
                                           product.WholesalePrice,
                                           product.WholesaleQuantity,
                                           product.SalePriceUnit,
                                           product.SaleReturnDays,
                                           product.Shelf,
                                           product.StyleNumber,
                                           product.TaxMethod,
                                           product.UnitId,
                                           product.UnitPerPack,
                                           product.Width,
                                           product.Guarantee,
                                           product.Serial,
                                           product.HighUnitPrice,
                                           product.TaxRateId,
                                           product.SchemeQuantity,
                                           product.Scheme,
                                           product.BarCodeType,
                                           BasicUnit = product.Unit.Name,
                                           CategoryNameEn = product.Category.Name,
                                           CategoryNameAr = product.Category.NameArabic,
                                           ColorName = product.Color.Name,
                                           ColorNameArabic = product.Color.NameArabic,
                                           OriginNameEn = product.Origin.Name,
                                           OriginNameAr = product.Origin.NameArabic,
                                           MasterProductEn = product.ProductMaster.Name,
                                           MasterProductAr = product.ProductMaster.NameArabic,
                                           SizeName = product.Size.Name,
                                           SizeNameArabic = product.Size.NameArabic,
                                           TaxRate = product.TaxRate.Name,
                                           TaxRateValue = product.TaxRate.Rate,
                                           PurchasePrice = product.PurchasePrice,
                                           ProductGroupId = product.ProductGroupId,
                                           BranchItems = product.BranchItems,
                                           UnitNameEnglish = product.Unit.Name,
                                           UnitNameArabic = product.Unit.NameArabic,
                                           BrandNameEnlish = product.Brand.Name,
                                           BrandNameArabic = product.Brand.NameArabic,
                                           OriginNamEnglish = product.Origin.Name,
                                           OriginNamArabic = product.Origin.NameArabic,
                                           SKU = product.SKU,
                                           BarCodeDisplayName = product.BarCodeDisplayName,
                                           PartNumber = product.PartNumber,
                                           ProductSizes = product.ProductSizes.Select(x => new ProductSize
                                           {
                                               ProductId = x.ProductId,
                                               SizeId = x.SizeId,
                                           }).ToList(),
                                       }).ToListAsync(cancellationToken: cancellationToken);


                    var stocks = await _context.Stocks.Select(x => new { x.ProductId, x.WareHouseId }).ToListAsync();

                    if (request.ProdutMasterId != null)
                    {
                        query = query.Where(x => x.ProductMasterId == request.ProdutMasterId).ToList();
                    }

                    if (request.CategoryId != Guid.Empty && request.CategoryId != null)
                    {
                        query = query.Where(x => x.CategoryId == request.CategoryId).ToList();
                    }


                    if (request.SubCategoryId != null )
                    {
                        query = query.Where(x => x.SubCategoryId == request.SubCategoryId).ToList();
                    }


                    if (request.SizeId != null )
                    {
                        query = query.Where(x => x.SizeId == request.SizeId).ToList();
                    }


                    if (request.ColorId != null)
                    {
                        query = query.Where(x => x.ColorId == request.ColorId).ToList();
                    }


                    if (request.BarCodeType == "None")
                    {
                        query = query.Where(x => x.BarCodeType == request.BarCodeType).ToList();
                    }
                    else if (request.BarCodeType == "Generated")
                    {
                        query = query.Where(x => x.BarCodeType == request.BarCodeType).ToList();
                    }
                    else if (request.BarCodeType == "Scanned")
                    {
                        query = query.Where(x => x.BarCodeType == request.BarCodeType).ToList();
                    }

                    var warehouseItems = stocks.Where(x => x.WareHouseId == request.WareHouseId).ToList();
                    var wareHouseProduct = new List<ProductLookUpModel>();

                    var subcategoies = await _context.SubCategories.ToListAsync(cancellationToken: cancellationToken);

                    if (request.WareHouseId != Guid.Empty && request.WareHouseId != null)
                    {
                        foreach (var item in warehouseItems)
                        {
                            

                            var itemDetails = query.FirstOrDefault(x => x.Id == item.ProductId);

                            if (itemDetails != null)
                            {
                                var subCate = subcategoies.FirstOrDefault(x => x.Id == itemDetails.SubCategoryId);

                                wareHouseProduct.Add(new ProductLookUpModel
                                {
                                    ArabicName = itemDetails.ArabicName,
                                    DisplayName = itemDetails.DisplayName,
                                    Code = itemDetails.Code,
                                    EnglishName = itemDetails.EnglishName,
                                    StyleNumber = itemDetails.StyleNumber,
                                    UnitPerPack = Convert.ToDecimal(itemDetails.Length) * Convert.ToDecimal(itemDetails.Width),
                                    BarCode = itemDetails.BarCode,
                                    Assortment = itemDetails.Assortment,
                                    BasicUnit = itemDetails.BasicUnit,
                                    BrandId = itemDetails.BrandId,
                                    CategoryId = itemDetails.CategoryId,
                                    CategoryNameEn = itemDetails.CategoryNameEn,
                                    CategoryNameAr = itemDetails.CategoryNameAr,
                                    ColorId = itemDetails.ColorId,
                                    ColorName = itemDetails.ColorName,
                                    OriginNameEn = itemDetails.OriginNameEn,
                                    OriginNameAr = itemDetails.OriginNameAr,
                                    ColorNameArabic = itemDetails.ColorNameArabic,
                                    MasterProductEn = itemDetails.MasterProductEn,
                                    MasterProductAr = itemDetails.MasterProductAr,
                                    Description = itemDetails.Description,
                                    IsActive = itemDetails.IsActive,
                                    Id = itemDetails.Id,
                                    Image = itemDetails.Image,
                                    IsExpire = itemDetails.IsExpire,
                                    IsRaw = itemDetails.IsRaw,
                                    Length = itemDetails.Length,
                                    LevelOneUnit = itemDetails.LevelOneUnit,
                                    StockLevel = itemDetails.StockLevel,
                                    OriginId = itemDetails.OriginId,
                                    SalePrice = itemDetails.SalePrice,
                                    WholesalePrice = itemDetails.WholesalePrice,
                                    SalePriceUnit = itemDetails.SalePriceUnit,
                                    SaleReturnDays = itemDetails.SaleReturnDays,
                                    Shelf = itemDetails.Shelf,
                                    SizeId = itemDetails.SizeId,
                                    SizeName = itemDetails.SizeName,
                                    SizeNameArabic = itemDetails.SizeNameArabic,
                                    SubCategoryId = itemDetails.SubCategoryId,
                                    TaxMethod = itemDetails.TaxMethod,
                                    TaxRate = itemDetails.TaxRate,
                                    TaxRateValue = itemDetails.TaxRateValue,
                                    TaxRateId = itemDetails.TaxRateId,
                                    UnitId = itemDetails.UnitId,
                                    Width = itemDetails.Width,
                                    Guarantee = itemDetails.Guarantee,
                                    Serial = itemDetails.Serial,
                                    CostPrice = itemDetails.CostPrice ?? 0,
                                    ProductSizes = itemDetails.ProductSizes,
                                    SchemeQuantity = itemDetails.SchemeQuantity,
                                    Scheme = itemDetails.Scheme,
                                    WholesaleQuantity = itemDetails.WholesaleQuantity,
                                    PurchasePrices = itemDetails.PurchasePrice,
                                    ProductGroupId = itemDetails.ProductGroupId,
                                    ServiceItem = itemDetails.ServiceItem,
                                    BarCodeType = itemDetails.BarCodeType,
                                    SubCategoryEnglish = itemDetails.SubCategoryId != null ? subCate.Name : "",
                                    SubCategoryArabic = itemDetails.SubCategoryId != null ? subCate.NameArabic : "",
                                    BrandArabic = itemDetails.BrandNameArabic,
                                    BrandEnglish = itemDetails.BrandNameEnlish,
                                    UnitNameEn = itemDetails.UnitNameEnglish,
                                    UnitNameAr = itemDetails.UnitNameArabic,
                                    SKU = itemDetails.SKU,
                                    PartNumber = itemDetails.PartNumber,
                                    BarCodeDisplayName = itemDetails.BarCodeDisplayName
                                });
                            }

                        }
                       
                    }
                    if (wareHouseProduct.Count > 0)
                    {
                        var productLookUpModel = new List<ProductLookUpModel>();
                        var count = 0;
                        wareHouseProduct = wareHouseProduct.Take(request.PageSize).ToList();
                        foreach (var product in wareHouseProduct)
                        {
                            var subCate = subcategoies.FirstOrDefault(x => x.Id == product.SubCategoryId);

                            productLookUpModel.Add(new ProductLookUpModel()
                            {
                                ArabicName = product.ArabicName,
                                DisplayName = product.DisplayName,
                                Code = product.Code,
                                EnglishName = product.EnglishName,
                                StyleNumber = product.StyleNumber,
                                UnitPerPack = Convert.ToDecimal(product.Length) * Convert.ToDecimal(product.Width),
                                BarCode = product.BarCode,
                                NewBarCode = "",
                                IsGenerated = false,
                                Assortment = product.Assortment,
                                BasicUnit = product.BasicUnit,
                                BrandId = product.BrandId,
                                CategoryId = product.CategoryId,
                                CategoryNameEn = product.CategoryNameEn,
                                CategoryNameAr = product.CategoryNameAr,
                                ColorId = product.ColorId,
                                ColorName = product.ColorName,
                                OriginNameEn = product.OriginNameEn,
                                OriginNameAr = product.OriginNameAr,
                                ColorNameArabic = product.ColorNameArabic,
                                MasterProductEn = product.MasterProductEn,
                                MasterProductAr = product.MasterProductAr,
                                Description = product.Description,
                                IsActive = product.IsActive,
                                Id = product.Id,
                                Image = product.Image,
                                IsExpire = product.IsExpire,
                                IsRaw = product.IsRaw,
                                Length = product.Length,
                                LevelOneUnit = product.LevelOneUnit,
                                StockLevel = product.StockLevel,
                                OriginId = product.OriginId,
                                SalePrice = product.SalePrice,
                                WholesalePrice = product.WholesalePrice,
                                SalePriceUnit = product.SalePriceUnit,
                                SaleReturnDays = product.SaleReturnDays,
                                Shelf = product.Shelf,
                                SizeId = product.SizeId,
                                SizeName = product.SizeName,
                                SizeNameArabic = product.SizeNameArabic,
                                SubCategoryId = product.SubCategoryId,
                                TaxMethod = product.TaxMethod,
                                TaxRate = product.TaxRate,
                                TaxRateValue = product.TaxRateValue,
                                TaxRateId = product.TaxRateId,
                                UnitId = product.UnitId,
                                Width = product.Width,
                                Guarantee = product.Guarantee,
                                Serial = product.Serial,
                                CostPrice = Convert.ToDecimal(product.CostPrice),
                                ProductSizes = product.ProductSizes,
                                SchemeQuantity = product.SchemeQuantity,
                                Scheme = product.Scheme,
                                WholesaleQuantity = product.WholesaleQuantity,
                                PurchasePrices = Convert.ToDecimal(product.PurchasePrice),
                                ProductGroupId = product.ProductGroupId,
                                ServiceItem = product.ServiceItem,
                                BarCodeType = product.BarCodeType,
                                SubCategoryEnglish = product.SubCategoryId != null ? subCate.Name : "",
                                SubCategoryArabic = product.SubCategoryId != null ? subCate.NameArabic : "",
                                BrandArabic = product.BrandArabic,
                                BrandEnglish = product.BrandEnglish,
                                UnitNameEn = product.UnitNameEn,
                                UnitNameAr = product.UnitNameAr,
                                SKU = product.SKU,
                                PartNumber = product.PartNumber,
                                BarCodeDisplayName = product.BarCodeDisplayName
                            });
                        }

                        return new PagedResult<ProductListModel>
                        {
                            Results = new ProductListModel
                            {
                                Products = productLookUpModel
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = productLookUpModel.Count / request.PageSize
                        };
                    }
                    else
                    {
                        var productLookUpModel = new List<ProductLookUpModel>();
                        var count = 0;
                        query = query.Take(request.PageSize).ToList();
                        foreach (var product in query)
                        {
                            var subCate = subcategoies.FirstOrDefault(x => x.Id == product.SubCategoryId);

                            productLookUpModel.Add(new ProductLookUpModel()
                            {
                                ArabicName = product.ArabicName,
                                DisplayName = product.DisplayName,
                                Code = product.Code,
                                EnglishName = product.EnglishName,
                                StyleNumber = product.StyleNumber,
                                UnitPerPack = Convert.ToDecimal(product.Length) * Convert.ToDecimal(product.Width),
                                BarCode = product.BarCode,
                                NewBarCode = "",
                                IsGenerated = false,
                                Assortment = product.Assortment,
                                BasicUnit = product.BasicUnit,
                                BrandId = product.BrandId,
                                CategoryId = product.CategoryId,
                                CategoryNameEn = product.CategoryNameEn,
                                CategoryNameAr = product.CategoryNameAr,
                                ColorId = product.ColorId,
                                ColorName = product.ColorName,
                                OriginNameEn = product.OriginNameEn,
                                OriginNameAr = product.OriginNameAr,
                                ColorNameArabic = product.ColorNameArabic,
                                MasterProductEn = product.MasterProductEn,
                                MasterProductAr = product.MasterProductAr,
                                Description = product.Description,
                                IsActive = product.IsActive,
                                Id = product.Id,
                                Image = product.Image,
                                IsExpire = product.IsExpire,
                                IsRaw = product.IsRaw,
                                Length = product.Length,
                                LevelOneUnit = product.LevelOneUnit,
                                StockLevel = product.StockLevel,
                                OriginId = product.OriginId,
                                SalePrice = product.SalePrice,
                                WholesalePrice = product.WholesalePrice,
                                SalePriceUnit = product.SalePriceUnit,
                                SaleReturnDays = product.SaleReturnDays,
                                Shelf = product.Shelf,
                                SizeId = product.SizeId,
                                SizeName = product.SizeName,
                                SizeNameArabic = product.SizeNameArabic,
                                SubCategoryId = product.SubCategoryId,
                                TaxMethod = product.TaxMethod,
                                TaxRate = product.TaxRate,
                                TaxRateValue = product.TaxRateValue,
                                TaxRateId = product.TaxRateId,
                                UnitId = product.UnitId,
                                Width = product.Width,
                                Guarantee = product.Guarantee,
                                Serial = product.Serial,
                                CostPrice = Convert.ToDecimal(product.CostPrice),
                                ProductSizes = product.ProductSizes,
                                SchemeQuantity = product.SchemeQuantity,
                                Scheme = product.Scheme,
                                WholesaleQuantity = product.WholesaleQuantity,
                                PurchasePrices = Convert.ToDecimal(product.PurchasePrice),
                                ProductGroupId = product.ProductGroupId,
                                ServiceItem = product.ServiceItem,
                                BarCodeType = product.BarCodeType,
                                SubCategoryEnglish = product.SubCategoryId != null ? subCate.Name : "",
                                SubCategoryArabic = product.SubCategoryId != null ? subCate.NameArabic : "",
                                BrandArabic = product.BrandNameArabic,
                                BrandEnglish = product.BrandNameEnlish,
                                UnitNameEn = product.UnitNameEnglish,
                                UnitNameAr = product.UnitNameArabic,
                                SKU = product.SKU,
                                PartNumber = product.PartNumber,
                                BarCodeDisplayName = product.BarCodeDisplayName
                            });
                        }

                        return new PagedResult<ProductListModel>
                        {
                            Results = new ProductListModel
                            {
                                Products = productLookUpModel
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = productLookUpModel.Count / request.PageSize
                        };
                    }
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
